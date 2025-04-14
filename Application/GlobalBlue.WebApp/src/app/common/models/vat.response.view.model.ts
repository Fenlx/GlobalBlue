import { BaseResponseViewModel } from "./base.response.view.model";

export class VatResponse extends BaseResponseViewModel {
    public vatRate: string;
    public net: string;
    public gross: string;
    public vat: string;
}