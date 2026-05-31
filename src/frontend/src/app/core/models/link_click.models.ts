export interface LinkClickDto {
  id: string;
  clickedAt: Date;
  ipAddress: string | null;
  userAgent: string | null;
}