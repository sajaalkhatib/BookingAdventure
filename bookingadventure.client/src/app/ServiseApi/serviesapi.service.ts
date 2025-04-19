import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiesapiService {

  private apiUrl = 'https://localhost:7280/api/Dashboard'; // ✅ بدون /bookings
  private baseUrl = 'https://localhost:7280/api/Dashboard/adventures';
  constructor(private http: HttpClient) { }

  // ✅ دالة تجيب كل الحجوزات
  getAllBookings(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/bookings`);
  }

  // ✅ Get Total Users
  getTotalUsers(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total-users`);
  }

  // ✅ Get Total Bookings
  getTotalBookings(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total-bookings`);
  }

  // ✅ Get Total Instructors
  getTotalInstructors(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total-instructors`);
  }


  // ===== Services API =====
  getAllAdventures(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/adventures`);
  }

  addAdventure(adventure: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/adventure`, adventure);
  }

  updateAdventure(id: number, adventure: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/adventure/${id}`, adventure);
  }

  deleteAdventure(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/adventure/${id}`);
  }
}




