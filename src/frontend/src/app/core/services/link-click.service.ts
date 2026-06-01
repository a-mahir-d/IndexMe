import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LinkClicksService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.serverUrl}/linkclicks`;

  getLinkClicks(linkId: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/get-link-clicks?linkId=${linkId}`);
  }
}