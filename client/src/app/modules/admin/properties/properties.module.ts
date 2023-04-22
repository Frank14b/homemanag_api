import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatRippleModule } from '@angular/material/core';
import { MatSortModule } from '@angular/material/sort';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialogModule } from '@angular/material/dialog';
import { FuseAlertModule } from '@fuse/components/alert';
import { MatTableModule } from '@angular/material/table';
import { SharedModule } from 'app/shared/shared.module';
import { PropertiesComponent } from './properties.component';
import { AddFormComponent } from './add-form/add-form.component';
import { CardGmapsComponent } from 'app/modules/card-gmaps/card-gmaps.component';
import { GoogleMapsModule } from '@angular/google-maps';


const exampleRoutes: Route[] = [
  {
    path: '',
    component: PropertiesComponent
  }
];

@NgModule({
  declarations: [
    PropertiesComponent,
    AddFormComponent,
    CardGmapsComponent
  ],
  imports: [
    RouterModule.forChild(exampleRoutes),
    CommonModule,
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatRippleModule,
    MatSortModule,
    MatSelectModule,
    MatSlideToggleModule,
    MatTooltipModule,
    MatDialogModule,
    FuseAlertModule,
    MatTableModule,
    SharedModule,
    GoogleMapsModule
  ]
})
export class PropertiesModule { }
