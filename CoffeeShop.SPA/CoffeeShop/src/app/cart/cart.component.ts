import { Component, OnInit, AfterViewChecked } from '@angular/core';
import { CartService } from '../_services/cart.service';
import { Cart } from '../_models/cart';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit, AfterViewChecked {
  cartCoffees: Cart[];
  sum: number;

  constructor(private cartService: CartService, private router: Router,
    private aleritfy: AlertifyService, private route: ActivatedRoute,
    private authService: AuthService) { }

  ngAfterViewChecked() {
    // this.route.data.subscribe(data => {
    //   this.cartCoffees = data['coffeesInCart'];
    // });
    //   this.getSum();
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.cartCoffees = data['coffeesInCart'];
    });
    // this.getItemsFromCart();
     this.getSum();
  }

  getSum() {
    this.cartService.getSum(this.authService.decodedToken.nameid).subscribe(response => {
      this.sum = response;
    }, error => {
      this.aleritfy.error(error);
    });
  }

  clearCart() {
    this.cartService.clearCart(this.authService.decodedToken.nameid).subscribe(response => {
      this.aleritfy.success('you have deleted your cart');
    }, error => {
      this.aleritfy.error(error);
    });
  }

  getItemsFromCart() {
    this.cartService.getItems(this.authService.decodedToken.nameid).subscribe(response => {
      this.cartCoffees = response;
    }, error => {
      this.aleritfy.error(error);
    });
  }

  deleteItem(itemId: number, coffeeName: string) {
    this.cartService.deleteItem(this.authService.decodedToken.nameid,itemId).subscribe(response => {
      this.aleritfy.success('You have deleted: ' + coffeeName);
      // this.router.navigate(['/menu']);
      // location.reload();
      // this.router.navigate(['/cart']);
      this.getItemsFromCart();
      this.getSum();
    }, error => {
      this.aleritfy.error(error);
    });

  }
}

