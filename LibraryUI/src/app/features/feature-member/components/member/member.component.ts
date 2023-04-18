import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { Member } from '../../model/member';

@Component({
  selector: 'app-member',
  templateUrl: './member.component.html',
  styleUrls: ['./member.component.scss']
})
export class MemberComponent implements OnInit {
  members: Member[] = [];
  memberId: any;

  constructor(private api: ApiService, private route: ActivatedRoute) { }

  async ngOnInit() {
    await this.getAll();
    this.memberId = this.route.snapshot.paramMap.get('id');
  }

  async getAll() {
    this.members = (await this.api.get('/members/all')).items || [];
  }

  delete(id: string = "", name: string = "") {
    if (confirm('You are about to delete ' + name + '. Are you sure?')) {
      this.api.delete('/members/delete-member', this.memberId = id).subscribe(async res => {
        await this.getAll();
      });
    }
  }
}
