import { ICarPoolDto } from './../../../services/car-pool/abstractions/car-pool-dto.interface';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'carPoolSearch'
})
export class CarPoolSearchPipe implements PipeTransform {

  transform(pools: ICarPoolDto[],search : string, ): ICarPoolDto[] {
    if(!search || search === ''){
      return pools;
    }

    const norm = search.toLowerCase();
    return pools.filter(x=> x.origin.toLowerCase().includes(norm) || x.destination.toLowerCase().includes(norm) || x.availableSeats.toString().includes(norm))
  }

}
