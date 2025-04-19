import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiesapiService {


  private apiUrl = 'https://localhost:7280/api/Dashboard'; // تأكد من مسار API الصحيح

  constructor(private http: HttpClient) { }

  getAllBookings(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

}
