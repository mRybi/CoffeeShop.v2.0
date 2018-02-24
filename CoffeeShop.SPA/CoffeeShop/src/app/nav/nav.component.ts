import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';



@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private router: Router,
    private authService: AuthService,
    private alertify: AlertifyService) { }

  ngOnInit() {

  }

  login() {
    this.authService.login(this.model).subscribe(response => {
      this.alertify.success('Logged in successfully');
    }, error => {
      this.alertify.error('Failed to login');
    }, () => {
      this.router.navigate(['/menu']);
    });
  }

  logout() {
    this.authService.userToken = null;
    this.authService.currentUser = null;
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.alertify.warning('Logged out');
    this.router.navigate(['/home']);
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
}
