import { LinkDto } from "./link.models";

export interface UserDto {
  id: string;
  username: string;
  email: string;
  displayName: string | null;
  bio: string | null;
  createdAt: Date;
  links: LinkDto[];
}