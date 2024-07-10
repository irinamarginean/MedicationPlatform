import { Component, Input, OnInit } from '@angular/core';
import { Medication } from 'src/app/_models/medication';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import MedicationService from '../../_services/medication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-medication-edit',
  templateUrl: './medication-edit.component.html',
  styleUrls: ['./medication-edit.component.css']
})
export class MedicationEditComponent implements OnInit {
  @Input() model: Medication = {} as Medication;

  constructor(private medicationService: MedicationService,
              private alertify: AlertifyService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.loadMedication();
  }

  loadMedication() {
    this.medicationService.getMedication(this.route.snapshot.params.id).subscribe(medication => {
      this.model = medication;
    });
  }

  editMedication(id, addMedicationForm) {
    this.medicationService.updateMedication(this.model).subscribe(medication => {
      this.alertify.success('Medication was edited successfully!');
      this.router.navigate(['/medications']);
    }, error => {
      console.log(error);
      this.alertify.error(error);
    });
  }
}
