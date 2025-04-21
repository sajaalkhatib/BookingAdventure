import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MyServiceService {
  private apiUrl = "https://localhost:7280/api/RegisterLogin";
  constructor(private _http: HttpClient) { }

  registration(data: any) {
    return this._http.post(`https://localhost:7280/api/RegisterLogin/register`, data);
  }

  login(data: FormData) {
    return this._http.post('https://localhost:7280/api/RegisterLogin/login', data, {
      observe: 'response'
    });
  }

  getProfile(id: any) {
    return this._http.get(`https://localhost:7280/api/RegisterLogin/profile/${id}`);
  }

  putProfile(id: any, data: any) {
    return this._http.put(`https://localhost:7280/api/RegisterLogin/update-profile/${id}`, data)

    
  }

  changeePassword(data: FormData) {
    return this._http.put('https://localhost:7280/api/RegisterLogin/change-password', data);
  }

 

  sendResetCode(email: string): Observable<any> {
    return this._http.post(`https://localhost:7280/api/RegisterLogin/send-reset-code`, {email});
  }

  verifyResetCode(email: string, code: string): Observable<any> {
    return this._http.post(`https://localhost:7280/api/RegisterLogin/verify-reset-code`, {email,code});
  }

  resetPassword(email: string, newPassword: string): Observable<any> {
    return this._http.post(`https://localhost:7280/api/RegisterLogin/reset-password`, {email,newPassword});
  }

}
