import { Injectable, inject, signal } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChangeBioCommand, ChangeDisplayNameCommand, ChangeEmailCommand, UserDto, UserPublicDto } from '../models/user.models';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.serverUrl}/users`;
  
  getMyInfo(): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.baseUrl}/get-my-info`);
  }

  getUserInfo(username: string): Observable<UserPublicDto> {
    const params = new HttpParams().set('username', username);
    return this.http.get<UserPublicDto>(`${this.baseUrl}/get-user-info`, { params });
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