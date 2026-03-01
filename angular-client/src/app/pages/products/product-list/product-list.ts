import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product';
import { ProductDto } from '../../../models/ecommerce.models';

@Component({
  selector: 'app-product-list',
  template: `
    <div *ngFor="let p of products" class="card">
      <h4>{{p.name}}</h4>
      <p>{{p.price | currency}} - Stock: {{p.quantity}}</p>
      <button [routerLink]="['/orders/create', p.id]">Buy</button>
    </div>
  `
})
export class ProductListComponent implements OnInit {
  products: ProductDto[] = [];
  constructor(private service: ProductService) {}
  ngOnInit() { this.service.getProducts().subscribe((data: ProductDto[]) => this.products = data); }
}