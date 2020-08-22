import { CoordinatesDto } from '../../models/coordinatesDto';

export interface TableShipDto {
  StartCoordinates: CoordinatesDto;
  isHorizontal: boolean;
  length: number;
  id: string;
}
