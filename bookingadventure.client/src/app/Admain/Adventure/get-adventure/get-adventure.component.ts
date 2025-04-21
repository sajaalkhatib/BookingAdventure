import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-get-adventure',
  standalone: false,
  templateUrl: './get-adventure.component.html',
  styleUrl: './get-adventure.component.css'
})
export class GetAdventureComponent implements OnInit {
  adventures: any[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getAllAdventures();
  }

  getAllAdventures(): void {
    this.http.get<any[]>('https://localhost:7280/api/Dashboard/adventures')
      .subscribe(
        data => this.adventures = data,
        error => console.error('Error fetching adventures:', error)
      );
  }

  deleteAdventure(id: number): void {
    if (confirm('Are you sure you want to delete this adventure?')) {
      this.http.delete(`https://localhost:7280/api/Dashboard/adventure/${id}`)
        .subscribe(
          () => {
            this.adventures = this.adventures.filter(a => a.adventureId !== id);
          },
          error => console.error('Error deleting adventure:', error)
        );
    }
  }

  editAdventure(id: number): void {
    console.log('Edit adventure with ID:', id);
  }
}
