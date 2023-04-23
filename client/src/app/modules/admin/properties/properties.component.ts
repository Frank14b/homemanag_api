import { Component, ViewChild } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { catchError, map, merge, of, startWith, switchMap } from 'rxjs';
import { ResultDeleteProperties, ResultPropertiesDto, ResultPropertiesListDto } from './properties.types';
import { AddFormComponent } from './add-form/add-form.component';
import { PropertiesService } from './properties.service';


@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.scss']
})
export class PropertiesComponent {

  searchInputControl: UntypedFormControl = new UntypedFormControl();
  isLoading: boolean = false;
  allProperties: ResultPropertiesListDto;

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
    public _propertiesServices: PropertiesService
  ) { }

  ngAfterViewInit(): void {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    this.isLoading = true

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this._propertiesServices!.getAllProperties(
            this.initialSkip,
            this.initialLimit,
            "desc"
          ).pipe(catchError(() => of(null)));
        }),
        map((data: ResultPropertiesDto) => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
          this.isRateLimitReached = data?.data === null;

          if (this.initialLimit >= data?.total) {
            this.initialSkip = 0
            this.initialLimit = this.itemPerPage
          } else {
            this.initialSkip = data?.limit
            this.initialLimit = data?.limit + this.itemPerPage
          }

          if (data?.data === null) {
            return [];
          }

          this.resultsLength = data?.total;
          return data;
        }),
      )
      .subscribe((data: ResultPropertiesDto) => {
        // console.log(data)
        this.allProperties = data?.data;
        this.isLoading = false
      });
  }

  getAllTypes(): void {
    this.isLoading = true
    this._propertiesServices.getAllProperties(0, this.initialLimit, "desc").pipe(
      map((data: ResultPropertiesDto) => {
        this.isLoadingResults = false;
        this.isRateLimitReached = data === null;

        if (data.data === null) {
          return [];
        }
        return data;
      })
    ).subscribe((result: ResultPropertiesDto) => {
      this.initialSkip = 0;
      this.initialLimit = this.itemPerPage;
      this.resultsLength = result.total;
      this.allProperties = result.data
      this.resultsLength = result.total
      this.isLoading = false
    })
  }

  createProduct(): void {
    const dialogRef = this.dialog.open(AddFormComponent, {
      data: {
        currentList: this.allProperties,
        defaultType: null
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      this.getAllTypes()
    });
  }

  editProperty(_data: ResultPropertiesListDto): void {
    const dialogRef = this.dialog.open(AddFormComponent, {
      data: {
        currentList: this.allProperties,
        defaultProperty: _data
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      this.getAllTypes()
    });
  }

  deleteProperty(id: number): void {
    this.isLoading = true
    this._propertiesServices.deleteProperty(id).pipe(
      map((data: ResultDeleteProperties) => {
        this.getAllTypes() 
        return data
      })
    ).subscribe(
      (result: ResultDeleteProperties) => {
        this.isLoading = false
        if(result.status){
          
        }
      }
    )
  }
}
