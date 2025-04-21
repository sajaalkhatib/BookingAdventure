import { Component, OnInit } from '@angular/core';
import { BookingService } from './booking.service';

@Component({
  selector: 'app-booking',
  standalone: false,
  templateUrl: './booking.component.html',
  styleUrl: './booking.component.css'
})
export class BookingComponent implements OnInit {

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

  userBookings: any[] = [];
  extraServiceList = [
    { key: 'pickup', label: 'Pickup From Home', price: 30 },
    { key: 'upgrade', label: 'Accommodation Upgrades', price: 50 },
    { key: 'exclusiveAccess', label: 'Exclusive Access', price: 40 }
  ];

  constructor(private bookingService: BookingService) { }

  ngOnInit(): void {
    this.getBookings();
    this.calculateTotalPrice();
    this.loadReviews();
    this.getReviews();

  }

  submitBooking() {
    this.bookingData['numberOfParticipants'] = this.bookingData.numberOfAdults + this.bookingData.numberOfChildren;

    this.bookingService.createBooking(this.bookingData).subscribe({
      next: (res: any) => {
        alert(' Booking successful!');
        console.log(res);
        this.getBookings();
      },
      error: (err: any) => {
        alert(' Error during booking');
        console.error(err);
      }
    });
}



  getBookings() {
    this.bookingService.getBookingsByUser(this.bookingData.userId).subscribe({
      next: (res: any[]) => {
        this.userBookings = res;
        console.log(' User Reservations:', res);
      },
      error: (err: any) => {
        console.error('خطأ أثناء جلب الحجوزات:', err);
      }
    });


  }



  increaseAdults() {
    this.bookingData.numberOfAdults++;
    this.calculateTotalPrice();
  }

  decreaseAdults() {
    if (this.bookingData.numberOfAdults > 1) {
      this.bookingData.numberOfAdults--;
      this.calculateTotalPrice();
    }
  }

  increaseChildren() {
    this.bookingData.numberOfChildren++;
    this.calculateTotalPrice();
  }

  decreaseChildren() {
    if (this.bookingData.numberOfChildren > 0) {
      this.bookingData.numberOfChildren--;
      this.calculateTotalPrice();
    }
  }







  calculateTotalPrice() {
    let total = 0;
    total += this.bookingData.numberOfAdults * 120;
    total += this.bookingData.numberOfChildren * 0;

    if (this.bookingData.extraServices.pickup) total += 30;
    if (this.bookingData.extraServices.upgrade) total += 50;
    if (this.bookingData.extraServices.exclusiveAccess) total += 40;

    this.bookingData.totalPrice = total;
  }

  private combineDateAndTime(dateStr: string, timeSlot: string): string {
    // input: '2025-05-01', '09:00' ➝ output: '2025-05-01T09:00:00'
    return `${dateStr}T${timeSlot}:00`;
  }

  //-------------------------------------------------------------------------------------------

  reviews: any[] = [];

  newReview = {
    name: '',
    comment: '',
    rating: 5,
    adventureId: 2, // نفس الـ ID تبع المغامرة الحالية
    reviewDate: new Date()
  };

  submitReview() {
    this.bookingService.createReview(this.newReview).subscribe({
      next: () => {
        alert(' Review submitted!');
        this.loadReviews(); // تحديث القائمة
        this.newReview = { name: '', comment: '', rating: 5, adventureId: this.newReview.adventureId, reviewDate: new Date() };
      },
      error: (err) => {
        console.error(' Error submitting review:', err);
      }
    });
  }

  loadReviews() {
    this.bookingService.getReviewsByAdventure(this.newReview.adventureId).subscribe({
      next: (data) => this.reviews = data,
      error: (err) => console.error(' Error loading reviews:', err)
    });
  }


  //-----------------------------------------------------------------------------------------

  getReviews() {
    this.bookingService.getReviewsByAdventure(this.bookingData.adventureId).subscribe({
      next: (res: any[]) => {
        this.reviews = res;
        this.totalReviews = res.length;

        const total = res.reduce((sum, r) => sum + (r.rating || 0), 0);
        this.averageRating = this.totalReviews ? +(total / this.totalReviews).toFixed(1) : 0;

        this.ratingLabel = this.getRatingLabel(this.averageRating);
      },
      error: (err) => {
        console.error("خطأ بجلب التقييمات", err);
      }
    });
  }

  getRatingLabel(rating: number): string {
    if (rating >= 4.5) return 'Excellent';
    if (rating >= 4) return 'Very Good';
    if (rating >= 3) return 'Good';
    if (rating >= 2) return 'Fair';
    return 'Poor';
  }



  //------------------------------------------------------------------------------------------------

  averageRating: number = 0;
  totalReviews: number = 0;
  ratingLabel: string = 'Excellent'; 










}
