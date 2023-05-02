import { Component, OnInit, ViewChild } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { AddFormComponent } from './add-form/add-form.component';
import { RolesService } from './roles.service';
import { ResultDeleteRole, ResultRolesDto, ResultRolesListDto } from './roles.types';

@Component({
  selector: 'app-users-roles',
  templateUrl: './users-roles.component.html',
  styleUrls: ['./users-roles.component.scss']
})
export class UsersRolesComponent implements OnInit {
  searchInputControl: UntypedFormControl = new UntypedFormControl();
  isLoading: boolean = false;
  allRoles: ResultRolesListDto;

  displayedColumns: string[] = ['id', 'details', 'name', 'status', 'createdAt', 'action'];

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
    public _rolesServices: RolesService
  ) { }

  ngOnInit(): void {
    this.getAllRoles()
  }

  createRole(): void {
    const dialogRef = this.dialog.open(AddFormComponent, {
      data: {
        currentList: [],
        defaultRoles: null
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      this.getAllRoles()
    });
  }

  editRole(_data: ResultRolesListDto): void {
    const dialogRef = this.dialog.open(AddFormComponent, {
      data: {
        currentList: [],
        defaultRoles: _data
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      this.getAllRoles()
    });
  }

  deleteRole(id: number): void {
    this.isLoading = true
    this._rolesServices.deleteRole(id).pipe(
      // map((data: ResultDeleteRole) => {
        // this.getAllRoles() 
      //   return data
      // })
    ).subscribe(
      (result: ResultDeleteRole) => {
        this.isLoading = false
        if(result.status){
          this.getAllRoles() 
        }
      }
    )
  }

  getAllRoles(): void {
    this.isLoading = true
    this._rolesServices.getAllRoles(0, 1000, "asc").pipe(

    ).subscribe(
      (result: ResultRolesListDto) => {
        this.isLoading = false;
        this.allRoles = result;
      }
    )
  }
}

