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
    
    if (!this.isAddMode) {
      this.http.get<Student>(this.apiUrl+'Student/' + this.id)
        .subscribe(result => {
          this.form.patchValue(result);
          this.student = result;
        });
    }
    this.form = this.formBuilder.group({
      regNumber: ['', Validators.required],
      name: ['', Validators.required],
      birthDate: [new Date(), Validators.required],
    });
  }

}
