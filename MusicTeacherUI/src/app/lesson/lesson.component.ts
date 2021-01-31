import { Component, OnInit, Input } from '@angular/core';
import { MusicTeacherAPIService } from '../services/music-teacher-api.service';
import { Lesson } from '../models/lesson.model';
import { Assignment } from '../models/assignment.model';
import { IfStmt } from '@angular/compiler';

@Component({
  selector: 'app-lesson',
  templateUrl: './lesson.component.html',
  styleUrls: ['./lesson.component.css']
})
export class LessonComponent implements OnInit {
  @Input() lesson:Lesson;

  constructor(private dataSvc: MusicTeacherAPIService) { }

  ngOnInit(): void {

  }

  addAssignment(assignment: Assignment) {
    //post the new assignment back to the api, which will return a new assignment object
    this.dataSvc.addAssignmentToLesson(this.lesson, assignment)
    .subscribe(assignment => this.lesson.assignments = [...this.lesson.assignments, assignment]); //add assignment to the collection
  }

  deleteAssignment(id: number) {
    if(confirm("Are you sure you want to delete this assignment?")){
      //delete the assignment
      this.dataSvc.deleteAssignment(id).subscribe(

        //remove assignment from the collection - future refactor: lazy load assignments and reload collection instead of mutating it
        () => this.lesson.assignments = this.lesson.assignments.filter(assignment => assignment.id !== id)      
      );
    }
  }
}
