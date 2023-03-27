import { Component, Inject, inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, NgForm, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseAlertType } from '@fuse/components/alert';
import { PropertyTypesService } from '../property-types.service';
import { ResultListDto } from '../property-types.types';

@Component({
  selector: 'app-add-property-types-form',
  templateUrl: './add-property-types-form.component.html',
  styleUrls: ['./add-property-types-form.component.scss']
})
export class AddPropertyTypesFormComponent {

  isLoading: boolean = false;
  @ViewChild('addTypeForm') addTypeForm: NgForm;
  typeDataForm: UntypedFormGroup;
  showAlert: boolean = false;
  currentList: ResultListDto;
  isUpdate = false

  alert: { type: FuseAlertType; message: string } = {
    type: 'success',
    message: ''
  };

  constructor(
    private _formBuilder: FormBuilder,
    private _propertyTypeServices: PropertyTypesService,
    public dialogRef: MatDialogRef<AddPropertyTypesFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { currentList: ResultListDto, defaultType: any }
  ) { }

  /**
     * On init
     */
  ngOnInit(): void {
    // Create the form
    this.typeDataForm = this._formBuilder.group({
      id: [0],
      name: ['', [Validators.required]],
      description: ['n/a'],
      subTypeId: [0]
    });

    this.currentList = this.data.currentList

    if (this.data.defaultType != null) {
      this.typeDataForm.get("id").setValue(this.data.defaultType.id)
      this.typeDataForm.get("name").setValue(this.data.defaultType.name)
      this.typeDataForm.get("description").setValue(this.data.defaultType.description)
      this.typeDataForm.get("subTypeId").setValue(this.data.defaultType.subTypeId)
      this.isUpdate = true
    }
  }

  /**
     *  create type
     */
  createType(): void {
    // Return if the form is invalid
    if (this.addTypeForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    // Create New Type
    this._propertyTypeServices.createType(this.addTypeForm.value)
      .subscribe(
        () => {
          // Reset the form
          this.addTypeForm.resetForm();
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
    if (this.addTypeForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    // Create New Type
    this._propertyTypeServices.updateType(this.addTypeForm.value)
      .subscribe(
        () => {
          // Reset the form
          this.addTypeForm.resetForm();
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
