import { Component, OnInit } from '@angular/core';
import { Coffee } from '../_models/coffee';
import { CoffeeService } from '../_services/coffee.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  coffees: Coffee[];
  coffee: Coffee;
  user: any;

  constructor(private coffeeService: CoffeeService,
    private alertify: AlertifyService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.coffees = data['coffees'];
    });
     // this.getCoffees();
    // this.getCoffee(1);
  }

  getCoffee(id: number) {
    this.coffeeService.getCoffee(id).subscribe(response => {
      this.coffee = response;
    });
  }

  getCoffees() {
    this.coffeeService.getCoffees().subscribe(reponse => {
      this.coffees = reponse;
    }
    );
  }
}
