import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Medication } from '../_models/medication';
import { MedicationPlan } from '../_models/medicationPlan';
import { Injectable } from '@angular/core';


@Injectable()
export default class MedicationService {

  baseUrl = environment.apiUrl;
  constructor(private httpClient: HttpClient) { }

  getMedications(): Observable<Medication[]> {
    return this.httpClient.get<Medication[]>(this.baseUrl + 'doctor/medications/all');
  }

  getMedication(id): Observable<Medication> {
    return this.httpClient.get<Medication>(this.baseUrl + 'doctor/medications/' + id);
  }

  addMedication(medication): Observable<Medication> {
    return this.httpClient.post<Medication>(this.baseUrl + 'doctor/medications/add', medication);
  }

  updateMedication(medication): Observable<Medication> {
    return this.httpClient.put<Medication>(this.baseUrl + 'doctor/medications/update', medication);
  }

  removeMedication(id): Observable<any> {
    return this.httpClient.delete<Medication[]>(this.baseUrl + 'doctor/medications/delete/' + id);
  }

  getMedicationPlans(patientId): Observable<MedicationPlan[]> {
    return this.httpClient.get<MedicationPlan[]>(this.baseUrl + 'doctor/medication-plan/' + patientId);
  }

  addMedicationPlan(medicationPlan): Observable<MedicationPlan> {
    return this.httpClient.post<MedicationPlan>(this.baseUrl + 'doctor/medication-plan/add', medicationPlan);
  }
}
