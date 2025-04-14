import { Component } from '@angular/core';
import { VatRequest } from './common/models/vat.request.view.model';
import { BusinessService } from './common/services/business.service';
import { ResponseType } from './common/enums/response.type.enum';
import { MessageComponent } from './common/components/message/message.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  vatRequest: VatRequest = new VatRequest();

  constructor(public businessService: BusinessService, public message: MessageComponent) { }

  calculateVat() {
    this.businessService.calculateVat(this.vatRequest).subscribe(res => {
      if (res.responseType == ResponseType.Ok) {
        this.message.showSuccess('Values updated', 'Calculation success');
        this.vatRequest.vat = res.vat;
        this.vatRequest.gross = res.gross;
        this.vatRequest.vatRate = res.vatRate;
        this.vatRequest.net = res.net;
      } else if (res.responseType == ResponseType.Error) {
        this.message.showError(res.messages.join(', '), 'An error ocurred!');
      }
    }, error => {
      this.message.showError(error.message, 'An error ocurred!');
    });
  }

  reset() {
    this.vatRequest = new VatRequest();
  }
}