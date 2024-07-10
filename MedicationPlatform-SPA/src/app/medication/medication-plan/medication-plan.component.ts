import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Medication } from 'src/app/_models/medication';
import { MedicationPlan } from 'src/app/_models/medicationPlan';
import { Patient } from 'src/app/_models/patient';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import MedicationService from 'src/app/_services/medication.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-medication-plan',
  templateUrl: './medication-plan.component.html',
  styleUrls: ['./medication-plan.component.css']
})
export class MedicationPlanComponent implements OnInit {

  medications: Medication[];
  medicationPlans: MedicationPlan[];
  patientId: string;
  patient: Patient;
  selectedMedication: boolean[];
  currentMedicationPlan: MedicationPlan;

  constructor(public userService: UserService, 
    private medicationService: MedicationService, 
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private authService: AuthService) { }

  ngOnInit() {
    this.patientId = this.route.snapshot.params.id;
    this.currentMedicationPlan = {} as MedicationPlan;
    this.loadPatient();
    this.loadMedication();
    this.loadMedicationPlans(this.patientId);
  }

  loadPatient() {
    this.userService.getPatient(this.patientId).subscribe(patient => {
      this.patient = patient;
    });
  }

  loadMedication() {
    this.medicationService.getMedications().subscribe(medications => {
      this.medications = medications;
      this.selectedMedication = Array(this.medications.length).fill(false, 0, this.medications.length);
    })
  }

  loadMedicationPlans(patientId) {
    this.medicationService.getMedicationPlans(patientId).subscribe(medicationPlans => {
      this.medicationPlans = medicationPlans;
    })
  }

  selectMedication(index: number) {
    this.selectedMedication[index] = !this.selectedMedication[index];
  }

  createPlan() {
    this.currentMedicationPlan.patient = this.patient;
    this.currentMedicationPlan.medication = new Array<Medication>();

    this.medications.forEach((value, index) => {
      this.selectedMedication[index] === true && this.currentMedicationPlan.medication.push(value);
    });
    this.medicationService.addMedicationPlan(this.currentMedicationPlan).subscribe(_ => {
      this.alertify.success(`Medication plan was added successfully!`);
      this.loadMedicationPlans(this.patientId);
    },
    error => {
      this.alertify.error(error);
    });
  }

  displayMedications(med: Medication[]): string {
    let displayString = '';
    if (med === null) {
      return '-';
    }
    for (let m of med) {
      displayString += `${m.name}, ${m.dosage}; `;
    }
    return displayString;
  }

  isUserDoctor() {
    return this.authService.isInRole('Doctor');
  }
}
