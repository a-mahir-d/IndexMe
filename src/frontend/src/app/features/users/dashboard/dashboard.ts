import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { LanguageService } from '../../../core/services/language.service';
import { UserService } from '../../../core/services/user.service';
import { UserDto } from '../../../core/models/user.models';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  public langService = inject(LanguageService);
  private userService = inject(UserService);

  user = signal<UserDto | null>(null);
  loading = signal(true);

  ngOnInit() {
    this.userService.getMyInfo().subscribe({
      next: (data) => {
        this.user.set(data);
        this.loading.set(false);
      }
    });
  }
}
