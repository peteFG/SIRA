import { Injectable } from '@angular/core';
import { BackendService } from './backend.service';
import { BehaviorSubject, Observable } from 'rxjs';

export interface Sample {
  sampleId: any;
  balance: any;
  id: any;
}

@Injectable({ providedIn: 'root' })
export class SampleService {
  private sampleList$ = new BehaviorSubject<Sample[]>([]);
  public sampleList = this.sampleList$.asObservable();

  constructor(private backendService: BackendService) {}

  public getSamples() {
    this.backendService
      .sample('ListSamples')
      .then((res) => this.sampleList$.next(res as Sample[]));
  }
}
