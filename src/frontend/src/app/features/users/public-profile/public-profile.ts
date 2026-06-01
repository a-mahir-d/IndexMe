import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LinkService } from '../../../core/services/link.service';
import { UserService } from '../../../core/services/user.service';
import { UserPublicDto } from '../../../core/models/user.models';
import { LanguageService } from '../../../core/services/language.service';

@Component({
  selector: 'app-public-profile',
  imports: [],
  templateUrl: './public-profile.html',
  styleUrl: './public-profile.css',
})
export class PublicProfile implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private profileService = inject(LinkService);
  private userService = inject(UserService);

  langService = inject(LanguageService);
  userProfile = signal<UserPublicDto | null>(null);
  isLoading = signal<boolean>(true);
  errorMessage = signal<string | null>(null);

  ngOnInit(): void {
    const username = this.route.snapshot.paramMap.get('username');
    
    if (username) {
      this.fetchProfile(username);
    } 
    else {
      this.router.navigate(['/']);
    }
  }

  private fetchProfile(username: string): void {
    this.userService.getUserInfo(username).subscribe({
      next: (data) => {
        if (data && data.links) {
          data.links.sort((a, b) => a.displayOrder - b.displayOrder);
        }
        this.userProfile.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        this.errorMessage.set('Kullanıcı bulunamadı veya bir hata oluştu.');
        this.isLoading.set(false);
      }
    });
  }

  onLinkClick(linkId: string): void {
    this.profileService.trackLink(linkId).subscribe({
      next: (targetUrl) => {
        if (targetUrl) {
          window.open(targetUrl, '_blank');
        }
      },
      error: (err) => {
        console.error('Link takibi esnasında hata oluştu:', err);
      }
    });
  }
}