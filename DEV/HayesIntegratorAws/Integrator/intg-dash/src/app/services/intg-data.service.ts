import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

import { Observable } from "rxjs";
import 'rxjs/add/operator/toPromise';

@Injectable()
export class IntgDataService {

    constructor(private _http: Http) {

    }

    private config: any = require('./config.json');


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

    getAggregateDataByDate(dateString: any, client: string) : Promise<any> {
        return this._http.get(this.config.apiUrl + this.config.apiRoutes.getAggregates + '?date=' + dateString + '&client=' + client)
        .toPromise()
        .then(
            data => {
                return Promise.resolve(data.json());
            })
        .catch(error => {
            return Promise.reject(error);
        });
    }

    getFilteredErrorsByDate(dateString: any, numrec = 100, page = 1, client, filterVal: string): Promise<any> {
        console.log(this.config.apiUrl + this.config.apiRoutes.getByDate + '?date=' + dateString + '&c=' + numrec + '&n=' + page + '&client=' + encodeURI(client) + '&e=' + encodeURI(filterVal));

        return this._http.get(this.config.apiUrl + this.config.apiRoutes.getByDate + '?date=' + dateString + '&c=' + numrec + '&n=' + page + '&client=' + encodeURI(client) + '&e=' + encodeURI(filterVal))
        .toPromise()
        .then(
            data => {
                console.log(data.json());
                return Promise.resolve(data.json());
            })
        .catch(error => {
            return Promise.reject(error);
        });
    }

    getFilteredErrorsAnalysisByDate(dateString: any, client, filterVal: string): Promise<any> {
        console.log(this.config.apiUrl + this.config.apiRoutes.getAnalysis + '?date=' + dateString + '&client=' + encodeURI(client) + '&e=' + encodeURI(filterVal));

        return this._http.get(this.config.apiUrl + this.config.apiRoutes.getAnalysis + '?date=' + dateString + '&client=' + encodeURI(client) + '&e=' + encodeURI(filterVal))
        .toPromise()
        .then(
            data => {
                console.log(data.json());
                return Promise.resolve(data.json());
            })
        .catch(error => {
            return Promise.reject(error);
        });
    }

}