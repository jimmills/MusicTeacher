import { Component, OnInit, Input } from '@angular/core';
import { Lesson } from '../models/lesson.model';

@Component({
  selector: 'app-lesson',
  templateUrl: './lesson.component.html',
  styleUrls: ['./lesson.component.css']
})
export class LessonComponent implements OnInit {
  @Input() lesson:Lesson;

  constructor() { }

  ngOnInit(): void {
  }

}
