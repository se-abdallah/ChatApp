import { Component, Input, OnInit } from '@angular/core';
import { Toast, ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/appModel/member';
import { MembersService } from 'src/app/appServices/members.service';
import { PresenceService } from 'src/app/appServices/presence.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() member: Member | undefined;
  
  constructor(private membersService : MembersService , private toastr :ToastrService, public presenceService : PresenceService) { }

  ngOnInit(): void {
  }

  addLike(member : Member){
    this.membersService.addLike(member.userName).subscribe({
      next : () => this.toastr.success('You liked ' + member.userName)
    })

  }

}
 