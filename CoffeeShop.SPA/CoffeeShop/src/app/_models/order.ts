import { Cart } from './cart';

export interface Order {
    email: string;
    name: string;
    surname: string;
    country: string;
    city: string;
    street: string;
    houseNumber: number;
    phoneNumber: number;
    //carts: Cart[];
}
