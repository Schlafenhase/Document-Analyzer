import { Axios } from 'axios-observable';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { BlobStorageRequest } from '../types/azure-storage';

export class SasGeneratorService {
  getSasToken(): Observable<BlobStorageRequest> {
    // return Axios.get<BlobStorageRequest>(
    //   'https://stottle-blob-storage-api.azurewebsites.net/api/GenerateSasToken'
    // ).pipe(map(res => res.data));
    const res$ = of (
        {"storageUri":"https://schlafenhase.blob.core.windows.net/","storageAccessToken":"?sv=2020-02-10&ss=bfqt&srt=sco&sp=rwdlacupx&se=2021-05-05T06:03:04Z&st=2021-04-13T22:03:04Z&spr=https&sig=U3fUk0RYlVjyE%2F%2Fp06%2BVanD5J4Qk9upza6ge0EOdKAw%3D"}
        );
    // const res$ = of (
    //     {"storageUri":"https://stottleblobstorage.blob.core.windows.net/","storageAccessToken":"sv=2019-02-02&ss=b&srt=sco&spr=https&st=2021-04-12T20%3A56%3A09Z&se=2021-04-12T21%3A01%3A09Z&sp=rwdlacup&sig=3SZ5JglG%2Fd%2FDIPyLzVIpC9zSPTCuzOnz3VDwBfHjOW8%3D"}
    //   );
    return res$
  }
}
