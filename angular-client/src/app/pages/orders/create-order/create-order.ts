
import { Component } from '@angular/core';
import { OrderService } from '../../services/order';
import { ActivatedRoute, Router } from '@angular/router';
// create-order.component.ts
@Component({
  template: `
    <input [(ngModel)]="customerName" placeholder="Customer Name">
    <button (click)="placeOrder()">Submit Order</button>
  `
})
export class CreateOrderPage {
  customerName = '';
  productId = ''; // Get from ActivatedRoute params

  constructor(private service: OrderService, private route: ActivatedRoute, private router: Router) {
    this.productId = this.route.snapshot.params['id'];
  }

  placeOrder() {
    const order = { customerName: this.customerName, items: [{ productId: this.productId, quantity: 1 }] };
    this.service.createOrder(order).subscribe((res: any) => this.router.navigate(['/orders', res.id]));
  }
}