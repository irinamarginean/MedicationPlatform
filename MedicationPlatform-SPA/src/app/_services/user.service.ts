import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { Patient } from '../_models/patient';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl + 'doctor/users';
  caregiverBaseUrl = environment.apiUrl + 'caregiver';

  constructor(private httpClient: HttpClient) { }

  getPatients(): Observable<any[]> {
    return this.httpClient.get<any[]>(this.baseUrl + '/patients/all');
  }

  getCaregivers(): Observable<any[]> {
    return this.httpClient.get<any[]>(this.baseUrl + '/caregivers/all');
  }

  getCaregiver(id): Observable<User> {
    return this.httpClient.get<User>(this.baseUrl + '/caregivers/' + id);
  }

  getPatient(id): Observable<Patient> {
    return this.httpClient.get<Patient>(this.baseUrl + '/patients/' + id);
  }

  getPatientsByCaregiver(id): Observable<Patient[]> {
    return this.httpClient.get<Patient[]>(this.caregiverBaseUrl + '/patients/all/' + id);
  }

  getCurrentCaregiver(): Observable<User> {
    return this.httpClient.get<User>(this.caregiverBaseUrl + '/current');
  }

  registerUser(caregiver): Observable<User> {
    return this.httpClient.post<User>(this.baseUrl + '/caregivers/register', caregiver);
  }

  registerPatient(patient): Observable<User> {
    return this.httpClient.post<User>(this.baseUrl + '/patients/register', patient);
  }

  updateCaregiver(user): Observable<User> {
    return this.httpClient.put<User>(this.baseUrl + '/caregivers/update', user);
  }

  updatePatient(patient): Observable<Patient> {
    return this.httpClient.put<Patient>(this.baseUrl + '/patients/update', patient);
  }

  removeUser(id): Observable<any> {
    return this.httpClient.delete<User[]>(this.baseUrl + '/delete/' + id);
  }

}
