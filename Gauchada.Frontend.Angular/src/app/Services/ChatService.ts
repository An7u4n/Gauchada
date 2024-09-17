import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../Models/response.model';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  chatUrl = 'http://localhost:5080/api/Chat';
  constructor(private _http: HttpClient) { }
  getChatMessages(tripId: number) {
    return this._http.get<ApiResponse>(`${this.chatUrl}/${tripId}`);
  }
}
