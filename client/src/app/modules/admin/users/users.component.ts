import { Component, ViewChild } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { AddFormComponent } from './add-form/add-form.component';
import { UsersService } from './users.service';
import { ResultDeleteUser, ResultUsersDto, ResultUsersListDto } from './users.types';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent {

  searchInputControl: UntypedFormControl = new UntypedFormControl();
  isLoading: boolean = false;
  allUsers: ResultUsersDto;

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
    public _usersServices: UsersService
  ) { }

  createUser(): void {
    const dialogRef = this.dialog.open(AddFormComponent, {
      data: {
        currentList: this.allUsers,
        defaultType: null
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      // this.getAllTypes()
    });
  }

  editUser(_data: ResultUsersListDto): void {
    const dialogRef = this.dialog.open(AddFormComponent, {
      data: {
        currentList: this.allUsers,
        defaultProperty: _data
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      // this.getAllTypes()
    });
  }

  deleteUser(id: number): void {
    this.isLoading = true
    this._usersServices.deleteUser(id).pipe(
      // map((data: ResultDeleteUser) => {
      //   // this.getAllTypes() 
      //   return data
      // })
    ).subscribe(
      (result: ResultDeleteUser) => {
        this.isLoading = false
        if(result.status){
          
        }
      }
    )
  }
}
