import { Component, Inject, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, NgForm, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseAlertType } from '@fuse/components/alert';
import { BusinessService } from '../../business/business.service';
import { ResultBusinessDto, ResultBusinessListDto } from '../../business/business.types';
import { RolesService } from '../roles.service';
import { CreateRoleDto, ResultRolesListDto } from '../roles.types';

@Component({
  selector: 'app-add-form',
  templateUrl: './add-form.component.html',
  styleUrls: ['./add-form.component.scss']
})

export class AddFormComponent implements OnInit {

  isLoading: boolean = false;
  @ViewChild('addRolesForm') addRolesForm: NgForm;
  roleDataForm: UntypedFormGroup;
  searchRoleDataForm: UntypedFormGroup;
  showAlert: boolean = false;
  currentList: ResultRolesListDto;
  isUpdate = false;
  stepper = 1;
  isLoadingType: boolean = false;
  userBusiness: ResultBusinessDto

  alert: { type: FuseAlertType; message: string } = {
    type: 'success',
    message: ''
  };

  constructor(
    private _formBuilder: FormBuilder,
    private _users: RolesService,
    private _businessServices: BusinessService,
    public dialogRef: MatDialogRef<AddFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { currentList: ResultRolesListDto, defaultRoles: any }
  ) { }

  /**
     * On init
     */
  ngOnInit(): void {
    // Create the form
    this.roleDataForm = this._formBuilder.group({
      id: [0],
      title: ['', [Validators.required]],
      description: ['n/a', [Validators.required]],
      businessId: ["", [Validators.required]]
    });

    if (this.data.defaultRoles != null) {
      this.roleDataForm.get("id").setValue(this.data.defaultRoles.id);
      this.roleDataForm.get("title").setValue(this.data.defaultRoles.title);
      this.roleDataForm.get("businessId").setValue(this.data.defaultRoles.businessId);
      this.roleDataForm.get("description").setValue(this.data.defaultRoles.description);
    }

    this.currentList = this.data.currentList

    if (this.data.defaultRoles != null) {
      this.isUpdate = true
    }

    this.getAllBusiness()
  }

  getAllBusiness(): void {
    this.isLoading = true
    this._businessServices.getAllBusiness(0, 5000, "asc").pipe()
      .subscribe((result: ResultBusinessListDto) => {
        this.userBusiness = result.data
        this.isLoading = false
      })
  }

  /**
     *  create property
     */
  createRole(): void {
    // Return if the form is invalid
    if (this.roleDataForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    var roleData: CreateRoleDto = this.roleDataForm.value;

    // Create New Type
    this._users.createRole(roleData)
      .subscribe(
        () => {
          // Reset the form
          // this.addRolesForm.resetForm();
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
  updateRole(): void {
    // Return if the form is invalid
    if (this.roleDataForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    // Create New Type
    this._users.updateRole(this.roleDataForm.value)
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

}

