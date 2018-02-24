import { Injectable } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';
import { Cart } from '../_models/cart';
import { CartService } from '../_services/cart.service';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class CartResolver implements Resolve<Cart[]> {

    constructor( private cartService: CartService,
        private router: Router,
        private alertify: AlertifyService,
        private authService: AuthService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Cart[]> {
        return this.cartService.getItems(this.authService.decodedToken.nameid).catch(error => {
            this.alertify.error('Problem retriving data');
            this.router.navigate(['/menu']);
            return Observable.of(null);
        });
    }

}
