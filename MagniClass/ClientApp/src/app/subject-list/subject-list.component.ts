import { Component } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { SubjectListVM } from './subjectlistVM';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-subject-list',
  templateUrl: './subject-list.component.html',
  styleUrls: ['./subject-list.component.css']
})
export class SubjectListComponent {
  public subjectList: SubjectListVM[];
  public courseId: string;
  public apiUrl: string;
  public subject : SubjectListVM;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
  ) {
    this.route.queryParams
      .subscribe(params => {
        this.courseId = params.id;
      });
    this.apiUrl = 'https://localhost:44327/api/';
    let subjectByCourseUrl = this.apiUrl + 'GetSubjectsByCourse/';
    if (this.courseId) {
      subjectByCourseUrl = subjectByCourseUrl  + this.courseId;
      let courseServiceUrl = this.apiUrl + 'Course/'+ this.courseId;
      this.http.get<SubjectListVM>(courseServiceUrl).subscribe(result => {
        this.subject = result;
      }, error => console.error(error));
    }
    this.http.get<SubjectListVM[]>(subjectByCourseUrl).subscribe(result => {
      this.subjectList = result;
    }, error => console.error(error));
  }


  navigateSubject(model: SubjectListVM) {
    let route = '/student-list';
    this.router.navigate([route], { queryParams: { subjectId: model.subjectId } });
  }

}

