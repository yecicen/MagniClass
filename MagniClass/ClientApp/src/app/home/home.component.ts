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
    http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string, 
    private route: ActivatedRoute,
    private router: Router
    ) {
    http.get<CourseListVM[]>('https://localhost:44327/api/GetCourses').subscribe(result => {
      this.courseList = result;
    }, error => console.error(error));
  }

   navigateCourse (model : CourseListVM ){
    let route = '/subject-list';
    this.router.navigate([route], { queryParams: { id: model.courseId} });
  }
}

interface CourseListVM {
  courseId: number;
  name:string;
  numberOfTeachers:number;  
  numberOfStudents: number;
  courseAvg:number;
}

