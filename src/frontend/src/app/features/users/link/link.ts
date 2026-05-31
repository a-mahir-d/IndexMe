import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LinkClicksService } from '../../../core/services/link-click.service';
import { ClickWithCountry, LinkClickDto } from '../../../core/models/link_click.models';
import { LinkService } from '../../../core/services/link.service';
import { EditableField } from "../components/editable-field.component";
import { UAParser } from 'ua-parser-js';
import { LanguageService } from '../../../core/services/language.service';

@Component({
  selector: 'app-link',
  imports: [CommonModule, EditableField],
  templateUrl: './link.html',
  styleUrl: './link.css',
})
export class Link implements OnInit {
  private router = inject(Router);
  private clickService = inject(LinkClicksService);
  private linkService = inject(LinkService);
  langService = inject(LanguageService);

  linkData = signal<any>(null);
  clicks = signal<ClickWithCountry[]>([]);
  errorMessage = signal<string | null>(null);
  loading = signal(true);

  ngOnInit() {
    const state = history.state;
    if (!state?.['linkData']) {
      this.router.navigate(['/dashboard']);
      return;
    }
    this.linkData.set(state['linkData']);
    this.loadClicks();
  }

  updateLinkField(field: 'title' | 'url', value: string) {
    this.errorMessage.set(null);
    
    const update$ = field === 'title' 
      ? this.linkService.changeTitle({ linkId: this.linkData().id, newTitle: value })
      : this.linkService.changeUrl({ linkId: this.linkData().id, newUrl: value });

    update$.subscribe({
      next: () => {
        this.linkData.update(l => ({ ...l, [field]: value }));
      },
      error: (err) => this.errorMessage.set(err.error || 'Güncelleme hatası')
    });
  }

  loadClicks() {
    this.clickService.getLinkClicks(this.linkData().id).subscribe({
      next: async (data) => {
        const processedClicks = await Promise.all(data.map(async (click: any) => ({
          ...click,
          countryCode: await this.getCountryFromIp(click.ipAddress)
        })));

        this.clicks.set(processedClicks);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  get userAgentStats() {
    const allClicks = this.clicks();
    if (allClicks.length === 0) return [];

    const counts: Record<string, number> = {};
    
    allClicks.forEach(c => {
      const parser = new UAParser(c.userAgent || '');
      const result = parser.getResult();
      
      const os = result.os.name || '?';
      counts[os] = (counts[os] || 0) + 1;
    });

    return Object.entries(counts).map(([name, count]) => ({
      name,
      count,
      percentage: Math.round((count / allClicks.length) * 100)
    })).sort((a, b) => b.count - a.count);
  }

  getBrowserFromUA(ua: string | null): string {
    if (!ua) return 'Bilinmiyor';
    const parser = new UAParser(ua);
    const browser = parser.getBrowser();
    return browser.name ? `${browser.name} | Version: ${browser.major || ''}` : '?';
  }

  get countryStats() {
    const allClicks = this.clicks();
    if (allClicks.length === 0) return [];

    const counts: Record<string, number> = {};
    allClicks.forEach(c => {
      const code = c.countryCode || 'XX';
      counts[code] = (counts[code] || 0) + 1;
    });

    return Object.entries(counts).map(([code, count]) => ({
      name: code,
      count,
      percentage: Math.round((count / allClicks.length) * 100)
    })).sort((a, b) => b.count - a.count);
}

  async getCountryFromIp(ip: string): Promise<string> {
    try {
      const response = await fetch(`http://ip-api.com/json/${ip}`);
      const data = await response.json();
      return data.countryCode || 'XX';
    } catch {
      return 'XX';
    }
  }

  getFlagEmoji(countryCode: string) {
    if (!countryCode || countryCode === 'XX') return '🌍';
    return countryCode.toUpperCase().replace(/./g, char => 
      String.fromCodePoint(127397 + char.charCodeAt(0))
    );
  }
}
