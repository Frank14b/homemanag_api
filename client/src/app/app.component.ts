import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'Home Managenent';
  users: any

  constructor(private http: HttpClient) {

  }

  ngOnInit(): void {
    this.http.get("http://localhost:5263/api/users").subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log("completed")
    })
  }

}
