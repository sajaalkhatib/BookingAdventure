import { Component } from '@angular/core';
import { MyServiceService } from '../serviceFarah/my-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-regester',
  standalone: false,
  templateUrl: './regester.component.html',
  styleUrl: './regester.component.css'
})
export class RegesterComponent {
  constructor(private _myser: MyServiceService, private _route: Router) { }

  registerData: any = {
    FullName: '',
    Email: '',
    Password: '',
    Phone: ''
  };

  selectedImage: File | null = null;


  onFileSelected(event: any): void {
    this.selectedImage = event.target.files[0];
  }

  registerUser() {
    const formData = new FormData();
    formData.append("FullName", this.registerData.FullName);
    formData.append("Email", this.registerData.Email);
    formData.append("Password", this.registerData.Password);
    formData.append("Phone", this.registerData.Phone);

    if (this.selectedImage) {
      formData.append("Img", this.selectedImage);
    }

    this._myser.registration(formData).subscribe(() => {
      alert("Registered successfully");
      this._route.navigate(["/login"]);
    });
  }
  
}
