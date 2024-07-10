import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import MedicationService from 'src/app/_services/medication.service';
import { Medication } from 'src/app/_models/medication';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-medication-add',
  templateUrl: './medication-add.component.html',
  styleUrls: ['./medication-add.component.css']
})
export class MedicationAddComponent implements OnInit {

  model: Medication = {} as Medication;

  constructor(private medicationService: MedicationService, private alertify: AlertifyService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
  }

  addMedication(addMedicationForm: NgForm) {
    this.medicationService.addMedication(this.model).subscribe(medication => {
      this.alertify.success(`${this.model.name} was added successfully!`);
      this.router.navigate(['/medications']);
    },
    error => {
      this.alertify.error(error);
    });
  }
}
