import { LinkDto, LinkPublicDto } from "./link.models";

export interface UserDto {
  id: string;
  username: string;
  email: string;
  displayName: string | null;
  bio: string | null;
  createdAt: Date;
  links: LinkDto[];
}

export interface UserPublicDto {
  username: string;
  displayName: string;
  bio: string | null;
  links: LinkPublicDto[];
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