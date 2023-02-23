import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  model: any = {};
  loggedIn: boolean = false;

  constructor(private accountService : AccountService) {}

  public ngOnInit(): void {

  }

  public login() {
    if(this.model.login && this.model.login.length > 0) {
      this.accountService.login(this.model).subscribe(
        {
          next: response => {console.log(response); this.loggedIn = true},
          error: err => {console.log(err); this.loggedIn = false},
          complete: () => console.log("Completed")
        }
      )
    }else{
      console.log("Please provide username & password")
    }
  }
  
  public logout() {
    this.accountService.logout()
    this.loggedIn = false
  }
}
