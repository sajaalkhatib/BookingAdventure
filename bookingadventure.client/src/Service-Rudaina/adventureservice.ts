import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
export interface Adventure {
  id: number;
  title: string;
  description: string;
  typeName: string;
  destinationName: string;
  destinationId: number;
  images: string[];
  overview: string;  // إضافة حقل Overview
  highlightsJson?: string;  // إضافة حقل HighlightsJson (اختياري)
  faqsJson?: string;  // إضافة حقل FaqsJson (اختياري)
}

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

@Injectable({
  providedIn: 'root'
})
export class AdventureService {
  private AdventuresApi = "https://localhost:7280/api/Rudaina/allAdventures";
  private apiUrl = 'https://localhost:7280/api/Rudaina/CreateBooking';
  private baseUrl = 'https://localhost:7280/api/Rudaina';



  constructor(private _http: HttpClient) { }
  getAllAdventures() {
    return this._http.get<Adventure[]>(this.AdventuresApi);
  }
  getAdventuresByID( id :any) {
    return this._http.get < AdventureDetailsDto>(`https://localhost:7280/api/Rudaina/GetAdventureDetails/${id}`);
  }
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
    return this._http.post(this.apiUrl, booking);
  }
  getBookingsByUser(userId: number): Observable<any[]> {
    return this._http.get<any[]>(`${ this.apiUrl } / user / ${ userId }`);
  }
  addReview(review: any): Observable<any> {
    return this._http.post(`${ this.baseUrl } / Add`, review);
  }





  createReview(review: any): Observable<any> {
    return this._http.post(this.baseUrl, review);

  }


  getReviewsByAdventure(adventureId: number): Observable<any[]> {
    return this._http.get<any[]>(`${this.baseUrl}/ByAdventure/${adventureId}`);
  }

}
