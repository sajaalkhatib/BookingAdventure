import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

//import { FormsModule } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { NavComponent } from './nav/nav.component';
import { FooterComponent } from './footer/footer.component';
import { ContactComponent } from './contact/contact.component';

import { DestinationComponent } from './destination/destination.component';
import { DestinationdetailsComponent } from './destinationdetails/destinationdetails.component';
import { BookingComponent } from './booking/booking.component';
import { LoginComponent } from './login/login.component';
import { RegesterComponent } from './regester/regester.component';
import { ProfileComponent } from './profile/profile.component';
import { CommonModule } from '@angular/common';
import { OverviewComponent } from './Admain/overview/overview.component';
import { AdmainComponent } from './Admain/admain.component';
import { GetAdventureComponent } from './Admain/Adventure/get-adventure/get-adventure.component';
import { EditeAdventureComponent } from './Admain/Adventure/edite-adventure/edite-adventure.component';
//import { AddadventureComponent } from './Admain/addadventure/addadventure.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';

;
import { ServiceComponent } from './service/service.component';

import { AddAdventureComponent } from './Admin/add-adventure/add-adventure.component';





@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    NavComponent,
    FooterComponent,
    ContactComponent,
   
    DestinationComponent,
    DestinationdetailsComponent,
    BookingComponent,
    LoginComponent,
    RegesterComponent,
    ProfileComponent,
    OverviewComponent,
    AdmainComponent,
    GetAdventureComponent,
    EditeAdventureComponent,
    //AddadventureComponent,
    ForgotPasswordComponent,
    ServiceComponent,
    AddAdventureComponent

  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    CommonModule,
    FormsModule
    , HttpClientModule, FormsModule, ReactiveFormsModule,
    AppRoutingModule
    ,
    FormsModule,
    ReactiveFormsModule 

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
