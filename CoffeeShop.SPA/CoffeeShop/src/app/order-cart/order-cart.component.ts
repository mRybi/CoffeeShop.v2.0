import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Validators } from '@angular/forms';
import { Order } from '../_models/order';
import { OrderService } from '../_services/order.service';
import { CartService } from '../_services/cart.service';
import { Cart } from '../_models/cart';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-order-cart',
  templateUrl: './order-cart.component.html',
  styleUrls: ['./order-cart.component.css']
})
export class OrderCartComponent implements OnInit {
  cartCoffees: Cart[];
  orderForm: FormGroup;
  order: Order;

  constructor(private alertify: AlertifyService,
  private fb: FormBuilder,
  private router: Router,
  private orderService: OrderService,
  private cartService: CartService,
  private authService: AuthService) { }

  ngOnInit() {
    this.createOrderForm();
    this.getCoffessFromCart();
  }

  createOrderForm() {
    this.orderForm = this.fb.group({
      email: ['', Validators.required],
      name: ['', Validators.required],
      surname: ['', Validators.required],
      country: ['', Validators.required],
      city: ['', Validators.required],
      street: ['', Validators.required],
      houseNumber: [null, Validators.required],
      phoneNumber: [null, Validators.required],
    });
  }

  getCoffessFromCart() {
    this.cartService.getItems(this.authService.decodedToken.nameid).subscribe(response => {
      this.cartCoffees = response;
    }, error => {
      this.alertify.error(error);
    })
  }

  clearCart() {
    this.cartService.clearCart(this.authService.decodedToken.nameid).subscribe(() => {
      this.alertify.success('We have cleared you cart');
    });
  }

  orderCart() {
    if (this.orderForm.valid) {
      this.order = Object.assign({}, this.orderForm.value);
      // this.order.carts = this.cartCoffees;
      this.orderService.addOrder(this.authService.decodedToken.nameid, this.order).subscribe(respond => {
        this.alertify.success('Order Successfuly sent');
        this.router.navigate(['/menu']);
        this.clearCart();
      }, error => {
        this.alertify.error(error);
      });
    }
  }

  cancel() {
    this.router.navigate(['/cart']);
  }
}
