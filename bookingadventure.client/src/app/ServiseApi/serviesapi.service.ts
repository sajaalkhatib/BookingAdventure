import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiesapiService {

  private apiUrl = 'https://localhost:7280/api/Dashboard';
  private baseUrl = 'https://localhost:7280/api/Dashboard/adventures';
  constructor(private http: HttpClient) { }

 
  getAllBookings(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/bookings`);
  }


  getTotalUsers(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total-users`);
  }


  getTotalBookings(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total-bookings`);
  }

  getTotalInstructors(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total-instructors`);
  }


  // ===== Services API =====
 
  getAllAdventures(): Observable<any> {
    return this.http.get<any>(`https://localhost:7280/api/Dashboard/adventures`)
  }


  addAdventure(adventure: FormData) {
    debugger
    return this.http.post(`https://localhost:7280/api/Dashboard/adventure`, adventure);
  }

 


  updateAdventure(id: number, adventure: any): Observable<any> {
    debugger
    return this.http.put<any>(`https://localhost:7280/api/Dashboard/adventure/${id}`, adventure);
  }

  deleteAdventure(id: any) {
    this.http.delete(`https://localhost:7280/api/Dashboard/adventures/${id}`)
  }





}




