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

export interface ChangeEmailCommand {
  newEmail: string;
}

export interface ChangeDisplayNameCommand {
  newDisplayName: string;
}

export interface ChangeBioCommand {
  newBio: string;
}