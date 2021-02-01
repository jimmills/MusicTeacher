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
      //this.dataSvc.getLessonsByStudent(this.student.id).subscribe(lessons => this.lessons = lessons)
      this.dataSvc.getLessonsByStudent(this.student.id).subscribe(lessons => { 
        this.lessons = this.sortLessonByDateDesc(lessons); 
      })
      
    }
  }

  addLesson(lesson: Lesson) {
    //post the new lesson back to the api, which will return a new lesson object
    this.dataSvc.addLessonToStudent(this.student, lesson)
    .subscribe(lesson => { 
      this.lessons =  this.sortLessonByDateDesc([...this.lessons, lesson]); //Add the lesson to the lessons collection
    }); 
  }

  deleteLesson(id: number) {
    if(confirm("Are you sure you want to delete this lesson?")){
      //delete the lesson
      this.dataSvc.deleteLesson(id).subscribe(

        //remove lesson from the collection - future refactor: lazy load lessons and reload collection instead of mutating it
        () => this.lessons = this.lessons.filter(lesson => lesson.id !== id)      
      );
    }
  }

  private sortLessonByDateDesc(lessons :Lesson[]){
    return lessons.sort((a,b) =>  b.startDate.valueOf() - a.startDate.valueOf());
  }

}
