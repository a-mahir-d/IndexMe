import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { LanguageService } from '../../../core/services/language.service';
import { UserService } from '../../../core/services/user.service';
import { UserDto } from '../../../core/models/user.models';
import { LinkService } from '../../../core/services/link.service';
import { CreateLinkCommand } from '../../../core/models/link.models';
import { AddLinkModal } from '../components/add-link-modal.component'; 

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, AddLinkModal],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  public langService = inject(LanguageService);
  private userService = inject(UserService);
  private linkService = inject(LinkService);
  
  isModalOpen = signal(false);
  user = signal<UserDto | null>(null);
  loading = signal(true);
  errorMessage: string = '';

  ngOnInit() {
    this.userService.getMyInfo().subscribe({
      next: (data) => {
        this.user.set(data);
        this.loading.set(false);
      }
    });
  }

  openAddLinkModal() { this.isModalOpen.set(true); }

  handleCreateLink(command: CreateLinkCommand) {
    this.errorMessage = ''; 

    this.linkService.createLink(command).subscribe({
      next: () => {
        this.isModalOpen.set(false);
        this.loadUserData();
      },
      error: (err) => {
        this.isModalOpen.set(false);
        this.errorMessage = (err.error || 'Bir hata oluştu, lütfen tekrar deneyin.');
      }
    });
  }

  loadUserData(): void {
    this.userService.getMyInfo().subscribe(data => this.user.set(data));
  }
}
