import { Component, Inject, inject, Input, ViewChild } from '@angular/core';
import { FormBuilder, NgForm, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseAlertType } from '@fuse/components/alert';
import { appCountries } from 'app/core/utils/app.countries';
// import { appCities } from 'app/core/utils/app.cities';
import { BusinessService } from '../business.service';
import { appUtils } from 'app/core/utils/app.utils';
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
  allCountries = appCountries;
  allCities = [];
  displayCities = [];
  countryFlag = "";

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
      email: ['', [Validators.required]],
      country: ['', [Validators.required]],
      city: ['n/a'],
      lat: [0],
      lng: [0],
      countryCode: [0, [Validators.required]],
      phoneNumber: [0, [Validators.required]],
      description: ['n/a'],
      address: ['', [Validators.required]]
    });

    this.currentList = this.data.currentList

    if (this.data.defaultBusiness != null) {
      this.businessDataForm.get("id").setValue(this.data.defaultBusiness.id)
      this.businessDataForm.get("name").setValue(this.data.defaultBusiness.name)
      this.businessDataForm.get("email").setValue(this.data.defaultBusiness.email)
      this.businessDataForm.get("country").setValue(this.data.defaultBusiness.country)
      this.businessDataForm.get("phoneNumber").setValue(this.data.defaultBusiness.phoneNumber)
      this.businessDataForm.get("countryCode").setValue(this.data.defaultBusiness.countryCode)
      this.businessDataForm.get("description").setValue(this.data.defaultBusiness.description)
      this.businessDataForm.get("address").setValue(this.data.defaultBusiness.address)
      this.isUpdate = true
      this.countryFlag = this.allCountries.find((x) => x.name.common == this.data.defaultBusiness.country).flag
    }

    this.businessDataForm.get("country").valueChanges.subscribe(
      (value: any) => {
        //  this.businessDataForm.get("city").setValue("")

         let country_code = "";
         let countryCode_suffix = this.allCountries.find((x) => x.name.common == value).idd.suffixes
         if(countryCode_suffix.length == 1) {
            country_code = this.allCountries.find((x) => x.name.common == value).idd?.root + countryCode_suffix[0];
         }else{
            country_code = this.allCountries.find((x) => x.name.common == value).idd?.root
         }
         this.countryFlag = this.allCountries.find((x) => x.name.common == value).flag

         this.businessDataForm.get("countryCode").setValue(parseInt(country_code));
        //  let iso2 = this.allCountries.find((x) => x.name.common == value).cca2;
        //  let cities = appCities.find((x) => x.iso2 == iso2);
        //  this.allCities = cities.cities;
        //  this.displayCities = this.allCities.slice(0, 100)
      }
    )

    this.allCountries = appUtils.sortedCountries(this.allCountries)

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
            message: JSON.stringify(response.error)
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
    this._businessService.updateBusiness(this.addBusinessForm.value, this.businessDataForm.get("id").value)
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
            message: JSON.stringify(response.error)
          };

          this.showAlert = true
          // hide the loading
          this.isLoading = false;
        }
      );
  }

}
