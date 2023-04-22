import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments';
import { Observable, ReplaySubject, tap } from 'rxjs';
import { CreateTypesDto, ResultDeleteDto, ResultListDto, UpdateTypesDto } from './property-types.types';

@Injectable({
  providedIn: 'root'
})

export class PropertyTypesService {

  private _properties: ReplaySubject<ResultListDto> = new ReplaySubject<ResultListDto>();

  constructor(private _httpClient: HttpClient) { }

  set properties(value: ResultListDto)
  {
    this._properties.next(value)
  }

  get properties$(): Observable<ResultListDto>
  {
     return this._properties.asObservable();
  }

  getAllPropertyTypes(skip: Number, limit: Number, sort: string): Observable<ResultListDto>
  {
    return this._httpClient.get<ResultListDto>(environment.API_HOST + `api/propertytypes/getall?skip=${skip}&limit=${limit}&sort=${sort}`).pipe(
      tap((result) => {
        this._properties.next(result)
      })
    );
  }

  createType(data: CreateTypesDto): Observable<ResultListDto>
  {
    return this._httpClient.post<ResultListDto>(environment.API_HOST + "api/propertytypes/add", data).pipe(
      tap((result) => {
      })
    );
  }

  updateType(data: UpdateTypesDto): Observable<ResultListDto>
  {
    return this._httpClient.put<ResultListDto>(environment.API_HOST + "api/propertytypes/edit", data).pipe(
      tap((result) => {
      })
    );
  }

  deleteType(id: number): Observable<ResultDeleteDto>
  {
    return this._httpClient.delete<ResultDeleteDto>(environment.API_HOST + "api/propertytypes/delete/"+id).pipe(
      tap((result) => {
      })
    );
  }
}
