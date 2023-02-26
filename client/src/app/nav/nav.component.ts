import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public accountService : AccountService) {}

  public ngOnInit(): void {
  }

  public login() {
    if(this.model.login && this.model.login.length > 0) {
      this.accountService.login(this.model).subscribe(
        {
          next: response => {console.log(response);},
          error: err => {console.log(err);},
          complete: () => console.log("Completed")
        }
      )
    }else{
      console.log("Please provide username & password")
    }
  }
  
  public logout() {
    this.accountService.logout()
  }
}
