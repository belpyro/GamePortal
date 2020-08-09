import { ShipDto } from './../../../models/ShipsDto';
import { CoordinatesDto } from './../../../models/CoordinatesDto';
import { Pipe, PipeTransform } from '@angular/core';
import { TableShipDto } from './TableShipDto';

@Pipe({
  name: 'fleetArr'
})
export class FleetArrPipe implements PipeTransform {
  transform(value: TableShipDto, args?: any): ShipDto {
    // const result: ShipDto;

    // console.log(`result = ${result}`);

    // if (value.isHorizontal) {
    //   for (let i = 0; i < value.length; i++) {
    //     let crd: CoordinatesDto = {
    //       CoordinateX: value.StartCoordinates.CoordinateX + i,
    //       CoordinateY: value.StartCoordinates.CoordinateY
    //     };
    //     console.log(`crd = ${crd}`);
    //     result.push(crd);
    //   }
    // } else {
    //   for (let i = 0; i < value.length; i++) {
    //     let crd: CoordinatesDto = {
    //       CoordinateX: value.StartCoordinates.CoordinateX,
    //       CoordinateY: value.StartCoordinates.CoordinateY + i
    //     };

    //   }
    // }

    // console.log(`result = ${result}`);
    // let shp: ShipDto;
    // shp.Coordinates = result;
    return null;
  }
}
