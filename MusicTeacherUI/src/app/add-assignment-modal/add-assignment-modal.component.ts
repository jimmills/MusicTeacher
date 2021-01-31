import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Assignment } from '../models/assignment.model';

@Component({
  selector: 'app-add-assignment-modal',
  templateUrl: './add-assignment-modal.component.html',
  styleUrls: ['./add-assignment-modal.component.css']
})
export class AddAssignmentModalComponent {
  @Input() assignment:Assignment;
  @Output() onAddAssignment = new EventEmitter<Assignment>();

  description:string;
  notes:string;

  constructor(private modalService: NgbModal) { }

  open(content) {
    this.modalService.open(content).result.then(
      (result) => {
      this.saveAssignment()}
    , (reason) => { 
      //nothing special to do on dismiss
    });
  }

  saveAssignment(){
    //Prepare the Assignment object <-- probably add validation here too
    let assignment = new Assignment();
    assignment.description = this.description;
    assignment.practiceNotes = this.notes;

    //Raise event
    this.onAddAssignment.emit(assignment);
  }

}
