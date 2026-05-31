import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { LanguageService } from '../../../core/services/language.service';
import { UserService } from '../../../core/services/user.service';
import { UserDto } from '../../../core/models/user.models';
import { LinkService } from '../../../core/services/link.service';
import { ChangeDisplayOrderCommand, CreateLinkCommand, LinkDto } from '../../../core/models/link.models';
import { AddLinkModal } from '../components/add-link-modal.component'; 
import { CdkDragDrop, DragDropModule, moveItemInArray } from '@angular/cdk/drag-drop';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, AddLinkModal, DragDropModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  public langService = inject(LanguageService);
  private userService = inject(UserService);
  private linkService = inject(LinkService);
  private router = inject(Router);
  
  isModalOpen = signal(false);
  user = signal<UserDto | null>(null);
  loading = signal(true);
  errorMessage: string = '';

  ngOnInit() {
    this.loadUserData();
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
    this.userService.getMyInfo().subscribe({
      next: (data) => {
        const sortedLinks = [...data.links].sort((a, b) => a.displayOrder - b.displayOrder);
        this.user.set({
          ...data,
          links: sortedLinks
        });
        this.loading.set(false);
      }
    });
  }

  onDrop(event: CdkDragDrop<any[]>) {
    const prevIndex = event.previousIndex;
    const currentIndex = event.currentIndex;

    if (prevIndex === currentIndex) return;
    const originalLinks = [...this.user()!.links];

    const links = [...originalLinks];
    moveItemInArray(links, prevIndex, currentIndex);
    
    const updatedLinks = links.map((link, index) => ({
      ...link,
      displayOrder: index + 1
    }));

    this.user.update(u => u ? ({ ...u, links: updatedLinks }) : null);

    const movedLink = updatedLinks[currentIndex];
    const command: ChangeDisplayOrderCommand = {
      linkId: movedLink.id,
      newDisplayOrder: currentIndex + 1
    };

    this.linkService.changeDisplayOrder(command).subscribe({
      error: (err) => {
        this.user.update(u => u ? ({ ...u, links: originalLinks }) : null);
        this.errorMessage = err.error || 'Sıralama güncellenemedi, lütfen tekrar deneyin.';
      }
    });
  }

  navigateToLink(link: LinkDto): void {
    this.router.navigate(['/link'], { queryParams: { id: link.id }, state: { linkData: link } });
  }
}
