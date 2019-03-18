import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {User} from '../../models/user';
import {Observable} from 'rxjs';
import {Share} from '../../models/share';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  // Replace with environment variable
  apiUrl = 'http://35.246.254.34/api/users';

  constructor(private http: HttpClient) {
  }

  public registerUser(user: User) {
    return this.http.post(this.apiUrl, user);
  }

  public updateUser(user: User) {
    return this.http.put(this.apiUrl, user);
  }

  public getUserById(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`);
  }

  public getUsers(url?: string): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  public getSharesForUser(id: number) {
    const shares: Share[] = [];
    this.http.get<User>(`${this.apiUrl}/${id}`).subscribe(res => res.shares.forEach((model, index) => {
      shares.push(model);
    }));

    return shares;
  }
}