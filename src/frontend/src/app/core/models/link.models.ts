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
  NewDisplayOrder: number;
}