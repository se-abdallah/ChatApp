import { Component, OnInit } from '@angular/core';
import { Member } from '../appModel/member';
import { Pagination } from '../appModel/pagination';
import { MembersService } from '../appServices/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members?: Member[];
  predicate = "liked";
  pageNumber = 1;
  pageSize = 5;
  pagination?: Pagination;

  constructor(private membersService: MembersService) { }

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes() {
    this.membersService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe({
      next: response => {
        this.members = response.result;
        this.pagination = response.pagination;
      }
    })
  }



  pageChanged(event: any) {
    if (this.pageNumber !== event.page)
      this.pageNumber = event.page;
    this.loadLikes();
  }
}
