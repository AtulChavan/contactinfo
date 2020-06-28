import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getList() {
    return this.http.get<Contact[]>(this.baseUrl + 'api/contact/list');
  }

  get(id: number) {
    return this.http.get<Contact>(this.baseUrl + 'api/contact/getbyid/' + id);
  }

  add(contact: Contact) {
    return this.http.post(this.baseUrl + 'api/contact/add', contact);
  }

  update(id: number, contact: Contact) {
    return this.http.put(this.baseUrl + 'api/contact/update/' + id, contact);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + 'api/contact/delete/' + id);
  }
}
