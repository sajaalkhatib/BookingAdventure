import { Component, OnInit } from '@angular/core';
import { ServiesapiService } from '../../ServiseApi/serviesapi.service';

@Component({
  selector: 'app-overview',
  standalone: false,
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
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

  // ✅ دالة تجيب الحجوزات
  getAllBookings() {
    this.bookingService.getAllBookings().subscribe(
      (data: any[]) => {
        this.allBooking = data;
        console.log('All Bookings:', this.allBooking);
      },
      (error: any) => {
        console.error('Error fetching bookings:', error);
      }
    );
  }

  // ✅ Get total counts (users, bookings, instructors)
  getTotalCounts() {
    this.bookingService.getTotalUsers().subscribe(
      (data: number) => {
        this.totalUsers = data;
        this.animateCounter('users', data);
      },
      (error: any) => console.error('Error fetching total users:', error)
    );

    this.bookingService.getTotalBookings().subscribe(
      (data: number) => {
        this.totalBookings = data;
        this.animateCounter('bookings', data);
      },
      (error: any) => console.error('Error fetching total bookings:', error)
    );

    this.bookingService.getTotalInstructors().subscribe(
      (data: number) => {
        this.totalInstructors = data;
        this.animateCounter('instructors', data);
      },
      (error: any) => console.error('Error fetching total instructors:', error)
    );
  }

  // ✅ Animate counter for users, bookings, and instructors
  animateCounter(type: string, target: number) {
    let count = 0;
    const step = Math.ceil(target / 50);

    const interval = setInterval(() => {
      count += step;
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
