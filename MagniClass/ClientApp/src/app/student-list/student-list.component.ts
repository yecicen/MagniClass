import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { SubjectListVM } from '../subject-list/subjectlistVM';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent{
  public studentList : StudentListVM[];
  public subjectId : number;
  public subject : SubjectListVM;
  public apiUrl: string;
  constructor(
    http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string,
    private route: ActivatedRoute,
    private router: Router
    ) {
      this.route.queryParams
      .subscribe(params => {
        this.subjectId = params.subjectId;
      }
    );
    let studentListUrl = 'https://localhost:44327/api/GetStudentsBySubject/'
    let SubjectUrl = 'https://localhost:44327/api/Subject/'
    if (this.subjectId) {
      studentListUrl = studentListUrl + this.subjectId;
      SubjectUrl = SubjectUrl + this.subjectId;
    }
    http.get<SubjectListVM>(SubjectUrl).subscribe(result => {
      this.subject = result;
    }, error => console.error(error));
    http.get<StudentListVM[]>(studentListUrl).subscribe(result => {
      this.studentList = result;
    }, error => console.error(error));
  }

}
interface StudentListVM {
  subjectId : number;
  subjectName : string;
  studentId : number;
  studentNumber : string;
  grade : number;
}