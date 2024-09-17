import { Component, Input, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ChatService } from '../../Services/ChatService';
import { MessageService } from '../../Services/MessageService';
import { UserService } from '../../Services/UserService';
import { TripService } from '../../Services/TripService';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit {

  messages = [
    { writerUsername: 'Alice', messageContent: 'Hello!' },
    { writerUsername: 'Bob', messageContent: 'Hi Alice, how are you?' },
    { writerUsername: 'Alice', messageContent: 'I’m good, thanks for asking!' },
    { writerUsername: 'Bob', messageContent: 'Great to hear!' }
  ];
  newMessageContent = '';
  chatId!: number;
  @Input() tripId!: number;
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
      this.chatId = res.data.chatId;

      // Connection to SignalR ChatHub
      this.connection.start().then(() => {
        this.connection.invoke("JoinChat", this.chatId.toString());

        this.connection.on("ReceiveMessage", (user, message) => {
          this.messages.push({ writerUsername: user, messageContent: message });
        });
      
      }).catch(err => console.error("Error al conectar al hub: ", err));
    });

  
  }

  sendMessage(/*user: string, message: string*/): void {
    //this.connection.invoke("SendMessageToChat", chatId, user, message)
    //this.connection.invoke("SendMessageToChat", this.chatId.toString(), "Lucas", this.newMessageContent)
    //  .catch(err => console.error('Error al enviar el mensaje: ', err));
    this._messageService.postMessage(this.tripId, this.newMessageContent).subscribe(res => {
      if (!res.success) console.error("Mensaje no guardado");
    }, err => console.error(err));
    this.newMessageContent = '';

  }
}
