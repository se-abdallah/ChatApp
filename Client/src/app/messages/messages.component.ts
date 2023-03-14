import { Component, OnInit } from '@angular/core';
import { Message } from '../appModel/message';
import { Pagination } from '../appModel/pagination';
import { MessageService } from '../appServices/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages?: Message[];
  pagination?: Pagination;
  container = 'Unread';
  // container = 'Inbox';
  // container = 'Outbox';
  pageNumber = 1;
  pageSize = 3;
  loading = false;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    this.loadMessages();
    
  }

  loadMessages() {
    this.loading = true;
    this.messageService.getMessages(this.pageNumber, this.pageSize, this.container).subscribe({
      next: response => {
        this.messages = response.result;
        this.pagination = response.pagination;
        this.loading = false;
      }
    })
  }

  deleteMessage(id: number) {
    this.messageService.deleteMessage(id).subscribe({
      next: () => this.messages?.splice(this.messages.findIndex(m => m.id === id), 1)
    })
  }


  pageChanged(event: any) {
    if (this.pageNumber !== event.page)
      this.pageNumber = event.page;
    this.loadMessages();
  }
}
