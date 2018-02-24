import { Injectable } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';
import { Coffee } from '../_models/coffee';
import { CoffeeService } from '../_services/coffee.service';

@Injectable()
export class MenuResolver implements Resolve<Coffee[]> {

    constructor( private coffeeService: CoffeeService,
        private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Coffee[]> {
        return this.coffeeService.getCoffees().catch(error => {
            this.alertify.error('Problem retriving data');
            this.router.navigate(['/home']);
            return Observable.of(null);
        });
    }

}
