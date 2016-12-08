import { Component, Input, Output, OnChanges, SimpleChanges, SimpleChange, EventEmitter, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
import { Http }    from '@angular/http';
import { Observable } from 'rxjs/Observable';
import '../rxjs-extensions';

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => PirisSelectComponent),
    multi: true
};

const noop = () => {
};

@Component({
    selector: 'piris-select',
    templateUrl: 'app/Common/ComponentsForm/pirisSelect.component.html',
    providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class PirisSelectComponent implements OnChanges, ControlValueAccessor{ 
    @Input() sourceUrl: string;
    @Input() required: boolean;

    selectedItemId: number;
    items: Observable<Array<any>>;

    private onTouchedCallback: () => void = noop;
    private onChangeCallback: (_: any) => void = noop;


    constructor(private http: Http) {}

    get value(): any {
        return this.selectedItemId;
    };

    //set accessor including call the onchange callback
    set value(v: any) {
        if (v !== this.selectedItemId) {
            this.selectedItemId = v;
            this.onChangeCallback(v);
        }
    }

    onBlur() {
        this.onTouchedCallback();
    }

    writeValue(value: any) {
        if (value !== this.selectedItemId) {
            this.selectedItemId = value;
        }
    }

    //From ControlValueAccessor interface
    registerOnChange(fn: any) {
        this.onChangeCallback = fn;
    }

    //From ControlValueAccessor interface
    registerOnTouched(fn: any) {
        this.onTouchedCallback = fn;
    }

    ngOnChanges(changes: SimpleChanges){
        if(changes['sourceUrl'] != null){
            this.onSourceUrlChanged(changes['sourceUrl']);
        }
    }

    onSourceUrlChanged(change: SimpleChange){
        if(change.currentValue == change.previousValue){
            return;
        }

        this.items = this.http.get(this.sourceUrl).map((response) => response.json());
    }
}