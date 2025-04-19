//import { Component, OnInit } from '@angular/core';
//import { ServiesapiService } from '../ServiseApi/serviesapi.service';
//import { CommonModule } from '@angular/common';

//@Component({
//  selector: 'app-dashboard',
//  standalone: true,
//  imports: [CommonModule],
//  templateUrl: './dashboard.component.html',
//  styleUrls: ['./dashboard.component.css']
//})
//export class DashboardComponent implements OnInit {
//  allBooking: any[] = [];

//  constructor(private serviesApi: ServiesapiService) { }

//  ngOnInit(): void {
//    this.getAllBooking();
//  }

// getAllBooking(): void {
//  this.serviesApi.gitAllBooking().subscribe(response => {
//    console.log('✅ Full API response:', response);
//    if (response.status === 200) {
//      this.allBooking = response.body;
//    }
//  }, error => {
//    console.error('❌ Error fetching bookings:', error);
//  });
//}

//  }
//}
