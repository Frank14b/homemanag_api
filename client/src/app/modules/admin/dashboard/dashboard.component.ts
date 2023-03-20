import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'app/core/user/user.service';
import { User } from 'app/core/user/user.types';
import { map } from 'lodash';
import { Subject, takeUntil } from 'rxjs';
import { TotalData } from './dashbaord.types';
import { DashboardService } from './dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent
  {
    public user:User;
    public totalData:TotalData;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _userSservice: UserService,
        private _dashbaordService: DashboardService,
        private _router: Router
    )
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
      // Subscribe to the user service
      this._userSservice.user$
      .pipe((takeUntil(this._unsubscribeAll)))
      .subscribe((user: User) => {
          this.user = user;
      });

      this._dashbaordService.totalData$
      .pipe((takeUntil(this._unsubscribeAll)))
      .subscribe((total: TotalData) => {
        this.totalData = total
      })
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}

