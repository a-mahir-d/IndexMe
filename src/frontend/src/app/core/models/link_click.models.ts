export interface LinkClickDto {
  id: string;
  clickedAt: Date;
  ipAddress: string | null;
  userAgent: string | null;
}

export interface ClickWithCountry extends LinkClickDto {
  countryCode?: string;
}