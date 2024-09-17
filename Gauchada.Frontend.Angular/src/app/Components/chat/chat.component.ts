import { Component, Input, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ChatService } from '../../Services/ChatService';
import { MessageService } from '../../Services/MessageService';
import { UserService } from '../../Services/UserService';
import { TripService } from '../../Services/TripService';
import { Trip } from '../../Models/trip.model';
import { sendMessage } from '@microsoft/signalr/dist/esm/Utils';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit {

  messages = [
    { writerUsername: 'Alice', messageContent: 'Hello!', writeTime: '2024-10-21T12:00:00' }
  ];
  newMessageContent = '';
  chatId!: number;
  @Input() trip!: Trip;
  tripId!: number;
  connection: HubConnection;
  constructor(private _chatService: ChatService, private _messageService: MessageService, private _userService: UserService, private _tripService: TripService)
  {
    this.connection = new HubConnectionBuilder()
      .withUrl("http://localhost:5080/chat")
      .build();
  }

  ngOnInit(): void {
    this.tripId = this._tripService.getSavedTrip().tripId;
    this._chatService.getChatMessages(this.tripId).subscribe(res => {
      this.messages = res.data.messages;
      console.log(res.data.messages);
      this.messages.sort((a, b) => new Date(a.writeTime).getTime() - new Date(b.writeTime).getTime());
      this.chatId = res.data.chatId;

      // Connection to SignalR ChatHub
      this.connection.start().then(() => {
        this.connection.invoke("JoinChat", this.chatId.toString());

        this.connection.on("ReceiveMessage", (user, message) => {
          this.messages.push({ writerUsername: user, messageContent: message, writeTime: '' });
        });
      
      }).catch(err => console.error("Error al conectar al hub: ", err));
    });

  
  }

  sendMessage(): void {
    this._messageService.postMessage(this.tripId, this.newMessageContent).subscribe(res => {
      if (!res.success) console.error("Mensaje no guardado");
    }, err => console.error(err));
    this.newMessageContent = '';
  }

  onKeydown(event: KeyboardEvent): void {
    if (event.key === 'Enter')
      this.sendMessage();
  }
}
