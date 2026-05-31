export interface LinkDto {
  id: string;
  title: string;
  url: string;
  displayOrder: number;
  createdAt: Date;
  clickCount: number;
}

export interface CreateLinkCommand {
  title: string;
  url: string;
}

export interface ChangeDisplayOrderCommand {
  linkId: string;
  newDisplayOrder: number;
}

export interface ChangeTitleCommand {
  linkId: string;
  newTitle: string;
}

export interface ChangeUrlCommand {
  linkId: string;
  newUrl: string;
}