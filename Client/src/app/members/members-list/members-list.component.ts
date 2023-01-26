import { Component, OnInit } from '@angular/core';
<<<<<<< HEAD
import { Member } from 'src/app/appModel/member';
import { Pagination } from 'src/app/appModel/pagination';
import { MembersService } from 'src/app/appServices/members.service';
=======
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6

@Component({
  selector: 'app-members-list',
  templateUrl: './members-list.component.html',
  styleUrls: ['./members-list.component.css']
})
export class MembersListComponent implements OnInit {
<<<<<<< HEAD
  // members$: Observable<Member[]> | undefined
  members!: Member[];
  pagination!: Pagination;
  pageNumber = 1;
  pageSize = 5;

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    this.loadMembers();
    // this.members$ = this.memberService.getMembers();
  }

  loadMembers() {
    this.memberService.getMembers(this.pageNumber, this.pageSize).subscribe({
      next: response => {
        if (response.result && response.pagination) {
          this.members = response.result;
          this.pagination = response.pagination;
        }
      }
      // next: members => this.members = members,
      // error: respone => console.log(respone),
    })
  }

  pageChanged(event : any){
    if(this.pageNumber !== event.page)
    this.pageNumber = event.page;
    this.loadMembers();
  }
=======

  constructor() { }

  ngOnInit(): void {
  }

>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
}
