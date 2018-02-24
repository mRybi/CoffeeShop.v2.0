import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { Order } from '../_models/order';

@Injectable()
export class OrderService {

constructor(private http: HttpClient) { }
baseUrl = environment.apiUrl;
private _headers = new HttpHeaders().set('Content-Type', 'application/json');

addOrder(userId: number, order: Order) {
   return this.http.post(this.baseUrl +'users/'+ {userId} + '/order', order, {headers : this._headers});
}

}
