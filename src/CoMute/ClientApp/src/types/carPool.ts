export interface CarPool {
  id: number;
  departureTime: string;
  arrivalTime: string;
  origin: string;
  daysAvailable?: any; 
  destination: string;
  availableSeats: number;
  ownerId: number;
  created: string;
  notes?: string;
}
