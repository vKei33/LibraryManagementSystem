import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Book } from 'src/app/features/feature-book/model/book';
import { Member } from 'src/app/features/feature-member/model/member';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-create-rent',
  templateUrl: './create-rent.component.html',
  styleUrls: ['./create-rent.component.scss']
})
export class CreateRentComponent implements OnInit {
  form = this.formBuilder.group({
    bookId: ['', Validators.required],
    memberId: [],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    phoneNumber: ['', Validators.required]
  });

  books: Book[] = [];
  members: Member[] = [];
  selectedMember: any;

  constructor(private formBuilder: FormBuilder, private api: ApiService, private router: Router) { }

  async ngOnInit() {
    await this.getBooks();
    await this.getMembers();
    this.onMemberChanges();
  }

  async getBooks() {
    this.books = (await this.api.get('/books/all')).items || [];
  }

  async getMembers() {
    this.members = (await this.api.get('/members/all')).items || [];
  }

  onMemberChanges() {
    this.form.get('memberId')?.valueChanges.subscribe(selectedValue => {
      this.selectedMember = this.members.find(x => x.id == selectedValue);
      if (this.selectedMember) {
        this.form.patchValue({
          firstName: this.selectedMember.firstName,
          lastName: this.selectedMember.lastName,
          phoneNumber: this.selectedMember.phoneNumber
        });
      } else {
        this.form.patchValue({
          firstName: '',
          lastName: '',
          phoneNumber: ''
        });
      }
    });
  }

  onSubmit() {
    this.api.post('/rents/create-rent', this.form.value).subscribe({
      next: () => {
        console.log('Success');
        this.form.reset();
        this.router.navigate(['/rents']);
      },
      error: () => {
        console.log('Failed');
      }
    });
  }
}
