import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from '../models/subject';
import { Teacher } from '../models/teacher';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.css']
})
export class SubjectComponent implements OnInit {
  form: FormGroup;
  id: number;
  isAddMode: boolean;
  loading = false;
  submitted = false;
  subject: Subject;
  teacherList: Teacher[];
  constructor(
    @Inject('API_URL') private apiUrl: string,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private readonly http: HttpClient
  ) {
    this.http.get<Teacher[]>(apiUrl + 'Teacher').subscribe(result => {
      this.teacherList = result;
    }, error => console.error(error));
  }
  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        this.id = params.id;
      });
    this.isAddMode = !this.id;

    if (!this.isAddMode) {
      this.http.get<Subject>(this.apiUrl + 'Subject/' + this.id)
        .subscribe(result => {
          this.form.patchValue({
            name:result.name, 
            teacherId:result.teacherId
          });
          this.subject = result;
        });
    }
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      teacherId: ['', Validators.required],
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.loading = true;
    if (this.isAddMode) {
      this.createSubject();
    } else {
      this.updateSubject();
    }
  }

  private createSubject() {
    const toSend = { name: this.form.value.name, teacherId: this.form.value.teacherId };
    this.http.post(this.apiUrl + 'Subject', toSend)
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

  private updateSubject() {
    if (this.form.value.name === undefined) { return }
    this.subject.name = this.form.value.name;
    this.subject.teacherId = this.form.value.teacherId;
    this.http.put(this.apiUrl + 'Subject/' + this.id, this.subject)
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
