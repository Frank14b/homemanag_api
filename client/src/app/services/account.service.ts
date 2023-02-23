import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseurl = "http://localhost:5263/api/"

  constructor(private http: HttpClient) { }

  public login(data: any) {
    return this.http.post<User>(this.baseurl + "Users/login", data).pipe(
      map((response:User) => {
        const user = response
        if(user) {
          localStorage.setItem("user", JSON.stringify(user))
        }
      })
    )
  }

  public logout()
  {
    localStorage.removeItem("user")
  }
}
