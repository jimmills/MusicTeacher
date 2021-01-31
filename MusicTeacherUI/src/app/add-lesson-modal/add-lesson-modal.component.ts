import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbModal, NgbDateStruct, NgbCalendar, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { Lesson } from '../models/lesson.model';
import { Student } from '../models/student.model';

@Component({
  selector: 'app-add-lesson-modal',
  templateUrl: './add-lesson-modal.component.html',
  styleUrls: ['./add-lesson-modal.component.css']
})
export class AddLessonModalComponent {
  @Input() student:Student;
  @Output() onAddLesson = new EventEmitter<Lesson>();
  date:NgbDateStruct;
  startTime:NgbTimeStruct;
  duration:number;

  constructor(private modalService: NgbModal, private calendar: NgbCalendar) { }

  open(content) {
    this.date = this.calendar.getToday();
    this.startTime = { hour:12, minute: 0, second: 0 };
    this.duration = 30;


    this.modalService.open(content).result.then(
      (result) => {
      this.saveLesson()}
    , (reason) => { 
      //nothing special to do on dismiss
    });
  }

  saveLesson(){
    //Prepare the Lesson object
    let newLesson:Lesson = new Lesson();
    let date = new Date();
    newLesson.startDate = new Date(this.date.year, this.date.month-1, this.date.day, this.startTime.hour, this.startTime.minute);
    newLesson.endDate = new Date(newLesson.startDate.getTime() + this.duration*60000);

    //Raise event
    this.onAddLesson.emit(newLesson);
  }

}
