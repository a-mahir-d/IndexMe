import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChangeDisplayOrderCommand, CreateLinkCommand } from '../models/link.models';

@Injectable({
  providedIn: 'root'
})
export class LinkService {
  private http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5000/api/links';

  createLink(command: CreateLinkCommand): Observable<any> {
    return this.http.post(`${this.baseUrl}/create`, command);
  }

  changeDisplayOrder(command: ChangeDisplayOrderCommand): Observable<any> {
    return this.http.post(`${this.baseUrl}/create`, command);
  }
}