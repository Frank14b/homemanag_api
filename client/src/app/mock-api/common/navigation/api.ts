import { Injectable } from '@angular/core';
import { cloneDeep } from 'lodash-es';
import { FuseNavigationItem } from '@fuse/components/navigation';
import { FuseMockApiService } from '@fuse/lib/mock-api';
import { defaultAdminNavigation, defaultNavigation } from 'app/mock-api/common/navigation/data';
import { AuthService } from 'app/core/auth/auth.service';

@Injectable({
    providedIn: 'root'
})
export class NavigationMockApi {
    private readonly _defaultNavigation: FuseNavigationItem[] = defaultNavigation;

    private readonly _defaultAdminNavigation: FuseNavigationItem[] = defaultAdminNavigation;

    /**
     * Constructor
     */
    constructor(private _fuseMockApiService: FuseMockApiService, private _authService: AuthService) {
        // Register Mock API handlers

        this.registerHandlers();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Register Mock API handlers
     */
    registerHandlers(): void {
        // -----------------------------------------------------------------------------------------------------
        // @ Navigation - GET
        // -----------------------------------------------------------------------------------------------------
        this._fuseMockApiService
            .onGet('api/common/navigation')
            .reply(() => {

                // Return the response
                return [
                    200,
                    {
                        default: cloneDeep(this._defaultNavigation),
                    }
                ];
            });

        // -----------------------------------------------------------------------------------------------------
        // @ Navigation - GET Admin
        // -----------------------------------------------------------------------------------------------------
        this._fuseMockApiService
            .onGet('api/common/admin/navigation')
            .reply(() => {

                // Return the response
                return [
                    200,
                    {
                        default: cloneDeep(this._defaultAdminNavigation),
                    }
                ];
            });
    }
}
