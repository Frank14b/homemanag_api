import { Component, ViewChild } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { AccessService } from './access.service';
import { ResultAccessDto, ResultAccessListDto, ResultDeleteAcces } from './access.types';
import { AddFormComponent } from './add-form/add-form.component';

@Component({
  selector: 'app-roles-access',
  templateUrl: './roles-access.component.html',
  styleUrls: ['./roles-access.component.scss']
})
export class RolesAccessComponent {
  searchInputControl: UntypedFormControl = new UntypedFormControl();
  isLoading: boolean = false;
  allAccess: ResultAccessDto;

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
    public _usersServices: AccessService
  ) { }

  createAcces(): void {
    const dialogRef = this.dialog.open(AddFormComponent, {
      data: {
        currentList: this.allAccess,
        defaultType: null
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      // this.getAllTypes()
    });
  }

  editAcces(_data: ResultAccessListDto): void {
    const dialogRef = this.dialog.open(AddFormComponent, {
      data: {
        currentList: this.allAccess,
        defaultProperty: _data
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      // this.getAllTypes()
    });
  }

  deleteAcces(id: number): void {
    this.isLoading = true
    this._usersServices.deleteAcces(id).pipe(
      // map((data: ResultDeleteAcces) => {
      //   // this.getAllTypes() 
      //   return data
      // })
    ).subscribe(
      (result: ResultDeleteAcces) => {
        this.isLoading = false
        if(result.status){
          
        }
      }
    )
  }
}
