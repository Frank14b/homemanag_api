import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertType } from '@fuse/components/alert';
import { AuthService } from 'app/core/auth/auth.service';
import {
    SocialAuthService,
    GoogleLoginProvider,
    SocialUser,
  } from '@abacritt/angularx-social-login';
import { environment } from 'environments';

@Component({
    selector     : 'auth-sign-in',
    templateUrl  : './sign-in.component.html',
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class AuthSignInComponent implements OnInit
{
    @ViewChild('signInNgForm') signInNgForm: NgForm;

    @ViewChild('loginRef', {static: true }) loginElement!: ElementRef;

    alert: { type: FuseAlertType; message: string } = {
        type   : 'success',
        message: ''
    };
    signInForm: UntypedFormGroup;
    showAlert: boolean = false;
    socialUser!: SocialUser;
    auth2: any;
    google_client_id:any = environment.GOOGLE_CLIENT_ID

    /**
     * Constructor
     */
    constructor(
        private _activatedRoute: ActivatedRoute,
        private _authService: AuthService,
        private _formBuilder: UntypedFormBuilder,
        private _router: Router,
        private socialAuthService: SocialAuthService
    ){}

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
        this.socialAuthService.signOut()

        // Create the form
        this.signInForm = this._formBuilder.group({
            login     : ['user', [Validators.required]],
            password  : ['admin', Validators.required],
            rememberMe: ['']
        });

        this.socialAuthService.authState.subscribe((user) => {
            this.socialUser = user;

            this._authService.socialSignUp(
                {
                    username: this.socialUser.name,
                    email: this.socialUser.email,
                    firstname: this.socialUser.firstName,
                    lastname: this.socialUser.lastName,
                    socialid: this.socialUser.id,
                    socialtoken: this.socialUser.idToken,
                    provider: this.socialUser.provider,
                    photourl: this.socialUser.photoUrl
                }
            ).subscribe(
                () => {

                    // Navigate to the confirmation required page
                    const redirectURL = this._activatedRoute.snapshot.queryParamMap.get('redirectURL') || '/signed-in-redirect';

                    // Navigate to the redirect url
                    this._router.navigateByUrl(redirectURL);
                },
                (response) => {
                    // Set the alert
                    this.alert = {
                        type   : 'error',
                        message: 'Something went wrong, please try again.'
                    };

                    // Show the alert
                    this.showAlert = true;
                }
            );
        });
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Sign in
     */
    signIn(): void
    {
        // Return if the form is invalid
        if ( this.signInForm.invalid )
        {
            return;
        }

        // Disable the form
        this.signInForm.disable();

        // Hide the alert
        this.showAlert = false;

        // Sign in
        this._authService.signIn(this.signInForm.value)
            .subscribe(
                () => {

                    // Set the redirect url.
                    // The '/signed-in-redirect' is a dummy url to catch the request and redirect the user
                    // to the correct page after a successful sign in. This way, that url can be set via
                    // routing file and we don't have to touch here.
                    const redirectURL = this._activatedRoute.snapshot.queryParamMap.get('redirectURL') || '/signed-in-redirect';

                    // Navigate to the redirect url
                    this._router.navigateByUrl(redirectURL);

                },
                (response) => {

                    // Re-enable the form
                    this.signInForm.enable();

                    // Reset the form
                    this.signInNgForm.resetForm();

                    // Set the alert
                    this.alert = {
                        type   : 'error',
                        message: 'Wrong login or password'
                    };

                    // Show the alert
                    this.showAlert = true;
                }
            );
    }

    //login using google social oauth2.0
    loginWithGoogle(): void
    {
        this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
    }
}
