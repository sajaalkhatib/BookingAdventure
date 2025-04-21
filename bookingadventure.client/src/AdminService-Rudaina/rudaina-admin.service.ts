import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RudainaAdminService {

  private baseUrl = 'https://localhost:7280/api/RudainaAdmin';

  private AddAdventureAPI = `${this.baseUrl}/AddAdventure`;

  private InstructorsAPI = `${this.baseUrl}/unique-instructors`;
  private TypesAdventureAPI = `${this.baseUrl}/unique-adventure-types`;
  private DestinationsAPI = `${this.baseUrl}/unique-destinations`;

  constructor(private _http: HttpClient) { }

  // 🎯 لإرسال مغامرة جديدة
  addAdventure(adventureData: any): Observable<any> {
    return this._http.post(this.AddAdventureAPI, adventureData);
  }

  // 🎯 لجلب المدربين بدون تكرار
  getUniqueInstructors(): Observable<string[]> {
    return this._http.get<string[]>(this.InstructorsAPI);
  }

  // 🎯 لجلب أنواع المغامرات بدون تكرار
  getUniqueAdventureTypes(): Observable<string[]> {
    return this._http.get<string[]>(this.TypesAdventureAPI);
  }

  // 🎯 لجلب الوجهات بدون تكرار
  getUniqueDestinations(): Observable<string[]> {
    return this._http.get<string[]>(this.DestinationsAPI);
  }
}
