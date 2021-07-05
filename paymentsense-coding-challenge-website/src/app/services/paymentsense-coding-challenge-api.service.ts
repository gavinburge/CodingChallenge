import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PaymentsenseCodingChallengeApiService {
  constructor(
    @Inject('BASE_API_URL') private baseUrl: string,
    private httpClient: HttpClient) {}

  public getHealth(): Observable<string> {
    return this.httpClient.get(`${this.baseUrl}/health`, { responseType: 'text' });
  }
}
