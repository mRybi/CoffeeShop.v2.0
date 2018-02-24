import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Cart } from '../_models/cart';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs/Observable';
import { Coffee } from '../_models/coffee';
import { RequestOptions, Headers, Response, Http  } from '@angular/http';


@Injectable()
export class CartService {

constructor(private http: HttpClient) { }
baseUrl = environment.apiUrl;
private _headers = new HttpHeaders().set('Content-Type', 'application/json');


    getItems(userId: number) {
        return this.http.get<Cart[]>(this.baseUrl +'users/'+ userId + '/cart');
    }

    getSum(userId: number) {
        return this.http.get<number>(this.baseUrl +'users/'+ userId + '/cart/sum');
    }

    clearCart(userId: number) {
        return this.http.post(this.baseUrl +'users/'+ userId + '/cart/clear', {});
    }

    addItem(userId: number, model: any) {
        return this.http.post(this.baseUrl +'users/'+ userId + '/cart', model, {headers : this._headers});

    }

    deleteItem(userId: number, id: number) {
        return this.http.post(this.baseUrl +'users/'+ userId + '/cart/delete/' + id, {});
    }

}
