import { Component, Inject, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, NgForm, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseAlertType } from '@fuse/components/alert';
import { AccessService } from '../access.service';
import { CreateAccesDto, ResultAccessListDto } from '../access.types';

@Component({
  selector: 'app-add-form',
  templateUrl: './add-form.component.html',
  styleUrls: ['./add-form.component.scss']
})

export class AddFormComponent implements OnInit {

  isLoading: boolean = false;
  @ViewChild('searchAccesNgForm') searchAccesNgForm: NgForm;
  userDataForm: UntypedFormGroup;
  searchAccesDataForm: UntypedFormGroup;
  showAlert: boolean = false;
  currentList: ResultAccessListDto;
  isUpdate = false;
  stepper = 1;
  isLoadingType: boolean = false;
  searchingAcces: ResultAccessListDto = null;

  alert: { type: FuseAlertType; message: string } = {
    type: 'success',
    message: ''
  };

  constructor(
    private _formBuilder: FormBuilder,
    private _users: AccessService,
    public dialogRef: MatDialogRef<AddFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { currentList: ResultAccessListDto, defaultAccess: any }
  ) { }

  /**
     * On init
     */
  ngOnInit(): void {
    // Create the form
    this.userDataForm = this._formBuilder.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      agreements: ['', Validators.requiredTrue]
    });

    this.searchAccesDataForm = this._formBuilder.group({
      keyword: ['', [Validators.required]]
    })

    if (this.data.defaultAccess != null) {
      this.userDataForm.get("id").setValue(this.data.defaultAccess.id);
      this.userDataForm.get("name").setValue(this.data.defaultAccess.name);
    }

    this.currentList = this.data.currentList

    if (this.data.defaultAccess != null) {
      this.isUpdate = true
    }
  }

  nextStepper(): void {
    this.stepper += 1
  }

  prevStepper(): void {
    this.stepper -= 1
  }

  /**
     *  create property
     */
  createAcces(): void {
    // Return if the form is invalid
    if (this.userDataForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    var userData: CreateAccesDto = this.userDataForm.value;

    // Create New Type
    this._users.createAcces(userData)
      .subscribe(
        () => {
          // Reset the form
          // this.addAccessForm.resetForm();
          this.dialogRef.close();
          // Set the alert
        },
        (response) => {
          console.log(response)
          // Set the alert
          this.alert = {
            type: 'error',
            message: response.error
          };

          this.showAlert = true
          // hide the loading
          this.isLoading = false;
        }
      );
  }

  /**
    *  create property
    */
  updateProperty(): void {
    // Return if the form is invalid
    if (this.userDataForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    // Create New Type
    this._users.updateAcces(this.userDataForm.value)
      .subscribe(
        () => {
          // Reset the form
          // this.propertyDataForm.resetForm();
          this.dialogRef.close();
          // Set the alert
        },
        (response) => {
          // console.log(response)
          // Set the alert
          this.alert = {
            type: 'error',
            message: response.error
          };

          this.showAlert = true
          // hide the loading
          this.isLoading = false;
        }
      );
  }

  searchAcces(): void {
    if(this.searchAccesDataForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.searchingAcces = null
    this.showAlert = false;

    this._users.searchAccesByEmailOrUniqueId(this.searchAccesDataForm.get("keyword").value).subscribe(
      (result: ResultAccessListDto) => {
        this.isLoading = false;
        this.searchingAcces = result;
      },
      (response) => {
        this.alert = {
          type: 'error',
          message: response.error
        };
        this.isLoading = false;
        this.showAlert = true;
      }
    )
  }

}

