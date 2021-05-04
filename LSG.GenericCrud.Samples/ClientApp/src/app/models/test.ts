import { CrudBase } from './crud';
import { ColumnDef } from './column-def';

export class Test implements CrudBase {

    id: string;
    name : string;
    
    entityName: string = "items";
    routeName: string = "tests";
    showHistory: boolean = false;
    
    columnDefs: ColumnDef[] = [
        { headerName: 'Id', field: 'id', sortable: true, hideInForm: true },
        { headerName: 'Name', field: 'name', sortable: true, hideInForm: false }
    ];
}
