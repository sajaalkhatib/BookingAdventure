import { Component } from '@angular/core';
import { AdventureService } from '../../Service-Rudaina/adventureservice';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';

export interface AdventureDetailsDto {
  adventureId: number;
  title: string;
  typeName: string;
  description: string;
  duration: number;
  price: number;
  maxParticipants: number;
  adventureTypeName: string;
  instructorName: string;
  images: string[];
  overview: string;  // إضافة حقل Overview
  highlightsJson?: string;  // إضافة حقل HighlightsJson (اختياري)
  faqsJson?: string;  // إضافة حقل FaqsJson (اختياري)
}


@Component({
  selector: 'app-booking',
  standalone: false,
  templateUrl: './booking.component.html',
  styleUrl: './booking.component.css'
})
export class BookingComponent {
  adventureId: number = 0;
  Adventure?: AdventureDetailsDto;
  bookingData = {
    userId: 1,
    adventureId: 0,
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
  constructor(
    private _AdventureServices: AdventureService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    const idParam = this.route.snapshot.paramMap.get('adventureId');
    this.adventureId = idParam ? +idParam : 0;

    if (this.adventureId) {
      this.getAdventuresByID(this.adventureId);
    }
    this.bookingData.adventureId = this.adventureId;
    this.newReview.adventureId = this.adventureId;

    this.getBookings();
    this.calculateTotalPrice();
    this.loadReviews();
    this.getReviews();

  }
  submitBooking() {
    this.bookingData['numberOfParticipants'] = this.bookingData.numberOfAdults + this.bookingData.numberOfChildren;

    this._AdventureServices.createBooking(this.bookingData).subscribe({
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

  getAdventuresByID(id: number) {
    this._AdventureServices.getAdventuresByID(id).subscribe({
      next: (data) => {
        this.Adventure = data;
        console.log("Adventure Loaded:", data);
      },
      error: (err) => {
        console.error("Error loading adventure", err);
      }
    });
  }
  get highlightsJsonParsed() {
    // إذا كان highlightsJson موجودًا وقادرًا على التحليل
    if (this.Adventure?.highlightsJson) {
      try {
        return JSON.parse(this.Adventure.highlightsJson); // قم بتحليل النص إلى مصفوفة
      } catch (error) {
        console.error("Error parsing highlightsJson:", error);
        return [];  // في حالة حدوث خطأ، أعد مصفوفة فارغة
      }
    }
    return []; // إذا لم يكن هناك highlightsJson
  }
  getBookings() {
    this._AdventureServices.getBookingsByUser(this.bookingData.userId).subscribe({
      next: (res: any[]) => {
        this.userBookings = res;
        console.log(' User Reservations:', res);
      },
      error: (err: any) => {
        console.error('خطأ أثناء جلب الحجوزات:', err);
      }
    });


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
  reviews: any[] = [];
  newReview = {
    name: '',
    comment: '',
    rating: 5,
    adventureId: 0, // 🔥 خذها من الـ bookingData
    reviewDate: new Date()
  };

  submitReview() {
    
    this._AdventureServices.createReview(this.newReview).subscribe({
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
    this._AdventureServices.getReviewsByAdventure(this.newReview.adventureId).subscribe({
      next: (data) => this.reviews = data,
      error: (err) => console.error(' Error loading reviews:', err)
    });
  }


  //-----------------------------------------------------------------------------------------

  getReviews() {
    this._AdventureServices.getReviewsByAdventure(this.bookingData.adventureId).subscribe({
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




  averageRating: number = 0;
  totalReviews: number = 0;
  ratingLabel: string = 'Excellent';

}
