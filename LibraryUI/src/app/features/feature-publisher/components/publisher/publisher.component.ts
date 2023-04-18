import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { Publisher } from '../../model/publisher';

@Component({
  selector: 'app-publisher',
  templateUrl: './publisher.component.html',
  styleUrls: ['./publisher.component.scss']
})
export class PublisherComponent implements OnInit {
  publishers: Publisher[] = [];
  publisherId: any;
  constructor(private api: ApiService, private route: ActivatedRoute) { }

  async ngOnInit() {
    await this.getAll();
    this.publisherId = this.route.snapshot.paramMap.get('id');
  }

  async getAll() {
    this.publishers = (await this.api.get('/publishers/all')).items || [];
  }

  delete(id: string = "", name: string = "") {
    if (confirm('You are about to delete ' + name + '. Are you sure?')) {
      this.api.delete('/publishers/delete-publisher', this.publisherId = id).subscribe(async res => {
        await this.getAll();
      });
    }
  }
}
