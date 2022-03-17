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

  constructor(
    private readonly http: HttpClient,
    @Inject('API_URL') private apiUrl: string,
    private route: ActivatedRoute,
    private router: Router
    ) {
      this.route.queryParams
      .subscribe(params => {
        this.subjectId = params.subjectId;
      }
    );
    let studentListUrl = this.apiUrl + 'GetStudentsBySubject/';
    if (this.subjectId) {
      studentListUrl = studentListUrl + this.subjectId;
      let SubjectUrl = this.apiUrl + 'Subject/' + this.subjectId;
      http.get<SubjectListVM>(SubjectUrl).subscribe(result => {
        this.subject = result;
      }, error => console.error(error));
    }

    http.get<StudentListVM[]>(studentListUrl).subscribe(result => {
      this.studentList = result;
    }, error => console.error(error));
  }

  
  createStudent() {
    let route = '/subject';
    this.router.navigate([route]);
  }
  addStudentToSubject(){

  }
  editStudent(model: StudentListVM) {
    let route = '/student';
    this.router.navigate([route], { queryParams: { id: model.studentId } });
  }
  editGrade(model: StudentListVM) {
    let route = '/grade';
    this.router.navigate([route], { queryParams: { id: model.gradeId } });
  }

  deleteStudent(model: StudentListVM) {
    this.http.delete(this.apiUrl + 'Student/' + model.studentId).subscribe(result => {
      console.log(result);
      this.studentList = this.studentList.filter(x => x.studentId != model.studentId)
    }, error => console.error(error));
    let route = '/student-list';
    this.router.navigate([route], { queryParams: { id: model.subjectId } });
  }
}
interface StudentListVM {
  subjectId : number;
  subjectName : string;
  studentId : number;
  studentNumber : string;
  grade : number;
  gradeId : number;
}