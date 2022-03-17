import { Component, Inject } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { SubjectListVM } from './subjectlistVM';
import { HttpClient } from '@angular/common/http';
import { Course } from '../models/course';
import { Subject } from '../models/subject';
@Component({
  selector: 'app-subject-list',
  templateUrl: './subject-list.component.html',
  styleUrls: ['./subject-list.component.css']
})
export class SubjectListComponent {
  public subjectList: SubjectListVM[];
  public courseId: string;
  public course : Course;
  // public subject: Subject;
  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('API_URL') private apiUrl: string,
  ) {
    this.route.queryParams
      .subscribe(params => {
        this.courseId = params.id;
      });

    let subjectByCourseUrl = this.apiUrl + 'GetSubjectsByCourse/';
    if (this.courseId) {
      subjectByCourseUrl = subjectByCourseUrl  + this.courseId;
      let courseServiceUrl = this.apiUrl + 'Course/'+ this.courseId;
      this.http.get<Course>(courseServiceUrl).subscribe(result => {
        this.course = result;
      }, error => console.error(error));
    }
    this.http.get<SubjectListVM[]>(subjectByCourseUrl).subscribe(result => {
      this.subjectList = result;
    }, error => console.error(error));
  }

  createSubject() {
    let route = '/subject';
    this.router.navigate([route]);
  }
  addSubjectToCourse(){

  }
  editSubject(model: SubjectListVM) {
    let route = '/subject';
    this.router.navigate([route], { queryParams: { id: model.subjectId } });
  }
  viewSubject(model: SubjectListVM) {
    let route = '/student-list';
    this.router.navigate([route], { queryParams: { subjectId: model.subjectId } });
  }
  deleteSubject(model: SubjectListVM) {
    this.http.delete(this.apiUrl + 'Subject/' + model.subjectId).subscribe(result => {
      console.log(result);
      this.subjectList = this.subjectList.filter(x => x.subjectId != model.subjectId)
    }, error => console.error(error));
    let route = '/subject-list';
    this.router.navigate([route], { queryParams: { id: model.courseId } });
  }

}

