import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MedicationAddComponent } from './medication/medication-add/medication-add.component';
import { MedicationEditComponent } from './medication/medication-edit/medication-edit.component';
import { MedicationListComponent } from './medication/medication-list/medication-list.component';
import { CaregiversAddComponent } from './users/caregivers-add/caregivers-add.component';
import { CaregiversListComponent } from './users/caregivers-list/caregivers-list.component';
import { PatientsAddComponent } from './users/patients-add/patients-add.component';
import { PatientsEditComponent } from './users/patients-edit/patients-edit.component';
import { PatientsListComponent } from './users/patients-list/patients-list.component';
import { CaregiversEditComponent } from './users/caregivers-edit/caregivers-edit.component';
import { AuthGuard } from './_guards/auth.guard';
import { CaregiversDetailsComponent } from './users/caregivers-details/caregivers-details.component';
import { RoleGuard } from './_guards/role.guard';
import { MedicationPlanComponent } from './medication/medication-plan/medication-plan.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'medications',
    component: MedicationListComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
        expectedRole: 'Doctor'
    }},
    { path: 'medications/add',
    component: MedicationAddComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
        expectedRole: 'Doctor'
    }},
    { path: 'medications/edit/:id',
    component: MedicationEditComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
        expectedRole: 'Doctor'
    } },
    { path: 'patients',
    component: PatientsListComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
        expectedRole: 'Doctor'
    }},
    { path: 'patients/add',
    component: PatientsAddComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
        expectedRole: 'Doctor'
    }},
    { path: 'patients/edit/:id',
    component: PatientsEditComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
        expectedRole: 'Doctor'
    } },
    { path: 'patients/medication-plan/:id',
    component: MedicationPlanComponent,
    canActivate: [AuthGuard]
    },
    { path: 'caregivers',
    component: CaregiversListComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
        expectedRole: 'Doctor'
    }},
    { path: 'caregivers/add',
    component: CaregiversAddComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
        expectedRole: 'Doctor'
    } },
    { path: 'caregivers/edit/:id',
    component: CaregiversEditComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
        expectedRole: 'Doctor'
    }},
    { path: 'caregiver/patients',
    component: CaregiversDetailsComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
        expectedRole: 'Caregiver'
     } },

    {path: '**', redirectTo: '', pathMatch: 'full'}
];
