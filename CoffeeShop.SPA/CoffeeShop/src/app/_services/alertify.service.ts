import { Injectable } from '@angular/core';
declare let alertify: any;

@Injectable()
export class AlertifyService {
value: number;
constructor() { }

confirm(message: string, okCallback: () => any ) {
    alertify.confirm(message, function(e) {
        if (e) {
            okCallback();
        } else {

        }
    });
}

success(message: string) {
    alertify.success(message);
}

error(message: string) {
    alertify.error(message);
}

warning(message: string) {
    alertify.message(message);
}

}
