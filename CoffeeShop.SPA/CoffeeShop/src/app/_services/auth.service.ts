import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { User } from '../_models/user';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthUser } from '../_models/AuthUser';
import 'rxjs/add/operator/map';


@Injectable()
export class AuthService {
    baseUrl = environment.apiUrl;
    private _headers = new HttpHeaders().set('Content-Type', 'application/json');
    userToken: any;
    decodedToken: any;
    currentUser: User;

constructor(private http: HttpClient, private jwtHelperService: JwtHelperService) { }

register(body: any) {
    return this.http.post(this.baseUrl + "auth/register", body, {headers: this._headers});
}

login(model: any) {
    return this.http.post<AuthUser>(this.baseUrl + 'auth/login', model, {headers: this._headers})
    .map(user => {
        if (user && user.tokenString) {
            localStorage.setItem('token', user.tokenString);
            localStorage.setItem('user', JSON.stringify(user.user));
            this.decodedToken = this.jwtHelperService.decodeToken(user.tokenString);
            this.currentUser = user.user;
            this.userToken = user.tokenString;   
        }
    });
}

loggedIn() {
    const token = this.jwtHelperService.tokenGetter();

    if (!token) {
        return false;
    }

    return !this.jwtHelperService.isTokenExpired(token);
}


public getToken(): string {
    return localStorage.getItem('token');
  }
}