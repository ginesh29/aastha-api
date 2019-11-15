import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule, MatInputModule, MatButtonModule } from '@angular/material';

const modules = [
  MatCardModule,
  MatInputModule,
  MatButtonModule
];


@NgModule({
  declarations: [],
  imports: [
    CommonModule, modules
  ],
  exports: modules,
})
export class MaterialModule { }
