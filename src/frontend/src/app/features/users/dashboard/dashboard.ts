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
    const dummyUser: UserDto = {
      id: '550e8400-e29b-41d4-a716-446655440000',
      username: 'ahmetmahir',
      email: 'ahmet@example.com',
      displayName: 'Ahmet Mahir Demirelli',
      bio: 'Software Developer | .NET & Angular Enthusiast | Woodworking DIYer',
      createdAt: new Date('2026-01-15T09:00:00Z'),
      links: [
        {
          id: 'l1',
          userId: '550e8400-e29b-41d4-a716-446655440000',
          title: 'Portfolio Website',
          url: 'https://ahmetmahirdemirelli.com',
          displayOrder: 1,
          createdAt: new Date(),
          clickCount: 2
        },
        {
          id: 'l2',
          userId: '550e8400-e29b-41d4-a716-446655440000',
          title: 'GitHub Profile',
          url: 'https://github.com/ahmetmahirdemirelli',
          displayOrder: 2,
          createdAt: new Date(),
          clickCount: 0
        },
        {
          id: 'l3',
          userId: '550e8400-e29b-41d4-a716-446655440000',
          title: 'DIY Woodworking Blog',
          url: 'https://diy.ahmetmahirdemirelli.com',
          displayOrder: 3,
          createdAt: new Date(),
          clickCount: 15
        }
      ]
    };

    // Veriyi doğrudan atıyoruz
    this.user.set(dummyUser);
    this.loading.set(false);
  }
}
