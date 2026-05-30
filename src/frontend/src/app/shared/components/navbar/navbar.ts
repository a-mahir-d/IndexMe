import { CommonModule } from '@angular/common';
import { Component, effect, inject, signal } from '@angular/core';
import { LanguageService } from '../../../core/services/language.service';
import { ThemeService } from '../../../core/services/theme.service';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  public themeService = inject(ThemeService);
  public langService = inject(LanguageService);

  isMobileMenuOpen = signal<boolean>(false);

  toggleMobileMenu(): void {
    this.isMobileMenuOpen.update(state => !state);
  }

  toggleTheme(): void {
    this.themeService.toggleTheme();
  }

  toggleLanguage(): void {
    const nextLang = this.langService.currentLang() === 'en' ? 'tr' : 'en';
    this.langService.setLanguage(nextLang);
  }
}
