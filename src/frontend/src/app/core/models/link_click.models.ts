export interface LinkClickDto {
  id: string;
  clickedAt: Date;
  countryCode: string;
  userAgent: string | null;
}