import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppComponent } from './app.component';
import { LayoutComponent } from './shared/layout/layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './account/login/login.component';
import { LeftMenuComponent } from './shared/left-menu/left-menu.component';
import { NotFound404Component } from './shared/not-found404/not-found404.component';
import { PatientListComponent } from './patient/patient-list/patient-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PatientFormComponent } from './patient/patient-form/patient-form.component';
import { DemoComponent } from './demo/demo.component';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'patients', component: PatientListComponent },
      { path: 'patient-form', component: PatientFormComponent },
      { path: 'demo', component: DemoComponent },
    ]
  },
  { path: '**', component: NotFound404Component }
];

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    DashboardComponent,
    LoginComponent,
    LeftMenuComponent,
    NotFound404Component,
    PatientListComponent,
    PatientFormComponent,
    DemoComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
