import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Student } from '../models/student';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  form: FormGroup;
  id: number;
  isAddMode: boolean;
  loading = false;
  submitted = false;
  student: Student;
  constructor(
    @Inject('API_URL') private apiUrl: string,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private readonly http: HttpClient
  ) { }

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        this.id = params.id;
      });
    this.isAddMode = !this.id;

    this.form = this.formBuilder.group({
      regNumber: ['', Validators.required],
      name: ['', Validators.required],
      birthDate: ['', Validators.required],
    });

    if (!this.isAddMode) {
      this.http.get<Student>(this.apiUrl + 'Student/' + this.id)
        .subscribe(result => {
          this.form.patchValue({
            regNumber:result.registrationNumber, 
            name:result.name, 
            birthDate:result.birthDate
          });
          this.student = result;
          this.form.get('birthDate').patchValue(this.formatDate(result.birthDate));
        });

    }
  }

  private formatDate(cSDate: any): Date {
    // cSDate is '2017-01-24T14:14:55.807'
    var datestr = String(cSDate);
    var dateAr = datestr.split('-');
    var year = parseInt(dateAr[0]);
    var month = parseInt(dateAr[1])-1;
    var day = parseInt(String(dateAr[2]).substring(0, String(dateAr[2]).indexOf("T")));
    var timestring = String(dateAr[2]).substring(String(dateAr[2]).indexOf("T") + 1);
    var timeAr = timestring.split(":");
    var hour = parseInt(timeAr[0]);
    var min = parseInt(timeAr[1]);
    var sek = parseInt(timeAr[2]);
    var date = new Date(year, month, day, hour, min, sek, 0);
    return date;
}


  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.loading = true;
    if (this.isAddMode) {
      this.createStudent();
    } else {
      this.updateStudent();
    }
  }

  private createStudent() {
    const toSend = { 
      registrationNumber: this.form.value.regNumber, 
      name: this.form.value.name, 
      birthDate: this.form.value.birthDate, 
    };
    this.http.post(this.apiUrl + 'Student', toSend)
      .subscribe({
        next: () => {
          this.router.navigate(['../'], { relativeTo: this.route });
        },
        error: error => {
          console.log(error);
          this.loading = false;
        }
      });
    this.form.reset();
  }

  private updateStudent() {
    if (this.form.value.name === undefined) { return }
    this.student.registrationNumber = this.form.value.regNumber;
    this.student.name = this.form.value.name;
    this.student.birthDate = this.form.value.birthDate;
    this.http.put(this.apiUrl + 'Student/' + this.id, this.student)
      .subscribe({
        next: () => {
          this.router.navigate(['../'], { relativeTo: this.route });
        },
        error: error => {
          console.log(error);
          this.loading = false;
        }
      });
    this.form.reset();
  }


}
