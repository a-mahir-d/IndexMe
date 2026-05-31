import { Component, EventEmitter, inject, Output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CreateLinkCommand } from '../../../core/models/link.models';
import { LanguageService } from '../../../core/services/language.service';

@Component({
  selector: 'app-add-link-modal',
  standalone: true,
  imports: [FormsModule],
  template: `
    <div class="fixed inset-0 bg-slate-900/50 flex items-center justify-center z-50 p-4">
      <div class="bg-white dark:bg-slate-900 rounded-2xl p-6 w-full max-w-md border border-slate-200 dark:border-slate-800 shadow-xl">
        <h2 class="text-xl font-bold mb-4 dark:text-white">{{ langService.translate('addLinkModal.addNewLink') }}</h2>
        
        <div class="space-y-4">
          <input [(ngModel)]="link.title" class="w-full p-2 rounded-lg border dark:bg-slate-800 dark:border-slate-700 dark:text-white">
          <input [(ngModel)]="link.url" class="w-full p-2 rounded-lg border dark:bg-slate-800 dark:border-slate-700 dark:text-white">
        </div>

        <div class="flex justify-end gap-3 mt-6">
          <button (click)="close.emit()" class="px-4 py-2 text-slate-500 hover:text-slate-700">{{ langService.translate('addLinkModal.cancel') }}</button>
          <button (click)="save.emit(link)" class="px-4 py-2 bg-indigo-600 text-white rounded-lg hover:bg-indigo-700">{{ langService.translate('addLinkModal.save') }}</button>
        </div>
      </div>
    </div>
  `
})
export class AddLinkModal {
  public langService = inject(LanguageService);

  @Output() close = new EventEmitter<void>();
  @Output() save = new EventEmitter<CreateLinkCommand>();

  
  link: CreateLinkCommand = { title: '', url: '' };
}