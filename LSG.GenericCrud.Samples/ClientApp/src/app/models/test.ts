import { Crud } from './crud';
import { ColumnDef } from './column-def';

export class Test implements Crud {

    id: string;
    name : string;
    
    entityName: string = "tests";
    columnDefs: ColumnDef[] = [
        { headerName: 'Id', field: 'id', sortable: true, hideInForm: true },
        { headerName: 'Name', field: 'name', sortable: true, hideInForm: false }
    ];
}
