import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  private apiUrl = 'https://localhost:7280/api/Booking';
  private baseUrl = 'https://localhost:7280/api/Feedback';
  constructor(private http: HttpClient) { }

  // ✅ تحديث توقيع البيانات لتشمل كل القيم الجديدة
  createBooking(booking: {
    userId: number;
    adventureId: number;
    scheduledDate: string;
    numberOfAdults: number;
    numberOfChildren: number;
    selectedTimeSlot: string;
    extraServices: {
      pickup: boolean;
      upgrade: boolean;
      exclusiveAccess: boolean;
    };
    paymentType: string;
    totalPrice: number;
  }): Observable<any> {
    return this.http.post(this.apiUrl, booking);
  }

  getBookingsByUser(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/user/${userId}`);
  }


  addReview(review: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Add`, review);
  }


  

  //-----------------------------------------------------------------------------------

  createReview(review: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Create`, review);
  }


  
  getReviewsByAdventure(adventureId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/ByAdventure/${adventureId}`);
  }




}
