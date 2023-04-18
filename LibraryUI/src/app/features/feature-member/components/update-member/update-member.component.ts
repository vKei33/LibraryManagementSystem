import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-update-member',
  templateUrl: './update-member.component.html',
  styleUrls: ['./update-member.component.scss']
})
export class UpdateMemberComponent implements OnInit {
  form = this.formBuilder.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    phoneNumber: ['', Validators.required],
    membershipStartDate: ['', Validators.required]
  });
  memberId: any;

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router, private route: ActivatedRoute) { }

  async ngOnInit() {
    this.memberId = this.route.snapshot.paramMap.get('id');
    await this.getMember();
  }

  async getMember() {
    let result = (await this.api.get(`/members/${this.memberId}`)).item;
    if (result) {
      this.form.reset({
        firstName: result.firstName,
        lastName: result.lastName,
        phoneNumber: result.phoneNumber,
        membershipStartDate: result.membershipStartDate
      });
    }
  }

  onSubmit() {
    this.api.put('/members/update-member', this.memberId, this.form.value).subscribe({
      next: () => {
        console.log('Success');
        this.form.reset();
        this.router.navigate(['/members']);
      },
      error: () => {
        console.log('Failed');
      }
    });
  }
}
