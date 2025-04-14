import { ResponseType } from "../enums/response.type.enum";

export class BaseResponseViewModel {
    public responseType: ResponseType;
    public messages: Array<string>;
}