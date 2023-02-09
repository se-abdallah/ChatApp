import {User} from "./user" ;

export class UserParams {
 gender! : string ;
 minAge = 10 ;
 maxAge =99 ;
 pageNumber = 1;
 pageSize = 3 ;
 orderBy = 'LastSeen' ;

 constructor(user : User){
  this.gender = user.gender === 'female' ? 'male' : 'female'
 }
}