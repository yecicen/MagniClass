import {Injectable} from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel, Subject } from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { Course } from '../models/course';

@Injectable({providedIn: 'root'})
export class CourseService{
    public connection: HubConnection;
    courseAdded = new Subject<any>();
    courseUpdated = new Subject<any>();

    constructor(

    ){
        this.connection = new HubConnectionBuilder()
            .withUrl('https://localhost:44327/api/Course')
            .configureLogging(LogLevel.Information)
            .build(); 

        this.registerEvents();
        this.connection.start().catch(err => console.error(err));
    }

    private registerEvents(){
        this.connection.on('courseAdded', item =>{ this.courseAdded.next(item)});
        this.connection.on('courseUpdated', item =>{ this.courseUpdated.next(item)});
    }

}