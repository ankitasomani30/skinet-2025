import {nanoid} from 'nanoid';

export interface ICart {
    id : string;
    items : CartItem[];
}

export interface CartItem {
    productId : number;
    productName : string;
    price : number;
    quantity : number;
    pictureUrl : string;
    brand : string;
    type : string;
}

export class Cart implements ICart {
    id = nanoid();
    items: CartItem[] = [];
}