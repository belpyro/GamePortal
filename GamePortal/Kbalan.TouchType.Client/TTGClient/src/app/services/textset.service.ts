import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TextSetDto } from '../models/textsetDto';
import { environment } from 'src/environments/environment';
import { TextSetDtomin } from '../models/textsetDtomin';

@Injectable({
  providedIn: 'root'
})
export class TextsetService {

  constructor(private http: HttpClient) { }

getAllTextSet(){
  return this.http.get<TextSetDtomin[]>(`${environment.backendurl}/api/textsets/`);
}

  getTextSetById(id: number){
    return this.http.get<TextSetDto>(`${environment.backendurl}/api/textsets/${id}`);
  }

  getTextSetByLevel(level: number){
    return this.http.get<TextSetDtomin[]>(`${environment.backendurl}/api/textsets/searchbylevel/${level}`);
  }

  getTextSetByLevelRandom(level: number){
    return this.http.get<TextSetDto>(`${environment.backendurl}/api/textsets/searchbylevelrand/${level}`);
  }
}
