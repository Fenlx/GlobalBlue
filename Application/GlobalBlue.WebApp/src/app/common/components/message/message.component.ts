import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrl: './message.component.css'
})
export class MessageComponent {
  constructor(private toastr: ToastrService) {        
  }

  showSuccess(messsage: string, title: string) {
      this.toastr.success(messsage, title, {
          closeButton: true            
      });
  }

  showWaring(message: string, title: string) {
    this.toastr.warning(message, title, {
      closeButton: true,
      timeOut: 20000
    });
  }

  showError(messsage: string, title: string) {
      this.toastr.error(messsage, title, {
          closeButton: true,
          timeOut: 20000
      });
  }
}
