import { Component, OnInit, Input } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { User } from '../../_models/user';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-caregivers-edit',
  templateUrl: './caregivers-edit.component.html',
  styleUrls: ['./caregivers-edit.component.css']
})
export class CaregiversEditComponent implements OnInit {

  @Input() model: User = {} as User;

  constructor(public userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.loadCaregiver();
  }

  loadCaregiver() {
    this.userService.getCaregiver(this.route.snapshot.params.id).subscribe(caregiver => {
      this.model = caregiver;
    });
  }

  editCaregiver(id, editCaregiverForm) {
    this.userService.updateCaregiver(this.model).subscribe(medication => {
      this.alertify.success('Caregiver was edited successfully!');
      this.router.navigate(['/caregivers']);
    }, error => {
      this.alertify.error(error);
    });
  }
}
