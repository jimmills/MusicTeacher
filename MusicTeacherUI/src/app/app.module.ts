import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StudentComponent } from './student/student.component';
import { HomeComponent } from './home/home.component';
import { LessonComponent } from './lesson/lesson.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AddLessonModalComponent } from './add-lesson-modal/add-lesson-modal.component';
import { AddAssignmentModalComponent } from './add-assignment-modal/add-assignment-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    StudentComponent,
    HomeComponent,
    LessonComponent,
    AddLessonModalComponent,
    AddAssignmentModalComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
