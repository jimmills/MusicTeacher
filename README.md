# Music Teacher Project

## Description
This is a simple project to manage lessons and associated assignments for a music teacher and his/her students. Mostly this is a sample project, and you will discover a fair amount of missing functionality. For example, you cannot create new students. That's not because adding students isn't important, it's just because I haven't had time to add that feature.

The project consists of an API built with .NET 5.0 and a client-side application built in Angular 11.

If you want to know what all the API can do. I recommend checking out the controllers. Or, better yet, pull it downs, run the code, and check out the swagger page.

## Installation
Mostly this is clone a go, but not completely. You need to do a little work to set up the SQLite db.

   * Grab the DB creation script, which can be found here: MusicTeacher/dbscripts/DBCreate-SQLite.txt
   * Use a SQLite command line tool to create the db in your favorite file location. This will create the tables and a little bit of test data.
   * Update MusicTeacherDB connection string in appsetting.json to your new database file
   * That should be all it takes. Hit run (assuming you have an IDE that has a run for .Net 5 apps) and it "should" work
   * Once the API is up and running, point your console at the MusicTeachUI folder and run "ng serve --open". You'll need to have the angular CLI installed.

## Dependencies
   * .NET 5 framework installed
   * Node/NPM
   * Angular CLI
   * TypeScript 4

## Notes/Assumptions
While probably not an extensive list, here are some notes about the project and assumptions I made while building it:
  
  1. The purpose of the application at this time is for a single music teacher to create lesson plans for their students. The lesson plans are composed of a list of assignments.
  2. The teacher only teaches 1 student at a time, and the lesson plan created is applicable only to the 1 student.
  3. Students are allowed to see and even edit the lesson plans of other students. We're high trust around here.
  4. There is minimal data validation and exception handling. If you want to break it, you probably can. So, be cool.
  5. The API's do not page data yet
  6. The unit test project is structured to match the API, so it should be easy to piece them together. The test are all AAA style.
  7. There are post methods that currently have an Id as an input property. These are ignored. You cannot create your own IDs.

### Additional information about the front end application found in the MusicTeacherUI readme
