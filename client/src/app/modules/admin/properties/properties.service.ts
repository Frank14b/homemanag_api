import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments';
import { Observable, ReplaySubject, tap } from 'rxjs';
import { CreatePropertyDto, ResultDeleteProperties, ResultPropertiesDto, UpdatePropertyDto } from './properties.types';

@Injectable({
  providedIn: 'root'
})
export class PropertiesService {

  private _properties: ReplaySubject<ResultPropertiesDto> = new ReplaySubject<ResultPropertiesDto>();

  constructor(private _httpClient: HttpClient) { }

  set properties(value: ResultPropertiesDto)
  {
    this._properties.next(value)
  }

  get properties$(): Observable<ResultPropertiesDto>
  {
     return this._properties.asObservable();
  }

  getAllProperties(skip: Number, limit: Number, sort: string): Observable<ResultPropertiesDto>
  {
    return this._httpClient.get<ResultPropertiesDto>(environment.API_HOST + `api/properties/getall?skip=${skip}&limit=${limit}&sort=${sort}`).pipe(
      tap((result) => {
        this._properties.next(result)
      })
    );
  }

  createProperty(data: CreatePropertyDto): Observable<ResultPropertiesDto>
  {
    return this._httpClient.post<ResultPropertiesDto>(environment.API_HOST + "api/properties/add", data).pipe(
      tap((result) => {
      })
    );
  }

  updateProperty(data: UpdatePropertyDto): Observable<ResultPropertiesDto>
  {
    return this._httpClient.put<ResultPropertiesDto>(environment.API_HOST + "api/properties/edit", data).pipe(
      tap((result) => {
      })
    );
  }

  deleteProperty(id: number): Observable<ResultDeleteProperties>
  {
    return this._httpClient.delete<ResultDeleteProperties>(environment.API_HOST + "api/properties/delete/"+id).pipe(
      tap((result) => {
      })
    );
  }
}
