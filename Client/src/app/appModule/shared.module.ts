import { CommonModule, NgOptimizedImage } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxSpinnerModule } from "ngx-spinner";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TimeagoModule } from 'ngx-timeago';



// ? Angular Material
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatRadioModule } from '@angular/material/radio';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatButtonToggleModule} from '@angular/material/button-toggle';

// import {MatDatepickerModule} from '@angular/material/datepicker';

// import {MatNativeDateModule} from '@angular/material';
// import { MatMomentDateModule } from "@angular/material-moment-adapter";


@NgModule({
  declarations: [

  ],

  imports: [
    CommonModule,
    TabsModule.forRoot(),
    NgxGalleryModule,
    NgxSpinnerModule.forRoot({ type: 'line-scale-party' }),
    FileUploadModule,
    NgOptimizedImage,
    PaginationModule.forRoot(),
    TimeagoModule.forRoot(),
    

    // ?Angular Material
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatMenuModule,
    MatCardModule,
    MatDatepickerModule,
    MatRadioModule,
    MatTableModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatButtonToggleModule,
    // MatNativeDateModule,
    // MatMomentDateModule,



  ],

  exports: [
    TabsModule,
    NgxGalleryModule,
    NgxSpinnerModule,
    FileUploadModule,
    NgOptimizedImage,
    PaginationModule,
    TimeagoModule,


    // ?Angular Material
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatMenuModule,
    MatCardModule,
    MatDatepickerModule,
    MatRadioModule,
    MatTableModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatButtonToggleModule,
    // MatNativeDateModule,MatMomentDateModule



  ]
})
export class SharedModule { }
