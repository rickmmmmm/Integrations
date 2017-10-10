import { Component, Input, OnChanges, OnInit, EventEmitter, Output } from "@angular/core";

@Component({
    selector: 'rollup',
    templateUrl: './rollup.component.html',
    styleUrls:['./rollup.component.css']
})

export class RollupComponent implements OnInit, OnChanges {

    @Input() public aggData: any;
    @Output() public errClicked = new EventEmitter<string>();
    public errorData: any;
    public nonErrorData: any;

    ngOnInit() {
        if (this.aggData) {
            console.log(this.aggData);
            this.errorData = this.aggData.filter(fil => { fil.DataType === 'Error'});
            this.nonErrorData = this.aggData.filter(fil => { fil.DataType === 'Non-Error'});
        }
    }

    ngOnChanges() {
        if (this.aggData) {
            console.log(this.aggData);
            this.errorData = this.aggData.filter(fil => { return fil.DataType === 'Error'; });
            this.nonErrorData = this.aggData.filter(fil => { return fil.DataType === 'Non-Error'; });
        }
    }

    onErrTypeClicked(val: string) {
        this.errClicked.emit(val);
    }

}