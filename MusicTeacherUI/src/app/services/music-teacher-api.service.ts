import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import {catchError, map} from 'rxjs/operators';
import { environment } from '../../environments/environment'

import { Student } from '../models/student.model';
import { Lesson } from '../models/lesson.model';
import { Assignment } from '../models/assignment.model';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class MusicTeacherAPIService {


  constructor(private http: HttpClient) { }

  public getStudents(): Observable<Student[]> {

    return this.http.get<Student[]>(environment.apiEndpoint +`/Student`).pipe(
      map(data => data.map(data => new Student().deserialize(data))),
      catchError(() => throwError('Students unavailable'))
    );

  }

  public getLessonsByStudent(studentid: number): Observable<Lesson[]> {
    return this.http.get<Lesson[]>(environment.apiEndpoint +`/LessonPlan/Student/${studentid}`).pipe(
      map(data => data.map(data => new Lesson().deserialize(data))),
      catchError(() => throwError('Lessons unavailable'))
    );
  }

  public addLessonToStudent(student: Student, lesson: Lesson) : Observable<Lesson>{
    const postData = {
      lessonID: 0,
      studentID: student.id,
      startDate: lesson.startDate,
      endDate: lesson.endDate
    }
    return this.http.post<Lesson>(environment.apiEndpoint +`/LessonPlan`, postData, httpOptions).pipe(
       map(data => new Lesson().deserialize(data)),
       catchError(() => throwError('Failed to add new lesson'))
    );
  }

  public deleteLesson(id: number) : Observable<{}>{
    return this.http.delete(environment.apiEndpoint + `/LessonPlan/${id}`, httpOptions)
    .pipe(
      catchError(() => throwError('Failed to delete lesson'))
    );
  }

  public addAssignmentToLesson(lesson: Lesson, assignment: Assignment) : Observable<Assignment>{
    const postData = {
      assignmentID: 0,
      lessonID: lesson.id,
      description: assignment.description,
      practiceNotes: assignment.practiceNotes
    }
    return this.http.post<Lesson>(environment.apiEndpoint + `/LessonPlan/Assignment`, postData, httpOptions).pipe(
       map(data => new Assignment().deserialize(data)),
       catchError(() => throwError('Failed to add new lesson'))
    );
  }

  public deleteAssignment(id: number) : Observable<{}>{
    return this.http.delete(environment.apiEndpoint + `/LessonPlan/Assignment/${id}`, httpOptions)
    .pipe(
      catchError(() => throwError('Failed to delete assignment'))
    );
  }

}
