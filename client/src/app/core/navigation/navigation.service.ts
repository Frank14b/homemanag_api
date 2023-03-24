import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, ReplaySubject, switchMap, tap } from 'rxjs';
import { Navigation } from 'app/core/navigation/navigation.types';
import { AuthService } from '../auth/auth.service';
import { map } from 'lodash';

@Injectable({
    providedIn: 'root'
})
export class NavigationService
{
    private _navigation: ReplaySubject<Navigation> = new ReplaySubject<Navigation>(1);

    /**
     * Constructor
     */
    constructor(private _httpClient: HttpClient, private _authService: AuthService)
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for navigation
     */
    get navigation$(): Observable<Navigation>
    {
        return this._navigation.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get all navigation data
     */
    get(): Observable<Navigation>
    {
        var userrole = this._authService.userRole
        
        if(userrole == "admin") {
            return this._httpClient.get<Navigation>('api/common/admin/navigation').pipe(
                tap((navigation) => {
                    this._navigation.next(navigation);
                })
            );
        }else{
            return this._httpClient.get<Navigation>('api/common/navigation').pipe(
                tap((navigation) => {
                    this._navigation.next(navigation);
                })
            );
        }
        
    }
}
