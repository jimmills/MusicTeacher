import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import {catchError, map} from 'rxjs/operators';

import { Student } from '../models/student.model';
import { Lesson } from '../models/lesson.model';
import { Assignment } from '../models/assignment.model';

@Injectable({
  providedIn: 'root'
})
export class MusicTeacherAPIService {
  constructor(private http: HttpClient) { }

  public getStudents(): Observable<Student[]> {

    return this.http.get<Student[]>(`https://localhost:5001/Student`).pipe(
      map(data => data.map(data => new Student().deserialize(data))),
      catchError(() => throwError('Students unavailable'))
    );

  }

  public getLessonsByStudent(studentid: number): Observable<Lesson[]> {
    return this.http.get<Lesson[]>(`https://localhost:5001/LessonPlan/Student/${studentid}`).pipe(
      map(data => data.map(data => new Lesson().deserialize(data))),
      catchError(() => throwError('Lessons unavailable'))
    );
  }

}
