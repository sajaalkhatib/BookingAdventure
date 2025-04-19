import { Component, OnInit } from '@angular/core';
import { ServiesapiService } from '../../ServiseApi/serviesapi.service';

@Component({
  selector: 'app-overview',
  standalone: false,
  templateUrl: './overview.component.html',
  styleUrl: './overview.component.css'
})

export class OverviewComponent implements OnInit {
  allBooking: any[] = [];

  constructor(private bookingService: ServiesapiService) { }

  ngOnInit() {
    this.getAllBookings();
  }

  getAllBookings() {
    this.bookingService.getAllBookings().subscribe(
      data => {
        this.allBooking = data;
        console.log(this.allBooking); // تحقق من البيانات في وحدة التحكم
      },
      error => {
        console.error('Error fetching bookings:', error);
      }
    );
  }
}
