import { Component, OnInit } from '@angular/core';
//import { Observable } from 'rxjs';
import { MusicTeacherAPIService } from '../services/music-teacher-api.service';
import { StudentListItemComponent } from '../student-list-item/student-list-item.component';
import { Student } from '../models/student.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public students: Student[];

  constructor(private dataSvc: MusicTeacherAPIService) { }

  ngOnInit(): void {
    this.dataSvc.getStudents().subscribe(students => this.students = students);
  }

}
