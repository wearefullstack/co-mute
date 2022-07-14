import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CarPoolContainerComponent } from './car-pool-container.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarPoolEditorComponent } from './components/car-pool-editor/car-pool-editor.component';
import { Route, RouterModule } from '@angular/router';
import { NgbDatepickerModule, NgbTimepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { CarPoolSearchPipe } from './pipes/car-pool-search.pipe';

const routes : Route[] = [
  {
    path : 'search',
    component : CarPoolContainerComponent
  },
  {
    path : 'create',
    component : CarPoolEditorComponent
  }
]

@NgModule({
  declarations: [
    CarPoolContainerComponent,
    CarPoolEditorComponent,
    CarPoolSearchPipe
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule,
    NgbDatepickerModule,
    NgbTimepickerModule
  ]
})
export class CarPoolModule { }
