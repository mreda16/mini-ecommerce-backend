import { Component, OnInit } from '@angular/core';
import { OrderDetailsDto } from '../models/models';
import { OrderService } from '../../core/services/OrdersService';
import { ActivatedRoute } from '@angular/router';
// order-details.component.ts
@Component({
  template: `
    <div *ngIf="order">
      <h3>Order for {{order.customerName}}</h3>
      <p>Discount: {{order.discount | currency}}</p>
      <p><strong>Total: {{order.total | currency}}</strong></p>
    </div>
  `
})
export class OrderDetailsPage implements OnInit {
  order?: OrderDetailsDto;
  constructor(private service: OrderService, private route: ActivatedRoute) {}
  ngOnInit() {
    const id = this.route.snapshot.params['id'];
    this.service.getOrderById(id).subscribe((data: OrderDetailsDto) => this.order = data);
  }
}