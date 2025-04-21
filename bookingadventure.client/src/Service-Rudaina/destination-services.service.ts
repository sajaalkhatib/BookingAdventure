import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
export interface Destination {
  destinationId: number;
  name: string;
  description: string;
  imageUrl: string;
  adventures: any[];
  categories: any[];
}



@Injectable({
  providedIn: 'root'
})
export class DestinationServicesService {
  private destinationApi = "https://localhost:7280/api/Rudaina/api/destinations";


  constructor(private _http: HttpClient) { }
  getAllDestinations() {
    return this._http.get<Destination[]>(this.destinationApi); 
  }
 

}
