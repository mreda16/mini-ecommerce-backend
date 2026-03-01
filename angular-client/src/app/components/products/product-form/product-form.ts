

// product-form.component.ts
import { Component, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-product-form',
  template: `
    <form (submit)="submit()">
      <input [(ngModel)]="model.name" name="name" placeholder="Name" class="form-control">
      <input [(ngModel)]="model.price" name="price" type="number" class="form-control">
      <input [(ngModel)]="model.quantity" name="qty" type="number" class="form-control">
      <button type="submit" class="btn btn-primary">Create</button>
    </form>
  `
})
export class ProductFormComponent {
  model = { name: '', price: 0, quantity: 0 };
  @Output() created = new EventEmitter();

  constructor(private service: ProductService) {}

  submit() {
    this.service.createProduct(this.model).subscribe(() => this.created.emit());
  }
}