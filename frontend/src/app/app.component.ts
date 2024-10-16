import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CustomerDto } from '../dtos/CustomerDto';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule, MatCardModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'Customers';
  url = 'http://localhost:5219';
  customers: CustomerDto[] = [];
  isNewRecord = true;
  customerId?: number;
  formGroup = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    phoneNumber: new FormControl(''),
    address: new FormControl(''),
  });

  constructor(private httpClient: HttpClient) {}

  ngOnInit(){
    this.httpClient.get<CustomerDto[]>(`${this.url}/customers`).subscribe(customers => {
      this.customers = customers;
    });
  }

  onAdd(){
    this.httpClient.post<CustomerDto>(`${this.url}/customers`, this.formGroup.value).subscribe(customer => {
      this.customers = [...this.customers, customer];
      this.formGroup.reset();
    });
  }

  onDelete(customer: CustomerDto){
    this.httpClient.delete(`${this.url}/customers/${customer.id}`).subscribe(() => {
      this.customers = this.customers.filter(c => c !== customer);
    });
  }

  fillFormToUpdate(customer: CustomerDto){
    this.isNewRecord = false;
    this.customerId = customer.id;
    this.formGroup.patchValue(customer);
  }

  onUpdate(){
    this.httpClient.put<CustomerDto>(`${this.url}/customers/${this.customerId}`, this.formGroup.value).subscribe(updatedCustomer => {
      this.customers = this.customers.map(c => c.id === updatedCustomer.id ? updatedCustomer : c);
    });
    this.formGroup.reset();
  }

  save(){
    if(this.isNewRecord){
      this.onAdd();
    } else {
      this.onUpdate();
    }
  }

}