import { Deserializable } from './deserializable.model';
import { Assignment } from './assignment.model';

export class Lesson implements Deserializable {
    public id: number;
    public startDate: Date;
    public endDate: Date;
    public duration: number;
    public assignments: Assignment[];

    deserialize(input: any): this {
        Object.assign(this, input);

        //Map in the assignments
        this.assignments = input.assignments.map(assignment => new Assignment().deserialize(assignment));

        return this;
    }
    
}
