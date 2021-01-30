import { Injectable } from '@angular/core';
import { Student } from '../models/student.model';

@Injectable({
  providedIn: 'root'
})
export class StateService {
  private selectedStudent: Student;

  constructor() { }

  set SelectedStudent(student: Student) {
    this.selectedStudent = student;
  }

  get SelectedStudent() : Student {
    return this.selectedStudent;
  }
}
