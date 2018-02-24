import { Component } from '@angular/core';
import { Coffee } from './_models/coffee';
import { OnInit } from '@angular/core';
import { CoffeeService } from './_services/coffee.service';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token) {
      this.authService.decodedToken = this.jwtHelperService.decodeToken(token);
    }
  }
  constructor(private authService: AuthService, private jwtHelperService: JwtHelperService) {
  }


}
