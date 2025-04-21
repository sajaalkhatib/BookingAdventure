import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { ContactComponent } from './contact/contact.component';

import { DestinationComponent } from './destination/destination.component';
import { DestinationdetailsComponent } from './destinationdetails/destinationdetails.component';
import { BookingComponent } from './booking/booking.component';
import { LoginComponent } from './login/login.component';
import { RegesterComponent } from './regester/regester.component';
import { ProfileComponent } from './profile/profile.component';
import { ServiceComponent } from './service/service.component';
//import { DashboardComponent } from './dashboard/dashboard.component';
import { AdmainComponent } from './Admain/admain.component';
import { OverviewComponent } from './Admain/overview/overview.component';
import { GetAdventureComponent } from './Admain/Adventure/get-adventure/get-adventure.component';
import { EditeAdventureComponent } from './Admain/Adventure/edite-adventure/edite-adventure.component';
//import { AddadventureComponent } from './Admain/addadventure/addadventure.component';


const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "about", component: AboutComponent },
  { path: "contact", component: ContactComponent },

  { path: "Destination", component: DestinationComponent },
  { path: "destinationdetails", component: DestinationdetailsComponent },
  { path: "booking", component: BookingComponent },
  { path: "login", component: LoginComponent },
  { path: "reg", component: RegesterComponent },
  { path: "profile", component: ProfileComponent },
  { path: "service", component: ServiceComponent },
  //{ path: "dashbord", component: DashboardComponent },
  {
    path: "Admin", component: AdmainComponent, children: [
      { path: "", component: OverviewComponent },
      { path: "getAdventure", component: GetAdventureComponent },
      { path: "editeAdventure/:id", component: EditeAdventureComponent },
      //{ path: "addAdventure", component: AddadventureComponent }
      


    ]
  }]

@NgModule({


  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
