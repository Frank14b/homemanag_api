import { Component, Inject, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, NgForm, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseAlertType } from '@fuse/components/alert';
import { appCities } from 'app/core/utils/app.cities';
import { appCountries } from 'app/core/utils/app.countries';
import { appUtils } from 'app/core/utils/app.utils';
import { PropertyTypesService } from '../property-types.service';
import { ResultListDto, ResultTypeDto } from '../property-types.types';
import { PropertiesService } from '../properties.service';
import { CreatePropertyDto, DataLocation, ResultPropertiesListDto } from '../properties.types';
import { ResultBusinessDto } from '../../business/business.types';
import { BusinessService } from '../../business/business.service';
import { ResultBusinessListDto } from 'app/modules/suadmin/business/business.types';

@Component({
  selector: 'app-add-form',
  templateUrl: './add-form.component.html',
  styleUrls: ['./add-form.component.scss']
})

export class AddFormComponent implements OnInit {

  isLoading: boolean = false;
  @ViewChild('addPropertyForm') addPropertyForm: NgForm;
  propertyDataForm: UntypedFormGroup;
  showAlert: boolean = false;
  currentList: ResultPropertiesListDto;
  currentListType: ResultTypeDto;
  isUpdate = false;
  stepper = 1;
  propertyMode = 0;
  allCountries = appCountries;
  allCities = [];
  displayCities = [];
  isLoadingType: boolean = false;
  dataLocation: DataLocation;
  userBusiness: ResultBusinessDto

  alert: { type: FuseAlertType; message: string } = {
    type: 'success',
    message: ''
  };

  constructor(
    private _formBuilder: FormBuilder,
    private _properties: PropertiesService,
    private _propertyTypeServices: PropertyTypesService,
    private _businessServices: BusinessService,
    public dialogRef: MatDialogRef<AddFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { currentList: ResultPropertiesListDto, defaultProperty: any }
  ) { }

  /**
     * On init
     */
  ngOnInit(): void {
    // Create the form
    this.propertyDataForm = this._formBuilder.group({
      id: [0],
      mode: [0, [Validators.required]],
      name: ['', [Validators.required]],
      address: ['', [Validators.required]],
      country: ['', [Validators.required]],
      city: ['', [Validators.required]],
      shortDesc: ['n/a', [Validators.required]],
      cityFilter: [''],
      description: ['n/a'],
      propertyTypeId: ["", [Validators.required]],
      businessId: ['', [Validators.required]]
    });

    if (this.data.defaultProperty != null) {
      this.propertyDataForm.get("id").setValue(this.data.defaultProperty.id);
      // this.propertyDataForm.get("mode").setValue(this.data.defaultProperty.mode);
      this.propertyDataForm.get("name").setValue(this.data.defaultProperty.name);
      this.propertyDataForm.get("address").setValue(this.data.defaultProperty.address);
      this.propertyDataForm.get("country").setValue(this.data.defaultProperty.country);
      this.propertyDataForm.get("city").setValue(this.data.defaultProperty.city);
      this.propertyDataForm.get("shortDesc").setValue(this.data.defaultProperty.shortDesc);
      this.propertyDataForm.get("description").setValue(this.data.defaultProperty.description);
      this.propertyDataForm.get("propertyTypeId").setValue(this.data.defaultProperty.propertyTypeId);
      this.propertyDataForm.get("businessId").setValue(this.data.defaultProperty.businessId);
      this.propertyDataForm.get("mode").setValue(this.data.defaultProperty.typeMode);

      this.filterCityByCountry(this.data.defaultProperty.country)
      
      this.dataLocation = {
        name: "",
        country: "",
        city: "",
        countryCode: "",
        url: "",
        lat: this.data.defaultProperty.lat,
        lng: this.data.defaultProperty.lng
      }
    }

    this.propertyDataForm.get("mode").valueChanges.subscribe(
      (value: any) => {
        this.propertyMode = value
      }
    )

    if (this.stepper == 1) {
      this.propertyDataForm.get("country").valueChanges.subscribe(
        (value: any) => {
          this.propertyDataForm.get("city").setValue("")
          if(value) {
            this.filterCityByCountry(value)
          }
        }
      )

      this.propertyDataForm.get("cityFilter").valueChanges.subscribe(
        (value: any) => {
          let data = this.allCities.filter((city) => city.toLowerCase().includes(value.toLowerCase()))

          this.displayCities = data.slice(0, 100)
        }
      )
    }

    this.currentList = this.data.currentList
    this.allCountries = appUtils.sortedCountries(this.allCountries)

    if (this.data.defaultProperty != null) {
      this.isUpdate = true
    }

    this.getAllTypes()
    this.getAllBusiness()
  }

  filterCityByCountry(country): void {
    let iso2 = this.allCountries.find((x) => x.name.common == country).cca2;
    let cities = appCities.find((x) => x.iso2 == iso2);
    this.allCities = cities.cities;
    this.displayCities = this.allCities.slice(0, 100)
  }

  getDataLocation(data: DataLocation) {
    this.dataLocation = data
  }

  nextStepper(): void {
    this.stepper += 1
  }

  prevStepper(): void {
    this.stepper -= 1
  }

  getAllTypes(): void {
    this.isLoading = true
    this._propertyTypeServices.getAllPropertyTypes(0, 5000, "asc").pipe()
      .subscribe((result: ResultListDto) => {
        this.currentListType = result.data
        this.isLoading = false
      })
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
  createProperty(): void {
    // Return if the form is invalid
    if (this.propertyDataForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    var propertyData: CreatePropertyDto = this.propertyDataForm.value
    propertyData.lat = this.dataLocation.lat
    propertyData.lng = this.dataLocation.lng

    // Create New Type
    this._properties.createProperty(propertyData)
      .subscribe(
        () => {
          // Reset the form
          // this.addPropertyForm.resetForm();
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
    if (this.propertyDataForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    // Create New Type
    this._properties.updateProperty(this.propertyDataForm.value)
      .subscribe(
        () => {
          // Reset the form
          // this.propertyDataForm.resetForm();
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

