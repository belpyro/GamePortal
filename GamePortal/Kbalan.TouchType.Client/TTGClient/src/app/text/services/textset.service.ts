import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TextSetDto } from '../models/textsetDto';
import { environment } from 'src/environments/environment';
import { TextSetDtomin } from '../models/textsetDtomin';

@Injectable({
  providedIn: 'root'
})
export class TextsetService {
  [x: string]: any;

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

  async getTextSetByLevelRandom(level: number){
    return await this.http.get<TextSetDto>(`${environment.backendurl}/api/textsets/searchbylevelrand/${level}`);
  }

  addTextSetToDb(model){
    return this.http.post(`${environment.backendurl}/api/textsets`, model);
  }

  deleteTextfromDb(id)
  {
    return this.http.delete(`${environment.backendurl}/api/textsets/${id}`);
  }

  updateTextfromDb(model){
    return this.http.put(`${environment.backendurl}/api/textsets`, model);
  }
}
