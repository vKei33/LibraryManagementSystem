import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-update-rent',
  templateUrl: './update-rent.component.html',
  styleUrls: ['./update-rent.component.scss']
})
export class UpdateRentComponent implements OnInit {
  form = this.formBuilder.group({
    isReturned: [, Validators.required]
  });
  rentDetail: any;
  rentId: any;

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router, private route: ActivatedRoute) { }

  async ngOnInit() {
    this.rentId = this.route.snapshot.paramMap.get('id');
    await this.getRent();
  }

  async getRent() {
    this.rentDetail = (await this.api.get(`/rents/${this.rentId}`)).item
    if (this.rentDetail) {
      this.form.reset({
        isReturned: this.rentDetail.isReturned
      });
    }
  }

  onSubmit() {
    this.api.put('/rents/update-rent', this.rentId, this.form.value).subscribe({
      next: () => {
        console.log(this.form.value);
        this.router.navigate(['/rents']);
      },
      error: () => {
        console.log('Failed');
      }
    });
  }
}
