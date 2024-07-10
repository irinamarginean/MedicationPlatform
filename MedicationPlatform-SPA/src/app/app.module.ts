import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { AuthGuard } from './_guards/auth.guard';
import { RoleGuard } from './_guards/role.guard';
import { AlertifyService } from './_services/alertify.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { MedicationListResolver } from './_resolvers/medication-list.resolver';
import { SignalRService } from '../app/_services/signalR.service';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { MedicationListComponent } from './medication/medication-list/medication-list.component';
import { MedicationPlanComponent } from './medication/medication-plan/medication-plan.component';
import { MedicationEditComponent } from './medication/medication-edit/medication-edit.component';
import { MedicationAddComponent } from './medication/medication-add/medication-add.component';
import { PatientsAddComponent } from './users/patients-add/patients-add.component';
import { PatientsEditComponent } from './users/patients-edit/patients-edit.component';
import { PatientsListComponent } from './users/patients-list/patients-list.component';
import { CaregiversAddComponent } from './users/caregivers-add/caregivers-add.component';
import { CaregiversListComponent } from './users/caregivers-list/caregivers-list.component';

import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { HomeComponent } from './home/home.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { appRoutes } from './routes';
import MedicationService from './_services/medication.service';
import { JwtInterceptor } from './_services/jwt.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { UserService } from './_services/user.service';
import { CaregiversEditComponent } from './users/caregivers-edit/caregivers-edit.component';
import { CaregiversDetailsComponent } from './users/caregivers-details/caregivers-details.component';

// tslint:disable-next-line: typedef
export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    MedicationListComponent,
    MedicationEditComponent,
    MedicationAddComponent,
    PatientsAddComponent,
    PatientsEditComponent,
    PatientsListComponent,
    CaregiversAddComponent,
    CaregiversListComponent,
    CaregiversEditComponent,
    CaregiversDetailsComponent,
    MedicationPlanComponent
   ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    FormsModule,
    JwtModule.forRoot({
        config: {
           tokenGetter
        }
    }),
    NgbModule
  ],
  providers: [
    AuthService,
    AlertifyService,
    ErrorInterceptorProvider,
    AuthGuard,
    RoleGuard,
    MedicationListResolver,
    MedicationService,
    UserService,
    SignalRService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


