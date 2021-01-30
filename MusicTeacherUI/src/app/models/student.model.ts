import { Deserializable } from './deserializable.model';
//import { Lesson } from './lesson.model';

export class Student implements Deserializable {
    public id: number;
    public firstName: string;
    public lastName: string;
    public instrument: string;
    public lessonWindow: string;
    //public lessons: Lesson[];

    deserialize(input: any): this {
        return Object.assign(this, input);

        //Map in the lessons -- may not be necessary
        //this.lessons = input.lessons.map(lesson => new Lesson().deserialize(lesson));
    }

    getFullName() {
        return this.firstName + ' ' + this.lastName;
    }
}
