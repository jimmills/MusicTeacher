import { Student } from './student.model';

describe('Student.Model', () => {
  it('should create an instance', () => {
    expect(new Student()).toBeTruthy();
  });
});
