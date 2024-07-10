import { Component, OnInit } from '@angular/core';
import { Medication } from 'src/app/_models/medication';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import MedicationService from '../../_services/medication.service';

@Component({
  selector: 'app-medication-list',
  templateUrl: './medication-list.component.html',
  styleUrls: ['./medication-list.component.css']
})
export class MedicationListComponent implements OnInit {

  medications: Medication[];

  constructor(private medicationService: MedicationService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadMedication();
  }

  loadMedication() {
    this.medicationService.getMedications().subscribe(medications => {
      this.medications = medications;
    })
  }

  removeMedication(id) {
    this.medicationService.removeMedication(id).subscribe();
    location.reload();
  }
}
