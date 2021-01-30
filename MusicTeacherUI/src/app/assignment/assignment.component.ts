import { Component, OnInit, Input } from '@angular/core';
import { Assignment } from '../models/assignment.model';

@Component({
  selector: 'app-assignment',
  templateUrl: './assignment.component.html',
  styleUrls: ['./assignment.component.css']
})
export class AssignmentComponent implements OnInit {
  @Input() assignment:Assignment;

  constructor() { }

  ngOnInit(): void {
  }

}
