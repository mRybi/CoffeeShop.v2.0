import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Coffee } from '../_models/coffee';
import { CoffeeService } from '../_services/coffee.service';
import { AlertifyService } from '../_services/alertify.service';


@Component({
  selector: 'app-menu-detail',
  templateUrl: './menu-detail.component.html',
  styleUrls: ['./menu-detail.component.css']
})
export class MenuDetailComponent implements OnInit {
  coffee: Coffee;
  constructor(private router: ActivatedRoute,
              private coffeeService: CoffeeService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.getCoffee();
  }

  getCoffee() {
    this.coffeeService.getCoffee(+this.router.snapshot.params['id']).subscribe(response => {
      this.coffee = response;
    }, error => {
      this.alertify.error('There was a problem retrivng data about this coffee');
    });
  }

}
