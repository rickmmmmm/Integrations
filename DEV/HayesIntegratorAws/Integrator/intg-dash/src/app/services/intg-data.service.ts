import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

import { Observable } from "rxjs";
import 'rxjs/add/operator/toPromise';

@Injectable()
export class IntgDataService {

    constructor(private _http: Http) {

    }

    private config: any = { 
        apiUrl: 'http://localhost:3000', 
        apiRoutes: { getByID: '/api/errorsByIntgID', getByDate: '/api/errorsByDate' } 
    };


    getErrorsByDate(dateString: any, numrec = 100, page = 1, client) : Promise<any> {
        return this._http.get(this.config.apiUrl + this.config.apiRoutes.getByDate + '?date=' + dateString + '&c=' + numrec + '&n=' + page + '&client=' + client)
        .toPromise()
        .then(
            data => {
                //console.log(data.json());
                return Promise.resolve(data.json());
            })
        .catch(error => {
            return Promise.reject(error);
        });
    }

    getErrorsByIntgID(intgid: any, numrec = 100, page = 1) : Promise<any> {
        return this._http.get(this.config.apiUrl + this.config.apiRoutes.getByID + '?id=' + intgid + '&c=' + numrec + '&n=' + page)
        .toPromise()
        .then(
            response => { console.log(response.json()); return Promise.resolve(response.json());})
        .catch(error => {
            return Promise.reject(error);
        });
    }

}