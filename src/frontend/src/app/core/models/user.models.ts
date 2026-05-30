export interface LinkClickDto {
  id: string;
  clickedAt: Date;
  ipAddress: string | null;
  userAgent: string | null;
}

export interface LinkDto {
  id: string;
  title: string;
  url: string;
  displayOrder: number;
  createdAt: Date;
  clickCount: number;
}

export interface UserDto {
  id: string;
  username: string;
  email: string;
  displayName: string | null;
  bio: string | null;
  createdAt: Date;
  links: LinkDto[];
}