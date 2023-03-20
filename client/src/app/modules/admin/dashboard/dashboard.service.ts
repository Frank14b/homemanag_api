import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, ReplaySubject, tap } from 'rxjs';
import { TotalData } from './dashbaord.types';
import { environment } from 'environments';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private _totalData: ReplaySubject<TotalData> = new ReplaySubject<TotalData>(0);

  constructor(private _httpClient: HttpClient) { }

  /**
  * Setter & getter
  *
  * @param value
  */
  set totalData(value: TotalData) {
    // Store the value
    this._totalData.next(value);
  }

  get totalData$(): Observable<TotalData> {
    return this._totalData.asObservable();
  }

  get(): Observable<TotalData> {
    return this._httpClient.get<TotalData>(environment.API_HOST + 'api/dashboard/total').pipe(
      tap((totals) => {
        this._totalData.next(totals);
      })
    );
  }
}
