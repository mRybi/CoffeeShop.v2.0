import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Coffee } from '../_models/coffee';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';


@Injectable()
export class CoffeeService {

constructor(private http: HttpClient) { }
baseUrl = environment.apiUrl;

    getCoffee(id: number): Observable<Coffee> {
       return this.http.get<Coffee>(this.baseUrl + 'coffee/' + id).catch(this.handleError);
    }

    getCoffees() {
        return this.http.get<Coffee[]>(this.baseUrl + 'coffee').catch(this.handleError);
    }


    private handleError(error: any) {

        if (error.status === 400) {
            return Observable.throw(error._body);
        }
        const applicationError = error.headers.get('Application-Error');

        if (applicationError) {
            return Observable.throw(applicationError);
        }

        const serverError = error.json();

        let modelStateError = '';

        if (serverError) {
            for (const key in serverError) {
                if (serverError[key]) {
                    modelStateError += serverError[key] + '\n';
                }
            }
        }

        return Observable.throw(
            modelStateError || 'Server error'
        );

    }
}

