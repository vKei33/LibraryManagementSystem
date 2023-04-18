import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { Rent } from '../../model/Rent';

@Component({
  selector: 'app-rent',
  templateUrl: './rent.component.html',
  styleUrls: ['./rent.component.scss']
})
export class RentComponent implements OnInit {
  rents: Rent[] = [];
  rentId: any;
  constructor(private api: ApiService, private route: ActivatedRoute) { }

  async ngOnInit() {
    this.rentId = this.route.snapshot.paramMap.get('id');
    await this.getAll();
  }

  async getAll() {
    this.rents = (await this.api.get('/rents/all')).items || [];
  }

  delete(id: string = "", name: string = "") {
    if (confirm('You are about to delete ' + name + '. Are you sure you want to delete the book rent?')) {
      this.api.delete('/rents/delete-rent', this.rentId = id).subscribe(async res => {
        await this.getAll();
      });
    }
  }
}
