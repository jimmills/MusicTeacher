import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { MusicTeacherAPIService } from './music-teacher-api.service';

describe('MusicTeacherAPIService', () => {
  let service: MusicTeacherAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MusicTeacherAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
