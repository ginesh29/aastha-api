import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-patient-form',
  templateUrl: './patient-form.component.html',
  styleUrls: ['./patient-form.component.css']
})
export class PatientFormComponent implements OnInit {
  formGroup: FormGroup;
  titleAlert = 'This field is required';
  post: any = '';
  constructor() { }

  ngOnInit() {
  }

}
