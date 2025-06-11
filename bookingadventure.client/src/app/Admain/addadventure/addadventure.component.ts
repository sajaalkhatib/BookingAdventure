//import { Component } from '@angular/core';
//import { Router } from '@angular/router';
//import { ServiesapiService } from '../../ServiseApi/serviesapi.service';

//@Component({
//  selector: 'app-addadventure',
//  templateUrl: './addadventure.component.html',
//  styleUrls: ['./addadventure.component.css']
//})
//export class AddadventureComponent {

//  adventure: Adventur = {
//    adventureId: 0,
//    title: '',
//    description: '',
//    duration: 0,
//    level: '',
//    price: 0,
//    location: '',
//    maxParticipants: 0,
//    isAvailable: true,
//    instructorName: '',
//    categoryName: '',
//    destinationName: ''
//  };

//  constructor(private adventureService: ServiesapiService, private router: Router) { }

//  addAdventure() {
//    this.adventureService.addAdventure(this.adventure).subscribe({
//      next: (response) => {
//        console.log('Adventure added successfully', response);
//        this.router.navigate(['/Admin/getAdventure']);
//      },
//      error: (error) => {
//        console.error('Error adding adventure', error);
//      }
//    });
//  }
//}
