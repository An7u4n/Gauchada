import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../Models/response.model';
import { UserService } from './UserService';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  chatUrl = 'http://localhost:5080/api/Message';
  constructor(private _http: HttpClient, private _userService: UserService) { }
  postMessage(tripId: number, messageContent: string) {
    var userType = this._userService.getLoggedUserType();
    var username = this._userService.getLoggedUser().userName;
    if(userType == "passenger" || userType == "driver")
      return this._http.post<ApiResponse>(`${this.chatUrl}/${tripId}?writer=${username}&messageContent=${messageContent}&userType=${userType}`, {});
    else throw new Error("UserType Incorrect")
  }
}
