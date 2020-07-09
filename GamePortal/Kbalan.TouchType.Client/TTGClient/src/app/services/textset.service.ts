import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TextSetDto } from '../models/textsetDto';

@Injectable({
  providedIn: 'root'
})
export class TextsetService {

  constructor(private http: HttpClient) { }

  getTextSetById(id: number){
    return this.http.get<TextSetDto>(`https://localhost:44313/api/textsets/${id}`);
  }
}
