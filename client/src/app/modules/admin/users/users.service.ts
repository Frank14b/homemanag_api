import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments';
import { Observable, ReplaySubject, tap } from 'rxjs';
import { CreateUserDto, ResultDeleteUser, ResultUsersDto, ResultUsersListDto, UpdateUserDto } from './users.types';

@Injectable({
  providedIn: 'root'
})

export class UsersService {

  private _users: ReplaySubject<ResultUsersDto> = new ReplaySubject<ResultUsersDto>();

  constructor(private _httpClient: HttpClient) { }

  set users(value: ResultUsersDto)
  {
    this._users.next(value)
  }

  get users$(): Observable<ResultUsersDto>
  {
     return this._users.asObservable();
  }

  getAllUsers(skip: Number, limit: Number, sort: string): Observable<ResultUsersDto>
  {
    return this._httpClient.get<ResultUsersDto>(environment.API_HOST + `api/users/getall?skip=${skip}&limit=${limit}&sort=${sort}`).pipe(
      tap((result) => {
        this._users.next(result)
      })
    );
  }

  createUser(data: CreateUserDto): Observable<ResultUsersListDto>
  {
    return this._httpClient.post<ResultUsersListDto>(environment.API_HOST + "api/users/add", data).pipe(
      tap((result) => {
      })
    );
  }

  updateUser(data: UpdateUserDto): Observable<ResultUsersListDto>
  {
    return this._httpClient.put<ResultUsersListDto>(environment.API_HOST + "api/users/edit", data).pipe(
      tap((result) => {
      })
    );
  }

  deleteUser(id: number): Observable<ResultDeleteUser>
  {
    return this._httpClient.delete<ResultDeleteUser>(environment.API_HOST + "api/users/delete/"+id).pipe(
      tap((result) => {
      })
    );
  }
}
