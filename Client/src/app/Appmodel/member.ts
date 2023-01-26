import { Photo } from "./photo";

export interface Member {
     id: number;
     userName: string;
     photoUrl: string;
     age: number;
     knownAs: string;
     created: Date;
     lastSeen: Date;
     gender: string;
     introduction: string;
     lookingFor: string;
     jobTitle: string;
     interests: string;
     city: string;
     country: string;
     photos: Photo[];
 }

