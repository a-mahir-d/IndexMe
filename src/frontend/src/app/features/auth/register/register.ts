import { Component, inject } from '@angular/core';
import { LanguageService } from '../../../core/services/language.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [CommonModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  public langService = inject(LanguageService);
  private router = inject(Router);

  navigateToHome(): void {
    this.router.navigate(['/']);
  }
}
