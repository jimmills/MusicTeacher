import { Component, OnInit, Input } from '@angular/core';
import { Student } from '../models/student.model';
import { StateService } from '../services/state.service';
import { MusicTeacherAPIService } from '../services/music-teacher-api.service';
import { Lesson } from '../models/lesson.model';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  public student: Student;
  public lessons: Lesson[];

  constructor(private state: StateService, private dataSvc: MusicTeacherAPIService) { }

  ngOnInit(): void {
    this.student = this.state.SelectedStudent;
    if(this.student){
      this.dataSvc.getLessonsByStudent(this.student.id).subscribe(lessons => this.lessons = lessons);
    }
  }

  addLesson() {
    window.alert('add the lesson already!');
  }

}
