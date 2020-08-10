import { CoordinatesDto } from './../../../models/CoordinatesDto';

export interface TableShipDto {
  StartCoordinates: CoordinatesDto;
  isHorizontal: boolean;
  length: number;
  id: string;
}
