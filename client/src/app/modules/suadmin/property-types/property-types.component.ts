import { Component, ViewEncapsulation } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AddPropertyTypesFormComponent } from './add-property-types-form/add-property-types-form.component';
import { PropertyTypesService } from './property-types.service';
import { ResultDeleteDto, ResultListDto, ResultTypeDto } from './property-types.types';

import { ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, SortDirection } from '@angular/material/sort';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-property-types',
  templateUrl: './property-types.component.html',
  styleUrls: ['./property-types.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class PropertyTypesComponent {

  searchInputControl: UntypedFormControl = new UntypedFormControl();
  isLoading: boolean = false;
  allPropertiesTypes: ResultTypeDto;

  displayedColumns: string[] = ['id', 'subtype', 'name', 'status', 'createdAt', 'action'];

  itemPerPage = 30;
  initialSkip = 0;
  initialLimit = this.itemPerPage
  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    public dialog: MatDialog,
    public _propertyTypeServices: PropertyTypesService
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    this.isLoading = true

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this._propertyTypeServices!.getAllPropertyTypes(
            this.initialSkip,
            this.initialLimit,
            "desc"
          ).pipe(catchError(() => observableOf(null)));
        }),
        map((data: ResultListDto) => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
          this.isRateLimitReached = data.data === null;

          if (this.initialLimit >= data.total) {
            this.initialSkip = 0
            this.initialLimit = this.itemPerPage
          } else {
            this.initialSkip = data.limit
            this.initialLimit = data.limit + this.itemPerPage
          }

          if (data.data === null) {
            return [];
          }

          this.resultsLength = data.total;
          return data;
        }),
      )
      .subscribe((data: ResultListDto) => {
        this.allPropertiesTypes = data.data;
        this.isLoading = false
      });
  }

  getAllTypes(): void {
    this.isLoading = true
    this._propertyTypeServices.getAllPropertyTypes(0, this.initialLimit, "desc").pipe(
      map((data: ResultListDto) => {
        this.isLoadingResults = false;
        this.isRateLimitReached = data === null;

        if (data.data === null) {
          return [];
        }
        return data;
      })
    ).subscribe((result: ResultListDto) => {
      this.initialSkip = 0;
      this.initialLimit = this.itemPerPage;
      this.resultsLength = result.total;
      this.allPropertiesTypes = result.data
      this.resultsLength = result.total
      this.isLoading = false
    })
  }

  createProduct(): void {
    const dialogRef = this.dialog.open(AddPropertyTypesFormComponent, {
      data: {
        currentList: this.allPropertiesTypes,
        defaultType: null
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      this.getAllTypes()
    });
  }

  editType(_data: ResultTypeDto): void {
    const dialogRef = this.dialog.open(AddPropertyTypesFormComponent, {
      data: {
        currentList: this.allPropertiesTypes,
        defaultType: _data
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      this.getAllTypes()
    });
  }

  deletetype(id: number): void {
    this.isLoading = true
    this._propertyTypeServices.deleteType(id).pipe(
      map((data: ResultDeleteDto) => {
        this.getAllTypes() 
        return data
      })
    ).subscribe(
      (result: ResultDeleteDto) => {
        this.isLoading = false
        if(result.status){
          
        }
      }
    )
  }
}