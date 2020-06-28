import { Component } from '@angular/core';
import { ContactService } from '../contact.service';
import { NotificationService } from '../../notification.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html'
})
export class ContactListComponent {
  public contacts: Contact[];

  constructor(private router: Router, private contactService: ContactService, private notification: NotificationService) {
    this.contactService.getList().subscribe(result => {
      this.notification.showSuccess("Contact list loaded successfully.", "");
      this.contacts = result;      
    }, error => {
        this.notification.showError("Contact list loading failed.", error.status);
        console.error(error);
    });
  }

  delete(id: number) {
    this.contactService.delete(id).subscribe(result => {
      this.notification.showSuccess("Contact " + id + " deleted successfully.", "");
      console.log(JSON.stringify(result));
      this.router.navigate(['/']);
    }, error => {
        this.notification.showError("Contact " + id + " delete failed.", error.status);
        console.error(error);
    });
  }
}
