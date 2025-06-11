import { CommonModule } from '@angular/common';

import { Component } from '@angular/core';
import { AdventureService } from '../../Service-Rudaina/adventureservice';
import { ActivatedRoute } from '@angular/router';
export interface Adventure {
  id: number;
  title: string;
  description: string;
  typeName: string;
  destinationName: string;
  destinationId: number;
  images: string[];
}
@Component({
  selector: 'app-service',
  standalone: false,
  templateUrl: './service.component.html',
  styleUrl: './service.component.css'
})
export class ServiceComponent {
  filteredAdventures: Adventure[] = [];
  selectedGovernorateName: string = "";
  constructor(private route: ActivatedRoute, private adventureService: AdventureService) {

  }
  ngOnInit() {
    const destinationId = Number(this.route.snapshot.paramMap.get('destinationId'));
    console.log(`destinationID: ${ destinationId }`);

    this.adventureService.getAllAdventures().subscribe((allAdventures) => {

      const filtered = allAdventures.filter(adventure => adventure.destinationId == destinationId);
      if (filtered.length > 0) {
        this.selectedGovernorateName = filtered[0].destinationName;
      } else {
        this.selectedGovernorateName = 'Unknown Region'; // أو أي نص افتراضي
      }
      this.filteredAdventures = filtered;
      console.log(this.filteredAdventures);

    });
  }
}
