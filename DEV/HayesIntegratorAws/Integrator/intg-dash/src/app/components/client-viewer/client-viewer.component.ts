import { Component, OnInit } from "@angular/core";

import { IntgDataService } from "../../services/intg-data.service";
import { mapSQLError } from "../../services/sql-error-mapping.service";

import { RollupComponent } from "../rollup/rollup.component";

@Component({
    selector: 'client-viewer',
    templateUrl: './client-viewer.component.html',
    providers:[IntgDataService]
})

export class ClientViewComponent implements OnInit {

    constructor(private _intgService: IntgDataService) {}

    public errorsData: any = [];
    public errorsAnalysisData: any;
    public searchingAnalysis: boolean = false;
    public searchingErrors: boolean = false;
    public intgDate: any;
    public filterValue: string;
    public pageNum: number = 1;
    public numResults: number = 100;
    public client: string = 'Chicago Test';
    public aggData: any;

    ngOnInit() {
    }

    getTheData(changePage?: number) {

        if (changePage === 1) {
            this.pageNum += 1;
        }

        else if (changePage === 0) {
            this.pageNum = this.pageNum === 1 ? this.pageNum : this.pageNum - 1;
        }

        this.getAggDataByDate(this.intgDate);
        //this.getErrorsByDate(this.intgDate, this.numResults, this.pageNum);
    }

    getFilteredData(filterString: string) {
        this.filterValue = filterString;
        this.getFilteredErrorsByDate(this.intgDate, this.numResults, this.pageNum, filterString);
        this.getFilteredAnalysisByDate(this.intgDate, filterString)

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

    getFilteredErrorsByDate(dateVal: Date, numrec: number, page: number, filterVal: string) {
        let dateString = dateVal;
        this.searchingErrors = true;
        
                this._intgService.getFilteredErrorsByDate(dateString, numrec, page, this.client, filterVal).then(
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
                            this.searchingErrors = false;
                        }
                    },
                    reject => {
                        console.error(reject);
                        this.searchingErrors = false;
                    }
                );
    }

    getFilteredAnalysisByDate(dateVal: Date, filterVal: string) {
        let dateString = dateVal;
        this.searchingAnalysis = true;
        
                this._intgService.getFilteredErrorsAnalysisByDate(dateString, this.client, filterVal).then(
                    resolve => {
                        console.log(resolve);
                        this.errorsAnalysisData = resolve;
                        this.searchingAnalysis = false;
                    },
                    reject => {
                        console.error(reject);
                        this.searchingAnalysis = false;
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

    getAggDataByDate(dateVal) {
        this._intgService.getAggregateDataByDate(dateVal, this.client).then(
            resolve => {
                this.aggData = resolve;
            },
            reject => {
                console.error(reject);
            }
        );
    }
}