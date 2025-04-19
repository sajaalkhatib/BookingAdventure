import { Component, OnInit } from '@angular/core';
import { ServiesapiService } from '../../ServiseApi/serviesapi.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-admin-ser',
  standalone: false,
  templateUrl: './admin-ser.component.html',
  styleUrl: './admin-ser.component.css'
})
export class AdminSerComponent implements OnInit {


  adventures: any[] = [];
  newAdventure: any = {
    title: '',
    description: '',
    duration: '',
    level: '',
    price: 0,
    location: '',
    maxParticipants: 0,
    isAvailable: true,
    imageUrl: ''
  };
  editMode: boolean = false;
  editingAdventureId: number | null = null;

  constructor(private adventureService: ServiesapiService, private http: HttpClient) { }

  ngOnInit(): void {
    this.loadAdventures();
  }

  loadAdventures(): void {
    this.adventureService.getAllAdventures().subscribe(data => {
      this.adventures = data;
    });
  }

  addAdventure(): void {
    this.adventureService.addAdventure(this.newAdventure).subscribe(() => {
      this.loadAdventures();
      this.newAdventure = { title: '', description: '', duration: '', level: '', price: 0, location: '', maxParticipants: 0, isAvailable: true, imageUrl: '' };
    });
  }

  editAdventure(adventure: any): void {
    this.newAdventure = { ...adventure };
    this.editingAdventureId = adventure.adventureId;
    this.editMode = true;
  }

  updateAdventure(): void {
    if (this.editingAdventureId !== null) {
      this.adventureService.updateAdventure(this.editingAdventureId, this.newAdventure).subscribe(() => {
        this.loadAdventures();
        this.newAdventure = { title: '', description: '', duration: '', level: '', price: 0, location: '', maxParticipants: 0, isAvailable: true, imageUrl: '' };
        this.editingAdventureId = null;
        this.editMode = false;
      });
    }
  }

  deleteAdventure(id: number): void {
    if (confirm('Are you sure you want to delete this adventure?')) {
      this.adventureService.deleteAdventure(id).subscribe(() => {
        this.loadAdventures();
      });
    }
  }

  cancelEdit(): void {
    this.editMode = false;
    this.editingAdventureId = null;
    this.newAdventure = { title: '', description: '', duration: '', level: '', price: 0, location: '', maxParticipants: 0, isAvailable: true, imageUrl: '' };
  }
}
