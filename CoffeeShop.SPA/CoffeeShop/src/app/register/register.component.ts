import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';
import { Route, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  userToRegister: any;

  constructor(private alertify: AlertifyService,
              private fb: FormBuilder,
              private router: Router,
              private authService: AuthService) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  register() {
    if (this.registerForm.valid) {
      this.userToRegister = Object.assign({}, this.registerForm.value);

      this.authService.register(this.userToRegister).subscribe(response => {
        this.alertify.success('Registration successfull');
        this.router.navigateByUrl('/menu');
      }, error => {
        this.alertify.error(error);
      });
    }

  }

}
