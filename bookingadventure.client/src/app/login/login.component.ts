import { Component } from '@angular/core';
import { MyServiceService } from '../serviceFarah/my-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private _myser: MyServiceService, private _route: Router) { }

  //loginUser(data:any) {
  //  var formData = new FormData();
  //  formData.append("Email", data.Email);
  //  formData.append("Password", data.Password);

  //  this._myser.login(data).subscribe(() => {
  //    alert("Login successful")
  //    this._route.navigate(["/about"]);
  //  })

  //loginUser(data: any) {
  //  const formData = new FormData();
  //  formData.append("Email", data.Email);
  //  formData.append("Password", data.Password);

  //  this._myser.login(formData).subscribe(
  //    (res: any) => {
  //      alert("login "); // "Login successful"
  //      this._route.navigate(["/about"]);
  //    },
  //    error => {
  //      alert("Login failed: " + error.error);
  //    }
  //  );
  //}

  loginUser(data: any) {
    const formData = new FormData();
    formData.append("Email", data.Email);
    formData.append("Password", data.Password);

    this._myser.login(formData).subscribe({
      next: (response: any) => {
        if (response.status === 200) {
          const userId = response.body.user.userId;
          sessionStorage.setItem('userId', userId);
          alert('Login Successful!');
          this._route.navigate(["/about"]);
        }
      },
      error: (err) => {
        console.error('Login error:', err);
        if (err.status === 404) {
          alert('User not found.');
        } else if (err.status === 400) {
          alert('Invalid email or password.');
        } else {
          alert('An error occurred. Please try again later.');
        }
      }
    });
  }

}
