import { Component, Inject, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, NgForm, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseAlertType } from '@fuse/components/alert';
import { appCities } from 'app/core/utils/app.cities';
import { appCountries } from 'app/core/utils/app.countries';
import { appUtils } from 'app/core/utils/app.utils';
import { PropertyTypesService } from '../../property-types/property-types.service';
import { ResultListDto, ResultTypeDto } from '../../property-types/property-types.types';
import { PropertiesService } from '../properties.service';
import { DataLocation, ResultPropertiesListDto } from '../properties.types';

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

  alert: { type: FuseAlertType; message: string } = {
    type: 'success',
    message: ''
  };

  constructor(
    private _formBuilder: FormBuilder,
    private _properties: PropertiesService,
    private _propertyTypeServices: PropertyTypesService,
    public dialogRef: MatDialogRef<AddFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { currentList: ResultPropertiesListDto, defaultType: any }
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
      description: ['n/a'],
      propertyTypeId: ["", [Validators.required]]
    });

    this.propertyDataForm.get("mode").valueChanges.subscribe(
      (value: any) => {
        this.propertyMode = value
      }
    )

    this.propertyDataForm.get("country").valueChanges.subscribe(
      (value: any) => {
         this.propertyDataForm.get("city").setValue("")
         let iso2 = this.allCountries.find((x) => x.name.common == value).cca2;
         let cities = appCities.find((x) => x.iso2 == iso2);
         this.allCities = cities.cities;
         this.displayCities = this.allCities.slice(0, 100)
      }
    )

    this.propertyDataForm.get("cityFilter").valueChanges.subscribe(
      (value: any) => {
         let data = this.allCities.filter((city) => city.toLowerCase().includes(value.toLowerCase()))

         this.displayCities = data.slice(0, 100)
      }
    )

    this.currentList = this.data.currentList
    this.allCountries = appUtils.sortedCountries(this.allCountries)

    if (this.data.defaultType != null) {
      this.isUpdate = true
    }

    this.getAllTypes()
  }

  getDataLocation(data:DataLocation)
  {
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
    this._propertyTypeServices.getAllPropertyTypes(0, 5000, "desc").pipe()
    .subscribe((result: ResultListDto) => {
      this.currentListType = result.data
      this.isLoading = false
    })
  }

  /**
     *  create type
     */
  createType(): void {
    // Return if the form is invalid
    if (this.addPropertyForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    // Create New Type
    this._properties.createProperty(this.addPropertyForm.value)
      .subscribe(
        () => {
          // Reset the form
          this.addPropertyForm.resetForm();
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
    if (this.addPropertyForm.invalid) {
      return;
    }

    // Show the loading
    this.isLoading = true;
    this.showAlert = false;

    // Create New Type
    this._properties.updateProperty(this.addPropertyForm.value)
      .subscribe(
        () => {
          // Reset the form
          this.addPropertyForm.resetForm();
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

