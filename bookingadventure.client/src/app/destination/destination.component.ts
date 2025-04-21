import { Component } from '@angular/core';
import { DestinationServicesService } from '../../Service-Rudaina/destination-services.service';
import { AdventureService } from '../../Service-Rudaina/adventureservice';
export interface Destination {
  destinationId: number;
  name: string;
  description: string;
  imageUrl: string;
  adventures: any[];  // إذا كنت بحاجة إلى استخدام هذه البيانات لاحقًا
  categories: any[];  // إذا كنت بحاجة إلى استخدام هذه البيانات لاحقًا
}


@Component({
  selector: 'app-destination',
  standalone: false,
  templateUrl: './destination.component.html',
  styleUrl: './destination.component.css'
})
export class DestinationComponent {

  constructor(private destinationService: DestinationServicesService, private adventureService:AdventureService) { }
  destinations: Destination[] = [];  // تعريف مصفوفة الوجهات

  ngOnInit() {
    this.getAlldestinations();
   

  }
  getAlldestinations() {
    this.destinationService.getAllDestinations().subscribe(
      (data: Destination[]) => {  
        this.destinations = data;
        console.log("Destinations:", data);
      },
      error => {
        console.error("Error fetching destinations:", error);
      }
    );
  }
 


}
