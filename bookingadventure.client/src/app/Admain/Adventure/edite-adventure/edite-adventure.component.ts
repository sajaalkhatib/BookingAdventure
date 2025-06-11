import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edite-adventure',
  standalone: false,
  templateUrl: './edite-adventure.component.html',
  styleUrl: './edite-adventure.component.css'
})
export class EditeAdventureComponent implements OnInit {

  adventureId!: number;
  adventure: any = {};

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.adventureId = +this.route.snapshot.paramMap.get('id')!;
    this.getAdventure();
  }

  getAdventure(): void {
    this.http.get<any>(`https://localhost:7280/api/Dashboard/adventure/${this.adventureId}`)
      .subscribe(
        data => {
          this.adventure = data;
        },
        error => {
          console.error('Error fetching adventure:', error);
        }
      );
  }

  updateAdventure(): void {
    this.http.put(`https://localhost:7280/api/Dashboard/adventure/${this.adventureId}`, this.adventure)
      .subscribe(
        () => {
          alert('Adventure updated successfully!');
          this.router.navigate(['/Admain/adventures']); // رجعه على صفحة عرض المغامرات بعد التحديث
        },
        error => {
          console.error('Error updating adventure:', error);
        }
    );



  }



  cancel(): void {
    this.router.navigate(['/Admin/getAdventure']);
  }

}
