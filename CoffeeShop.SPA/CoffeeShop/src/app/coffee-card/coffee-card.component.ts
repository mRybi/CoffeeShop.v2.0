import { Component, OnInit, Input } from '@angular/core';
import { Coffee } from '../_models/coffee';
import { CartService } from '../_services/cart.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-coffee-card',
  templateUrl: './coffee-card.component.html',
  styleUrls: ['./coffee-card.component.css']
})
export class CoffeeCardComponent implements OnInit {
 @Input() coffee: Coffee;
 model: any = {};

  constructor(private cartService: CartService,
              private alertify: AlertifyService,
              private authService: AuthService) { }

  ngOnInit() {
    this.isLoggedIn();
  }

  isLoggedIn() {
    return this.authService.loggedIn();
  }

  addToCart() {
    this.model.idItem = this.coffee.id;
    this.cartService.addItem(this.authService.decodedToken.nameid, this.model).subscribe(response => {
      this.alertify.success('You have added item to cart');
    }, error => {
      this.alertify.error('You have to be loged in.');
    });
  }
}
