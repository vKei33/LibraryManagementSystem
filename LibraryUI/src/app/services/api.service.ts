import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseURL: string = 'https://localhost:7208/api';
  constructor(private http: HttpClient) { }

  get(path: string = "") {
    return lastValueFrom(this.http.get<any>(`${this.baseURL}${path}`));
  }

  post(path: string = "", data: any) {
    return this.http.post<any>(`${this.baseURL}${path}`, data);
  }

  put(path: string = "", id: string = "", data: any) {
    return this.http.put<any>(`${this.baseURL}${path}/${id}`, data);
  }

  delete(path: string = "", id: string = "") {
    return this.http.delete<any>(`${this.baseURL}${path}/${id}`);
  }
}
