export interface Rent {
  id?: string;
  bookId: string;
  bookTitle: string;
  memberId?: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  rentalDate: string;
  returnDate: string;
  isReturned: boolean;
}