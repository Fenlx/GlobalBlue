export class VatRequest {
    public vatRate: string;
    public net: string;
    public gross: string;
    public vat: string;

    constructor() {
        this.vatRate = '';
        this.net = '';
        this.gross = '';
        this.vat = '';
    }
}