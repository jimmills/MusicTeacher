import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { StateService } from '../services/state.service';
import { Student } from '../models/student.model';


@Component({
  selector: 'app-student-list-item',
  templateUrl: './student-list-item.component.html',
  styleUrls: ['./student-list-item.component.css']
})
export class StudentListItemComponent implements OnInit {
  @Input() student:Student;

  constructor(private state: StateService) { }

  ngOnInit(): void {
  }

  setSelectedStudent(student: Student) {
    this.state.SelectedStudent = student;
  }

}
