import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LinkClicksService {
  private http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5000/api/linkclicks';

  getLinkClicks(linkId: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/get-link-clicks?linkId=${linkId}`);
  }
}