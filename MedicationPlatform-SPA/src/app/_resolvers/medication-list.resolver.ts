import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Medication } from '../_models/medication';
import MedicationService from '../_services/medication.service';

@Injectable()
export class MedicationListResolver implements Resolve<Medication[]> {

    constructor(private medicationService: MedicationService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Medication[]> {
        return this.medicationService.getMedications().pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data!');
                this.router.navigate(['/home']);
                return of(null);
            })
        )
    }
}