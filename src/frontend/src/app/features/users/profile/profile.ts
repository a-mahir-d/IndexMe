import { Component, inject, OnInit, signal } from '@angular/core';
import { EditableField } from '../components/editable-field.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserService } from '../../../core/services/user.service';
import { ChangeBioCommand, ChangeDisplayNameCommand, ChangeEmailCommand, UserDto } from '../../../core/models/user.models';
import { LanguageService } from '../../../core/services/language.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-profile',
  imports: [CommonModule, FormsModule, EditableField],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit {
  private userService = inject(UserService);
  
  langService = inject(LanguageService);
  user = signal<UserDto | null>(null);
  loading = signal(true);
  errorMessage = signal<string | null>(null);

  ngOnInit() {
    this.loadProfile();
  }

  loadProfile(): void {
    this.userService.getMyInfo().subscribe({
      next: (data) => {
        this.user.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  updateField(field: keyof UserDto, value: string): void {
    this.errorMessage.set(null);
    
    let update$: Observable<void>;

    switch (field) {
      case 'email':
        const emailCmd: ChangeEmailCommand = { newEmail: value };
        update$ = this.userService.updateEmail(emailCmd);
        break;
      case 'displayName':
        const nameCmd: ChangeDisplayNameCommand = { newDisplayName: value };
        update$ = this.userService.updateDisplayName(nameCmd);
        break;
      case 'bio':
        const bioCmd: ChangeBioCommand = { newBio: value };
        update$ = this.userService.updateBio(bioCmd);
        break;
      default:
        return;
    }

    update$.subscribe({
      next: () => {
        this.user.update(u => u ? ({ ...u, [field]: value }) : null);
      },
      error: (err) => {
        this.errorMessage.set(err.error || 'Güncelleme sırasında bir hata oluştu.');
      }
    });
  }
}
