//import { Component, OnInit } from '@angular/core';
//import { MyServiceService } from '../serviceFarah/my-service.service';
//import { ActivatedRoute, Route, Router } from '@angular/router';

//@Component({
//  selector: 'app-profile',
//  standalone: false,
//  templateUrl: './profile.component.html',
//  styleUrl: './profile.component.css'
//})
//export class ProfileComponent implements OnInit {

//  user: any;

//  constructor(private _myser: MyServiceService, private active: ActivatedRoute, private _route: Router) { }

//  ngOnInit(): void {
//    const id = sessionStorage.getItem('userId');
//    debugger
//    if (id) {
//      this._myser.getProfile(id).subscribe({
//        next: (res: any) => {
//          this.user = res;
//        },
//        error: (err) => {
//          alert("Failed to load profile");
//          console.error(err);
//        }
//      });
//    }
//  }

//  selectedImage: File | null = null;

//  onFileSelected(event: any): void {
//    this.selectedImage = event.target.files[0];
//  }


//  //editProfile(data: any) {
//  //  let userId = sessionStorage.getItem('userId');
//  //  debugger
//  //  var DataForm = new FormData();
//  //  DataForm.append("FullName", data.FullName)
//  //  DataForm.append("Phone", data.Phone)
//  //  DataForm.append("Img", data.Img)

//  //  this._myser.putProfile(userId, DataForm).subscribe(() => {
//  //    alert("updated !!")
//  //  })
//  //}




//  //editProfile(data: any) {
//  //  let userId = sessionStorage.getItem('userId');
//  //  const formData = new FormData();
//  //  formData.append("FullName", data.FullName);
//  //  formData.append("Phone", data.Phone);

//  //  if (this.selectedImage) {
//  //    formData.append("Img", this.selectedImage); // 👈 لاحظ الاسم "ImageFile"
//  //  }
//  //  console.log(formData);
//  //  this._myser.putProfile(userId, formData).subscribe({
//  //    next: () => alert("Profile updated!"),
//  //    error: () => alert("Something went wrong."),

//  //  });
//  //}


//  editProfile(data: any) {
//    const formData = new FormData();
//    formData.append("FullName", data.FullName);
//    formData.append("Phone", data.Phone);

//    if (this.selectedImage) {
//      formData.append("ImageFile", this.selectedImage);
//    }
//    const id = sessionStorage.getItem('userId');
//    this._myser.putProfile(id, formData).subscribe({
//      next: () => alert("Profile updated!"),
//      error: () => alert("Something went wrong.")
//    });
//    this._route.navigate(["/profile"]);

//  }



//  changePasswordData = {
//    OldPassword: '',
//    NewPassword: '',
//    ConfirmNewPassword: ''
//  };

//  changePassword() {
//    let userId = sessionStorage.getItem('userId');
//    if (!userId) {
//      alert("User ID not found. Please login again.");
//      this._route.navigate(["/profile"]);
//      return;
//    }

//    const formData = new FormData();
//    formData.append("UserId", userId);
//    formData.append("OldPassword", this.changePasswordData.OldPassword.trim());
//    formData.append("NewPassword", this.changePasswordData.NewPassword.trim());
//    formData.append("ConfirmNewPassword", this.changePasswordData.ConfirmNewPassword.trim());

//    this._myser.changeePassword(formData).subscribe({
//      next: (res: any) => {
//        console.log("Password change response:", res);
//        if (res.success) {
//          alert(res.message || "Password changed successfully!");
//        } else {
//          alert(res.message || "Password change failed.");
//        }
//      },
//      error: (err) => {
//        console.error("Change password error:", err);
//        alert("Something went wrong. Please try again later.");
//      }
//    });
//  }




//}


import { Component, OnInit } from '@angular/core';
import { MyServiceService } from '../serviceFarah/my-service.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  user: any;
  selectedImage: File | null = null;

  editFormData: any = {
    FullName: '',
    Phone: ''
  };

  constructor(private _myser: MyServiceService, private active: ActivatedRoute, private _route: Router) { }

  ngOnInit(): void {
    const id = sessionStorage.getItem('userId');
    if (id) {
      this._myser.getProfile(id).subscribe({
        next: (res: any) => {
          this.user = res;
          // ✅ تحديث نموذج التعديل مباشرة من البيانات
          this.editFormData.FullName = res.fullName;
          this.editFormData.Phone = res.phone;
        },
        error: (err) => {
          alert("Failed to load profile");
          console.error(err);
        }
      });
    }
  }

  onFileSelected(event: any): void {
    this.selectedImage = event.target.files[0];
  }

  editProfile(data: any) {
    const formData = new FormData();
    formData.append("FullName", data.FullName);
    formData.append("Phone", data.Phone);

    if (this.selectedImage) {
      formData.append("ImageFile", this.selectedImage);
    }

    const id = sessionStorage.getItem('userId');
    this._myser.putProfile(id, formData).subscribe({
      next: () => {
        alert("Profile updated!");

        this.user.fullName = data.FullName;
        this.user.phone = data.Phone;

        if (this.selectedImage) {
          this.user.img = URL.createObjectURL(this.selectedImage);
        }

        // مافي داعي لتوجيه الصفحة
        // this._route.navigate(["/profile"]);
      },
      error: () => alert("Something went wrong.")
    });
  }


  changePasswordData = {
    OldPassword: '',
    NewPassword: '',
    ConfirmNewPassword: ''
  };

  changePassword() {
    let userId = sessionStorage.getItem('userId');
    if (!userId) {
      alert("User ID not found. Please login again.");
      this._route.navigate(["/profile"]);
      return;
    }

    const formData = new FormData();
    formData.append("UserId", userId);
    formData.append("OldPassword", this.changePasswordData.OldPassword.trim());
    formData.append("NewPassword", this.changePasswordData.NewPassword.trim());
    formData.append("ConfirmNewPassword", this.changePasswordData.ConfirmNewPassword.trim());

    this._myser.changeePassword(formData).subscribe({
      next: (res: any) => {
        if (res.success) {
          alert(res.message || "Password changed successfully!");
        } else {
          alert(res.message || "Password change failed.");
        }
      },
      error: (err) => {
        console.error("Change password error:", err);
        alert("Something went wrong. Please try again later.");
      }
    });
  }
}
