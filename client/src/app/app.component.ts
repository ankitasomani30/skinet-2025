import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/product';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  baseUrl = 'http://localhost:5000/api';
  protected title = "skinet";
  private http = inject(HttpClient);
  products : Product[] = [];

  ngOnInit(): void {
    this.http.get<any>(this.baseUrl + '/products').subscribe({
      next : response => this.products = response.data,
      error: error => console.log(error),
      complete : () => console.log("Request completed.")
    })
  }
}
