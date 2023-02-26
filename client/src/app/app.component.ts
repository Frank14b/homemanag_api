import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'Home Managenent';
  users: any

  constructor(private http: HttpClient, private accountService: AccountService) {

  }

  public ngOnInit(): void {
    this.http.get("http://localhost:5263/api/users").subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log("completed")
    })

    this.setCurrentUser()
  }

  public setCurrentUser() {
    let userString:any = localStorage.getItem("user")
    if(userString != null) {
      const user:User = JSON.parse(userString)
      this.accountService.setCurrentUser(user)
    }
  }

}
