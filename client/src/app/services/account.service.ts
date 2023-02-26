import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private baseurl = "http://localhost:5263/api/";

  private currentUserSource = new BehaviorSubject<User | null>(null);
  public currentUser$ = this .currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  public login(data: any) {
    return this.http.post<User>(this.baseurl + "Users/login", data).pipe(
      map((response:User) => {
        const user = response
        if(user) {
          localStorage.setItem("user", JSON.stringify(user))
          this.currentUserSource.next(user)
        }
      })
    )
  }

  public setCurrentUser(user: User)
  {
    this.currentUserSource.next(user)
  }

  public logout()
  {
    localStorage.removeItem("user")
    this.currentUserSource.next(null)
  }
}
