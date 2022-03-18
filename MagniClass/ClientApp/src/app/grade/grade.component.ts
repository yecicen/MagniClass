import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Grade } from '../models/grade';

@Component({
  selector: 'app-grade',
  templateUrl: './grade.component.html',
  styleUrls: ['./grade.component.css']
})
export class GradeComponent implements OnInit {
  form: FormGroup;
  id: number;
  isAddMode: boolean;
  loading = false;
  submitted = false;
  grade: Grade;
  constructor(
    @Inject('API_URL') private apiUrl: string,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private readonly http: HttpClient
  ) { 
    //to do bring student names and subject names
  }

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        this.id = params.id;
      });
    this.isAddMode = !this.id;

    if (!this.isAddMode) {
      this.http.get<Grade>(this.apiUrl + 'Grade/' + this.id)
        .subscribe(result => {
          this.form.patchValue({
            score:result.score
          });
          this.grade = result;
        });
    }
    this.form = this.formBuilder.group({
      score: ['', Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.loading = true;
    this.updateGrade();
  }

  private updateGrade() {
    if (this.form.value.score === undefined) { return }
    this.grade.score = this.form.value.score;
    this.http.put(this.apiUrl + 'Grade/' + this.id, this.grade)
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
