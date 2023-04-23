import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments';
import { Observable, ReplaySubject, tap } from 'rxjs';
import { CreateBusinessDto, ResultBusinessListDto, ResultDeleteDto, UpdateBusinessDto } from './business.types';

@Injectable({
  providedIn: 'root'
})

export class BusinessService {

  private _business: ReplaySubject<ResultBusinessListDto> = new ReplaySubject<ResultBusinessListDto>();

  constructor(private _httpClient: HttpClient) { }

  set business(value: ResultBusinessListDto)
  {
    this._business.next(value)
  }

  get business$(): Observable<ResultBusinessListDto>
  {
     return this._business.asObservable();
  }

  getAllBusiness(skip: Number, limit: Number, sort: string): Observable<ResultBusinessListDto>
  {
    return this._httpClient.get<ResultBusinessListDto>(environment.API_HOST + `api/business/getall?skip=${skip}&limit=${limit}&sort=${sort}`).pipe(
      tap((result) => {
        this._business.next(result)
      })
    );
  }

  createBusiness(data: CreateBusinessDto): Observable<ResultBusinessListDto>
  {
    return this._httpClient.post<ResultBusinessListDto>(environment.API_HOST + "api/business/add", data)
  }

  updateBusiness(data: UpdateBusinessDto, id: number): Observable<ResultBusinessListDto>
  {
    return this._httpClient.put<ResultBusinessListDto>(environment.API_HOST + "api/business/update/"+id, data)
  }

  updateStatus(id: number): Observable<ResultBusinessListDto>
  {
    return this._httpClient.put<ResultBusinessListDto>(environment.API_HOST + "api/business/status/"+id, {})
  }

  deleteBusiness(id: number): Observable<ResultDeleteDto>
  {
    return this._httpClient.delete<ResultDeleteDto>(environment.API_HOST + "api/business/delete/"+id)
  }
}
