import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  adventures: any[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getAllAdventures();
  }

  getAllAdventures() {
    this.http.get<any[]>('https://localhost:7280/api/Dashboard/adventures')
      .subscribe({
        next: (data) => {
          this.adventures = data;
          console.log(this.adventures); // بس للتأكد
        },
        error: (error) => {
          console.error('Error fetching adventures', error);
        }
      });
  }
}
