import { Component, Inject, inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, NgForm, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseAlertType } from '@fuse/components/alert';
import { BusinessService } from '../business.service';
import { ResultBusinessListDto } from '../business.types';

@Component({
  selector: 'app-add-form',
  templateUrl: './add-form.component.html',
  styleUrls: ['./add-form.component.scss']
})
export class AddFormComponent {

  isLoading: boolean = false;
  @ViewChild('addBusinessForm') addBusinessForm: NgForm;
  businessDataForm: UntypedFormGroup;
  showAlert: boolean = false;
  currentList: ResultBusinessListDto;
  isUpdate = false

  alert: { type: FuseAlertType; message: string } = {
    type: 'success',
    message: ''
  };

  constructor(
    private _formBuilder: FormBuilder,
    private _businessService: BusinessService,
    public dialogRef: MatDialogRef<AddFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { currentList: ResultBusinessListDto, defaultBusiness: any }
  ) { }

  /**
     * On init
     */
  ngOnInit(): void {
    // Create the form
    this.businessDataForm = this._formBuilder.group({
      id: [0],
      name: ['', [Validators.required]],
      description: ['n/a'],
      address: ['', [Validators.required]]
    });

    this.currentList = this.data.currentList

    if (this.data.defaultBusiness != null) {
      this.businessDataForm.get("id").setValue(this.data.defaultBusiness.id)
      this.businessDataForm.get("name").setValue(this.data.defaultBusiness.name)
      this.businessDataForm.get("description").setValue(this.data.defaultBusiness.description)
      this.businessDataForm.get("address").setValue(this.data.defaultBusiness.address)
      this.isUpdate = true
    }
  }

  /**
     *  create type
     */
  createType(): void {
    // Return if the form is invalid
    if (this.addBusinessForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    // Create New Type
    this._businessService.createBusiness(this.addBusinessForm.value)
      .subscribe(
        () => {
          // Reset the form
          this.addBusinessForm.resetForm();
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
    *  create type
    */
  updateType(): void {
    // Return if the form is invalid
    if (this.addBusinessForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    // Create New Type
    this._businessService.updateBusiness(this.addBusinessForm.value)
      .subscribe(
        () => {
          // Reset the form
          this.addBusinessForm.resetForm();
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

}
