import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public courseList: CourseListVM[];
  constructor(
    @Inject('API_URL') private apiUrl: string,
    private route: ActivatedRoute,
    private router: Router,
    private readonly http: HttpClient
  ) {
    this.http.get<CourseListVM[]>(apiUrl + 'GetCourses').subscribe(result => {
      this.courseList = result;
    }, error => console.error(error));
  }

  viewCourse(model: CourseListVM) {
    let route = '/subject-list';
    this.router.navigate([route], { queryParams: { id: model.courseId } });
  }
  createCourse() {
    let route = '/course';
    this.router.navigate([route]);
  }
  editCourse(model: CourseListVM) {
    let route = '/course';
    this.router.navigate([route], { queryParams: { id: model.courseId } });
  }
  deleteCourse(model: CourseListVM) {
    this.http.delete(this.apiUrl + 'Course/' + model.courseId).subscribe(result => {
      console.log(result);
      this.courseList = this.courseList.filter(x => x.courseId != model.courseId)
    }, error => console.error(error));
    let route = '/';
    this.router.navigate([route], { queryParams: { id: model.courseId } });
  }
}

interface CourseListVM {
  courseId: number;
  name: string;
  numberOfTeachers: number;
  numberOfStudents: number;
  courseAvg: number;
}

