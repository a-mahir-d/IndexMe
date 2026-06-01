import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChangeDisplayOrderCommand, ChangeTitleCommand, ChangeUrlCommand, CreateLinkCommand } from '../models/link.models';

@Injectable({
  providedIn: 'root'
})
export class LinkService {
  private http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5000/api/links';

  createLink(command: CreateLinkCommand): Observable<any> {
    return this.http.post(`${this.baseUrl}/create`, command);
  }

  trackLink(id: string): Observable<string> {
    const params = new HttpParams().set('id', id);
    return this.http.get(`${this.baseUrl}/track-link`, { params, responseType: 'text' });
  }

  changeDisplayOrder(command: ChangeDisplayOrderCommand): Observable<any> {
    return this.http.patch(`${this.baseUrl}/change-display-order`, command);
  }

  changeTitle(command: ChangeTitleCommand): Observable<any> {
    return this.http.patch(`${this.baseUrl}/change-title`, command);
  }

  changeUrl(command: ChangeUrlCommand): Observable<any> {
    return this.http.patch(`${this.baseUrl}/change-url`, command);
  }
}