import { Coffee } from './coffee';

export interface Cart {
    id: number;
    name: string;
    price: number;
    itemsQuantity: number;
    coffee: Coffee;
}
