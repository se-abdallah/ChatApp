import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { Member } from 'src/app/appModel/member';
import { Pagination } from 'src/app/appModel/pagination';
import { User } from 'src/app/appModel/user';
import { UserParams } from 'src/app/appModel/userParams';
// import { AccountService } from 'src/app/appServices/account.service';
import { MembersService } from 'src/app/appServices/members.service';

@Component({
  selector: 'app-members-list',
  templateUrl: './members-list.component.html',
  styleUrls: ['./members-list.component.css']
})
export class MembersListComponent implements OnInit {
  // members$: Observable<Member[]> | undefined
  members!: Member[];
  pagination!: Pagination;
  userParams!: UserParams;
  
  genderList =[{value : 'male' , display: 'Males'},{value : 'female' , display : 'Female'}] 

  constructor(private memberService: MembersService) {
  this.userParams = this.memberService.getUserParams();
    // this.accountService.currentUser$.pipe(take(1)).subscribe({
    //   next: user => {
    //     if (user) {
    //       this.userParams = new UserParams(user);
    //       this.user = user;
    //     }
    //   }
    // })
  }

  ngOnInit(): void {
    this.loadMembers();
    // this.members$ = this.memberService.getMembers();
  }

  restFilters(){
      this.userParams = this.memberService.resetUserParams();
      this.loadMembers();
  }

  loadMembers() {
    this.memberService.setUserParams(this.userParams);
    if(this.userParams){this.memberService.getMembers(this.userParams).subscribe({
      next: response => {
        if (response.result && response.pagination) {
          this.members = response.result;
          this.pagination = response.pagination;
        }
      }
    })
  }
}
    

  pageChanged(event: any) {
    if (this.userParams && this.userParams.pageNumber !== event.page)
      this.userParams.pageNumber = event.page;
      this.memberService.setUserParams(this.userParams);
      this.loadMembers();
  }
}
