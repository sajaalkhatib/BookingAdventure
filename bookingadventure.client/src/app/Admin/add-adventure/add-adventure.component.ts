import { Component, OnInit } from '@angular/core';
import { RudainaAdminService } from '../../../AdminService-Rudaina/rudaina-admin.service';
import Swal from 'sweetalert2';

interface AdventureFormValues {
  ImageUrls: string; // روابط الصور مفصولة بفواصل أو أسطر
  Title: string;
  Description: string;
  Duration: string;
  Level: string;
  Price: string;
  Location: string;
  MaxParticipants: string;
  InstructorId: string;
  TypeId: string;
  DestinationId: string;
}
@Component({
  selector: 'app-add-adventure',
  templateUrl: './add-adventure.component.html',
  styleUrls: ['./add-adventure.component.css'],
  standalone:false
})
export class AddAdventureComponent implements OnInit {

  instructors: any[] = [];
  types: any[] = [];
  destinations: any[] = [];
  categories: any[] = [];

  constructor(private adminService: RudainaAdminService) { }

  ngOnInit(): void {
    this.loadDynamicData();
  }

  loadDynamicData(): void {
    this.adminService.getUniqueInstructors().subscribe(data => {
      this.instructors = data;
      console.log(data);
    });

    this.adminService.getUniqueAdventureTypes().subscribe(data => {
      this.types = data;
      console.log(data);

    });

    this.adminService.getUniqueDestinations().subscribe(data => {
      this.destinations = data;
      console.log(data);

    });
  }



onSubmit(formValues: AdventureFormValues): void {
  console.log(formValues);

  const formData = new FormData();

  // 🔄 تنسيق الروابط: فصل بواسطة فاصلة أو سطر جديد، ثم حذف المسافات الزائدة
  const imageUrls = formValues.ImageUrls
    .split(/[\n,]+/) // فصل على أساس فاصلة أو سطر جديد
    .map((url: string) => url.trim())
    .filter((url: string) => url.length > 0)
    .join(','); // دمجهم بفاصل واحد

  // ✔️ إضافة الصور كـ string مفصولة بفواصل إلى FormData
  if(imageUrls) {
    formData.append('ImageUrl', imageUrls); // نفس اسم الحقل المستخدم في backend
  }

  // ✔️ إضافة باقي الحقول
  formData.append('Title', formValues.Title);
  formData.append('Description', formValues.Description);
  formData.append('Duration', formValues.Duration);
  formData.append('Level', formValues.Level);
  formData.append('Price', formValues.Price);
  formData.append('Location', formValues.Location);
  formData.append('MaxParticipants', formValues.MaxParticipants);
  formData.append('InstructorId', formValues.InstructorId);
  formData.append('TypeId', formValues.TypeId);
  formData.append('DestinationId', formValues.DestinationId);

  // ✔️ إرسال البيانات إلى الخدمة
  this.adminService.addAdventure(formData).subscribe({
    next: res => {
      console.log('Adventure added successfully!', res);

      Swal.fire({
        icon: 'success',
        title: 'Success',
        text: 'Adventure has been added successfully!',
        confirmButtonColor: '#3085d6'
      });
    },
    error: err => {
      if (err.error && err.error.errors) {
        console.error('Validation errors:', err.error.errors);
        Swal.fire({
          icon: 'warning',
          title: 'Validation Error',
          text: 'Please check the form data and try again.',
          confirmButtonColor: '#d33'
        });
      } else {
        console.error('Error adding adventure:', err);
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An unexpected error occurred while adding the adventure.',
          confirmButtonColor: '#d33'
        });
      }
    }
  });

}




}
