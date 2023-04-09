import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GoogleMapsModule } from '@angular/google-maps';
import { Route, RouterModule } from '@angular/router';
import { CardGmapsComponent } from './card-gmaps.component';

const exampleRoutes: Route[] = [
  {
    path: '',
    component: CardGmapsComponent
  }
];

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild(exampleRoutes),
    CommonModule,
    GoogleMapsModule
  ]
})
export class CardGmapsModule { }
