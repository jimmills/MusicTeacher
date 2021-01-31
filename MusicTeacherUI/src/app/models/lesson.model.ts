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
        //Type the dates or they'll be strings
        this.startDate = new Date(this.startDate);
        this.endDate = new Date(this.endDate);

        //Map in the assignments
        this.assignments = input.assignments.map(assignment => new Assignment().deserialize(assignment));

        return this;
    }

    getStartDatePrettyString(){
        return this.prettyDateString(this.startDate);
    }

    getEndDatePrettyString(){
        return this.prettyDateString(this.endDate);
    }

    private prettyDateString(date :Date) {
        //January 27, 2021 01:13 AM
        if(date){
            return date.toLocaleDateString(
                'en-US',
                {
                  year: 'numeric',
                  month: 'long',
                  day: 'numeric'
                }
              )+ ' ' +
              date.toLocaleTimeString('en-us',{
                hour: '2-digit',
                minute: '2-digit'
              });
        } else {
            return '';
        }
    }    
}
