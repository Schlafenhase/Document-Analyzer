import { Axios } from 'axios-observable';
import { Observable } from 'rxjs';
import { BlobStorageRequest } from '../types/azure-storage';
import { BaseURL } from "../../constants";
import { map } from "rxjs/operators";

export class SasGeneratorService {
    getSasToken(): Observable<BlobStorageRequest> {
        return Axios.get<BlobStorageRequest>(
            BaseURL + '/Api/AzureBlobAuth',
            {
                headers: {
                    Authorization: "Bearer " + localStorage.getItem("token"),
                }
            }
        ).pipe(map(res => res.data));
    }
}
