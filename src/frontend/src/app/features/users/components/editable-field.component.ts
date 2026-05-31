import { CommonModule } from "@angular/common";
import { Component, EventEmitter, inject, Input, Output } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { LanguageService } from "../../../core/services/language.service";

@Component({
  selector: 'app-editable-field',
  standalone: true,
  imports: [FormsModule, CommonModule],
  template: `
    <div>
      <label class="text-xs font-bold text-slate-500 uppercase">{{ label }}</label>
      <div class="flex items-center gap-2 mt-1">
        @if (isEditing) {
          <input [(ngModel)]="tempValue" class="flex-1 p-2 border rounded-lg dark:bg-slate-800 dark:border-slate-700 dark:text-white focus:ring-2 focus:ring-indigo-500 outline-none">
          <button (click)="onSave()" class="px-3 py-1.5 border border-indigo-600 text-indigo-600 rounded-lg hover:bg-indigo-600 hover:text-white transition-colors text-sm font-semibold cursor-pointer">
            {{ langService.translate('editableFieldComponent.save') }}
          </button>
        } 
        @else {
          <div class="flex-1 p-2 text-slate-700 dark:text-slate-300">{{ value }}</div>
          <button (click)="isEditing = true" class="px-3 py-1.5 border border-slate-300 dark:border-slate-700 text-slate-600 dark:text-slate-400 rounded-lg hover:bg-slate-100 dark:hover:bg-slate-800 transition-colors text-sm font-medium cursor-pointer">
            {{ langService.translate('editableFieldComponent.update') }}
          </button>
        }
      </div>
    </div>
  `
})
export class EditableField {
  langService = inject(LanguageService);
  @Input() label!: string;
  @Input() value?: string | null;
  @Output() save = new EventEmitter<string>();
  isEditing = false;
  tempValue = '';

  onSave() {
    this.save.emit(this.tempValue);
    this.isEditing = false;
  }
}