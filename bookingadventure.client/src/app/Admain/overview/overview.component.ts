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

  totalUsers: number = 0;
  totalBookings: number = 0;
  totalInstructors: number = 0;

  animatedUsers: number = 0;
  animatedBookings: number = 0;
  animatedInstructors: number = 0;

  constructor(private bookingService: ServiesapiService) { }

  ngOnInit() {
    this.getAllBookings();
    this.getTotalCounts();
  }

  getTotalCounts() {
    this.bookingService.getTotalUsers().subscribe(
      data => {
        this.totalUsers = data;
        this.animateCounter('users', data);
      },
      error => console.error('Error fetching total users:', error)
    );

    this.bookingService.getTotalBookings().subscribe(
      data => {
        this.totalBookings = data;
        this.animateCounter('bookings', data);
      },
      error => console.error('Error fetching total bookings:', error)
    );

    this.bookingService.getTotalInstructors().subscribe(
      data => {
        this.totalInstructors = data;
        this.animateCounter('instructors', data);
      },
      error => console.error('Error fetching total instructors:', error)
    );
  }

  getAllBookings() {
    this.bookingService.getAllBookings().subscribe(
      data => {
        this.allBooking = data;
        console.log(this.allBooking);
      },
      error => {
        console.error('Error fetching bookings:', error);
      }
    );
  }

  animateCounter(type: string, target: number) {
    let count = 0;
    const interval = setInterval(() => {
      count += Math.ceil(target / 50);
      if (count >= target) {
        count = target;
        clearInterval(interval);
      }
      if (type === 'users') this.animatedUsers = count;
      if (type === 'bookings') this.animatedBookings = count;
      if (type === 'instructors') this.animatedInstructors = count;
    }, 30);
  }
}
