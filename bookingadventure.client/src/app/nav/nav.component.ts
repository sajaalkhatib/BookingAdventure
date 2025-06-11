import { Component, OnInit } from '@angular/core';
import { MyServiceService } from '../serviceFarah/my-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  standalone: false,
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {
  isLoggedIn: boolean = false;

  constructor(private _myser: MyServiceService, private router: Router) { }

  ngOnInit(): void {
    this._myser.loginStatus$.subscribe(status => {
      this.isLoggedIn = status;
    });
  }

  logout() {
    this._myser.logout();
    this.router.navigate(['/login']);
  }
}
