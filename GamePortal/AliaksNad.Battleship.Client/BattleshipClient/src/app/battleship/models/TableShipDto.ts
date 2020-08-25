import { CoordinatesDto } from './coordinatesDto';

export interface TableShipDto {
  StartCoordinates: CoordinatesDto;
  isHorizontal: boolean;
  length: number;
  id: string;
}
