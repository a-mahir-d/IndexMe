import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { LanguageService } from '../../../core/services/language.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  public langService = inject(LanguageService);
  private router = inject(Router);

  navigateToRegister(): void {
    this.router.navigate(['/register']);
  }
}
