import { Component, OnInit } from '@angular/core';
import { BookingService } from '../booking/booking.service';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  bookingData = {
    userId: 1,
    adventureId: 2,
    scheduledDate: '',
    numberOfAdults: 1,
    numberOfChildren: 0,
    numberOfParticipants: 0,
    selectedTimeSlot: '',
    extraServices: {
      pickup: false,
      upgrade: false,
      exclusiveAccess: false
    },
    paymentType: 'Online',
    totalPrice: 0
  };


  constructor(private bookingService: BookingService) { }
  ngOnInit(): void {
    this.getBookings();
  }


  userBookings: any[] = [];
  extraServiceList = [
    { key: 'pickup', label: 'Pickup From Home', price: 30 },
    { key: 'upgrade', label: 'Accommodation Upgrades', price: 50 },
    { key: 'exclusiveAccess', label: 'Exclusive Access', price: 40 }
  ];



  getBookings() {
    this.bookingService.getBookingsByUser(this.bookingData.userId).subscribe({
      next: (res: any[]) => {
        this.userBookings = res;
        console.log(' User Reservations:', res);
      },
      error: (err: any) => {
        console.error('Error fetching reservations:', err);
      }
    });


  }


}
