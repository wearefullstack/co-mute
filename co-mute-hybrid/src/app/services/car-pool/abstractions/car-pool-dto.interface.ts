export interface ICarPoolDto {
  depart: Date;
  arrive: Date;
  origin: string;
  destination: string;
  availableSeats: number;
  notes?: string;
  userId : string;
}

export interface ICarPoolResult {
  depart: string;
  arrive: string;
  origin: string;
  destination: string;
  availableSeats: number;
  notes?: string;
  userId : string;
}
