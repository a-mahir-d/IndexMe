import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChangeBioCommand, ChangeDisplayNameCommand, ChangeEmailCommand, UserDto } from '../models/user.models';

@Injectable({ providedIn: 'root' })
export class UserService {
  private http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5000/api/users';
  
  getMyInfo(): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.baseUrl}/get-my-info`);
  }

  updateEmail(command: ChangeEmailCommand): Observable<void> {
    return this.http.patch<void>(`${this.baseUrl}/change-email`, command );
  }

  updateDisplayName(command: ChangeDisplayNameCommand): Observable<void> {
    return this.http.patch<void>(`${this.baseUrl}/change-display-name`, command );
  }

  updateBio(command: ChangeBioCommand): Observable<void> {
    return this.http.patch<void>(`${this.baseUrl}/change-bio`, command );
  }
}