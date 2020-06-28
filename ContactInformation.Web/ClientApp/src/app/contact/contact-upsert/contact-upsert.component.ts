import { Component } from '@angular/core';
import { ContactService } from '../contact.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from '../../notification.service';

@Component({
  selector: 'app-contact-upsert',
  templateUrl: './contact-upsert.component.html',
  styleUrls: ['./contact-upsert.component.css']
})
export class ContactUpsertComponent {
  public contact: Contact = { name: "", active: false, id: -1, email: "", phone: "" };
  constructor(private activatedRoute: ActivatedRoute, private router: Router, private contactService: ContactService, private notification: NotificationService) {
    this.activatedRoute.params.subscribe(params => {
      if (params.id) {
        this.contactService.get(params.id).subscribe(result => {
          this.contact = result;
        }, error => console.error(error));
      }
    });
  }
  
  addContact(contact: Contact) {
    debugger;    this.contactService.add(contact).subscribe(result => {
      this.notification.showSuccess("Contact " + contact.name + " added successfully.", "");
      console.log(JSON.stringify(result));
      this.router.navigate(['/contacts']);
    }, error => {
      this.notification.showError("Contact " + contact.name + " add failed.", error.status);
      console.error(error);
    });
  }

  updateContact(id: number, contact: Contact) {
    this.contactService.update(id, contact).subscribe(result => {
      this.notification.showSuccess("Contact " + contact.name + " update successfully.", "");
      console.log(JSON.stringify(result));
      this.router.navigate(['/contacts']);
    }, error => {
        this.notification.showError("Contact " + contact.name + " update failed.", error.status);
      console.error(error);
    });
  }
}
