import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Course } from '../models/course';
import { CourseService } from './course.service';
import { of } from 'rxjs';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseSubject } from '../models/courseSubject';
import { Subject } from '../models/subject';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {
  form: FormGroup;
  id: number;
  isAddMode: boolean;
  loading = false;
  submitted = false;
  course: Course;
  courseSubjects: CourseSubject[];
  subjects: Subject[];
  selectedSubjects: Subject[];
  constructor(
    @Inject('API_URL') private apiUrl: string,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private readonly courseService: CourseService,
    private readonly http: HttpClient
  ) {
    // this.http.get<Subject[]>(this.apiUrl + 'Subject')
    //   .subscribe(result => {
    //     this.form.patchValue({
    //       subjects: result
    //     });
    //     this.subjects = result;
    //   });

    // of(courseService.courseAdded).subscribe( (course) => { this.courses.push(course);});
    // of(courseService.courseUpdated).subscribe( (course) => { 
    //   this.courses =  this.courses.filter(x => x.id !== (course as any).id);
    //   this.courses.push(course);
    // } );
  }

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        this.id = params.id;
      });
    this.isAddMode = !this.id;

    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      // subjects: [[], Validators.required],
    });

    if (!this.isAddMode) {
      this.http.get<Course>(this.apiUrl + 'Course/' + this.id)
        .subscribe(result => {
          this.form.patchValue(result);
          this.course = result;
        });
    }
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    if (this.isAddMode) {
      this.createCourse();
    } else {
      this.updateCourse();
    }
  }


  private createCourse() {
    const toSend = { name: this.form.value.name };
    this.http.post(this.apiUrl + 'Course', toSend)
      .subscribe({
        next: () => {
          //think about rollback
          // this.http.post(this.apiUrl + 'AddSubjectsToCourse',this.selectedSubjects).subscribe({
          //   next: () => {
          //     this.router.navigate(['../'], { relativeTo: this.route });
          //   }
          // })
          this.router.navigate(['../'], { relativeTo: this.route });
        },
        error: error => {
          console.log(error);
          this.loading = false;
        }
      });
    this.form.reset();
  }

  private updateCourse() {
    if (this.form.value.name === undefined) { return }
    this.course.name = this.form.value.name;
    const toSend = { name: this.form.value.name };
    this.http.put(this.apiUrl + 'Course/' + this.id, this.course)
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
