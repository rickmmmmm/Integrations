import { Component, OnInit } from "@angular/core";

import { IntgDataService } from "../../services/intg-data.service";
import { mapSQLError } from "../../services/sql-error-mapping.service";

@Component({
    selector: 'client-viewer',
    templateUrl: './client-viewer.component.html',
    providers:[IntgDataService]
})

export class ClientViewComponent implements OnInit {

    constructor(private _intgService: IntgDataService) {}

    public errorsData = [];
    public intgDate: any;
    public filterValue: string;
    public pageNum: number = 1;
    public numResults: number = 100;
    public client: string = 'Chicago Test';
    

    ngOnInit() {
        //this.getErrorsByID(133);
    }

    getTheData(changePage?: number) {

        if (changePage === 1) {
            this.pageNum += 1;
        }

        else if (changePage === 0) {
            this.pageNum = this.pageNum === 1 ? this.pageNum : this.pageNum - 1;
        }
        console.log(typeof changePage);
        console.log(this.pageNum);
        console.log(this.numResults);

        this.getErrorsByDate(this.intgDate, this.numResults, this.pageNum);
    }

    getErrorsByID(intgid: number) {

        this._intgService.getErrorsByIntgID(intgid).then(
            resolve => {
                this.errorsData = resolve;
                for (let item of this.errorsData) {
                    item.ErrorObject = JSON.parse(item.ErrorObject);
                    if (item.ErrorObject.error) {
                        item.ErrorObject.error = mapSQLError(item.ErrorObject.error);
                    }
                }
            },
            reject => {
                console.error(reject);
            }
        );
    }

    getErrorsByDate(dateVal: Date, numrec: number, page: number) {

        let dateString = dateVal;

        this._intgService.getErrorsByDate(dateString, numrec, page, this.client).then(
            resolve => {
                this.errorsData = resolve;
                for (let item of this.errorsData) {
                    try {
                        item.ErrorObject = JSON.parse(item.ErrorObject);
                    }
                    catch (e) {
                        console.error(e);
                        continue;
                    }
                    if (item.ErrorObject.error) {
                        item.ErrorObject.error = mapSQLError(item.ErrorObject.error);
                    }
                }
            },
            reject => {
                console.error(reject);
            }
        );
    }
}