import { EmptyCellDto } from './EmptyCellsDto';
import { ShipDto } from './ShipsDto';
export interface BattleAreaDto {
  Ships: ShipDto[];
  EmptyCells: EmptyCellDto;
}
