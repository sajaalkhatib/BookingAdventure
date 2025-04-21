import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public showNavbarFooter = true;
  title = 'bookingadventure.client';

  constructor(private http: HttpClient, private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        const hiddenRoutes = ['/login', '/reg', '/dashboard', '/Admin'];
        this.showNavbarFooter = !hiddenRoutes.some(route => event.urlAfterRedirects.includes(route));
      }
    });
  }

  ngOnInit(): void {
    // حذفنا getForecasts لأنه مش مستخدم
  }
}
