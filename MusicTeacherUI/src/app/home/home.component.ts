import { Component, OnInit } from '@angular/core';
import { MusicTeacherAPIService } from '../services/music-teacher-api.service';
import { StateService } from '../services/state.service';
import { Student } from '../models/student.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public students: Student[];

  constructor(private dataSvc: MusicTeacherAPIService, private state: StateService) { }

  ngOnInit(): void {
    this.dataSvc.getStudents().subscribe(students => this.students = students);
  }

  setSelectedStudent(student: Student) {
    this.state.SelectedStudent = student;
  }

}
