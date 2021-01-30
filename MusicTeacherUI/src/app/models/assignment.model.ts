import { Deserializable } from './deserializable.model';

export class Assignment implements Deserializable {
    public id: number;
    public description: string;
    public practiceNotes: string;

    deserialize(input: any): this {
        Object.assign(this, input);

        return this;
    }
}
