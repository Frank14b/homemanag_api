import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments';
import { Observable, ReplaySubject, tap } from 'rxjs';
import { CreateAccesDto, ResultDeleteAcces, ResultAccessDto, ResultAccessListDto, UpdateAccesDto } from './access.types';

@Injectable({
  providedIn: 'root'
})

export class AccessService {

  private _users: ReplaySubject<ResultAccessDto> = new ReplaySubject<ResultAccessDto>();

  constructor(private _httpClient: HttpClient) { }

  set users(value: ResultAccessDto)
  {
    this._users.next(value)
  }

  get users$(): Observable<ResultAccessDto>
  {
     return this._users.asObservable();
  }

  getAllAccess(skip: Number, limit: Number, sort: string): Observable<ResultAccessDto>
  {
    return this._httpClient.get<ResultAccessDto>(environment.API_HOST + `api/access/getall?skip=${skip}&limit=${limit}&sort=${sort}`).pipe(
      tap((result) => {
        this._users.next(result)
      })
    );
  }

  createAcces(data: CreateAccesDto): Observable<ResultAccessListDto>
  {
    return this._httpClient.post<ResultAccessListDto>(environment.API_HOST + "api/access/add", data).pipe(
      tap((result) => {
      })
    );
  }

  updateAcces(data: UpdateAccesDto): Observable<ResultAccessListDto>
  {
    return this._httpClient.put<ResultAccessListDto>(environment.API_HOST + "api/access/edit", data).pipe(
      tap((result) => {
      })
    );
  }

  deleteAcces(id: number): Observable<ResultDeleteAcces>
  {
    return this._httpClient.delete<ResultDeleteAcces>(environment.API_HOST + "api/users/delete/"+id).pipe(
      tap((result) => {
      })
    );
  }

  searchAccesByEmailOrUniqueId(keyword: string): Observable<ResultAccessListDto>
  {
    return this._httpClient.get<ResultAccessListDto>(environment.API_HOST + `api/access/search-join?keyword=${keyword}`).pipe(
      tap((result) => {
      })
    );
  }

}
