import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TextSetDto } from '../models/textsetDto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TextsetService {

  constructor(private http: HttpClient) { }

getAllTextSet(){
  return this.http.get<TextSetDto>(`${environment.backendurl}/api/textsets/`);
}

  getTextSetById(id: number){
    return this.http.get<TextSetDto>(`${environment.backendurl}/api/textsets/${id}`);
  }
}
