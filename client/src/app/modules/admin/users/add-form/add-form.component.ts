import { Component, Inject, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, NgForm, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseAlertType } from '@fuse/components/alert';
import { UsersService } from '../users.service';
import { CreateUserDto, ResultUsersListDto } from '../users.types';

@Component({
  selector: 'app-add-form',
  templateUrl: './add-form.component.html',
  styleUrls: ['./add-form.component.scss']
})

export class AddFormComponent implements OnInit {

  isLoading: boolean = false;
  @ViewChild('searchUserNgForm') searchUserNgForm: NgForm;
  userDataForm: UntypedFormGroup;
  searchUserDataForm: UntypedFormGroup;
  showAlert: boolean = false;
  currentList: ResultUsersListDto;
  isUpdate = false;
  stepper = 1;
  isLoadingType: boolean = false;
  searchingUser: ResultUsersListDto = null;

  alert: { type: FuseAlertType; message: string } = {
    type: 'success',
    message: ''
  };

  constructor(
    private _formBuilder: FormBuilder,
    private _users: UsersService,
    public dialogRef: MatDialogRef<AddFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { currentList: ResultUsersListDto, defaultUsers: any }
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

    this.searchUserDataForm = this._formBuilder.group({
      keyword: ['', [Validators.required]]
    })

    if (this.data.defaultUsers != null) {
      this.userDataForm.get("id").setValue(this.data.defaultUsers.id);
      this.userDataForm.get("name").setValue(this.data.defaultUsers.name);
    }

    this.currentList = this.data.currentList

    if (this.data.defaultUsers != null) {
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
  createUser(): void {
    // Return if the form is invalid
    if (this.userDataForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    var userData: CreateUserDto = this.userDataForm.value;

    // Create New Type
    this._users.createUser(userData)
      .subscribe(
        () => {
          // Reset the form
          // this.addUsersForm.resetForm();
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
    this._users.updateUser(this.userDataForm.value)
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

  searchUser(): void {
    if(this.searchUserDataForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.searchingUser = null
    this.showAlert = false;

    this._users.searchUserByEmailOrUniqueId(this.searchUserDataForm.get("keyword").value).subscribe(
      (result: ResultUsersListDto) => {
        this.isLoading = false;
        this.searchingUser = result;
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

