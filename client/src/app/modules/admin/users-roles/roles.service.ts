import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments';
import { Observable, ReplaySubject, tap } from 'rxjs';
import { CreateRoleDto, ResultDeleteRole, ResultRolesDto, ResultRolesListDto, UpdateRoleDto } from './roles.types';

@Injectable({
  providedIn: 'root'
})

export class RolesService {

  private _roles: ReplaySubject<ResultRolesListDto> = new ReplaySubject<ResultRolesListDto>();

  constructor(private _httpClient: HttpClient) { }

  set roles(value: ResultRolesListDto)
  {
    this._roles.next(value)
  }

  get roles$(): Observable<ResultRolesListDto>
  {
     return this._roles.asObservable();
  }

  getAllRoles(skip: Number, limit: Number, sort: string): Observable<ResultRolesListDto>
  {
    return this._httpClient.get<ResultRolesListDto>(environment.API_HOST + `api/roles/getall?skip=${skip}&limit=${limit}&sort=${sort}`).pipe(
      tap((result) => {
        this._roles.next(result)
      })
    );
  }

  createRole(data: CreateRoleDto): Observable<ResultRolesListDto>
  {
    return this._httpClient.post<ResultRolesListDto>(environment.API_HOST + "api/roles/add", data).pipe(
      tap((result) => {
      })
    );
  }

  updateRole(data: UpdateRoleDto): Observable<ResultRolesListDto>
  {
    return this._httpClient.put<ResultRolesListDto>(environment.API_HOST + "api/roles/edit/"+data.id, data).pipe(
      tap((result) => {
      })
    );
  }

  deleteRole(id: number): Observable<ResultDeleteRole>
  {
    return this._httpClient.delete<ResultDeleteRole>(environment.API_HOST + "api/roles/delete/"+id).pipe(
      tap((result) => {
      })
    );
  }

}
