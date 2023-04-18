import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-update-publisher',
  templateUrl: './update-publisher.component.html',
  styleUrls: ['./update-publisher.component.scss']
})
export class UpdatePublisherComponent implements OnInit {
  publisherId: any;
  form = this.formBuilder.group({
    name: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder, private api: ApiService, private route: ActivatedRoute, private router: Router) { }

  async ngOnInit() {
    this.publisherId = this.route.snapshot.paramMap.get('id');
    await this.getPublisher();
  }

  async getPublisher() {
    let result = (await this.api.get(`/publishers/${this.publisherId}`)).item;
    if (result) {
      this.form.reset({
        name: result.name
      });
    }
  }

  onSubmit() {
    this.api.put('/publishers/update-publisher', this.publisherId, this.form.value).subscribe({
      next: () => {
        console.log("Success");
        this.form.reset();
        this.router.navigate(['/publishers']);
      },
      error: () => {
        console.warn("Failed");
      }
    });
  }
}
