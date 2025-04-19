import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiesapiService {

  private apiUrl = 'https://localhost:7280/api/Dashboard';
  private servicesUrl = `${this.apiUrl}/services`; // رابط خاص بالخدمات

  constructor(private http: HttpClient) { }

  // Get Bookings
  getAllBookings(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // Get Total Users
  getTotalUsers(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total-users`);
  }

  // Get Total Bookings
  getTotalBookings(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total-bookings`);
  }

  // Get Total Instructors
  getTotalInstructors(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total-instructors`);
  }

  // ===== Services API =====

  // Get All Services
  getAllServices(): Observable<any[]> {
    return this.http.get<any[]>(this.servicesUrl);
  }

  // Add New Service
  addService(service: any): Observable<any> {
    return this.http.post<any>(this.servicesUrl, service);
  }

  // Update Service
  updateService(id: number, service: any): Observable<any> {
    return this.http.put<any>(`${this.servicesUrl}/${id}`, service);
  }

  // Delete Service
  deleteService(id: number): Observable<any> {
    return this.http.delete<any>(`${this.servicesUrl}/${id}`);
  }
}
