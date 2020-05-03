import { CrudBase } from './crud';
import { ColumnDef } from './column-def';

export class History implements CrudBase {   

    id: string;
    name : string;
    
    entityName: string = "history";
    routeName: string = "history";
    showHistory: boolean = false;

    columnDefs: ColumnDef[] = [
        { headerName: 'Action', field: 'action', sortable: true, hideInForm: false },
        { headerName: 'By', field: 'createdBy', sortable: true, hideInForm: false },
        { headerName: 'At', field: 'createdDate', sortable: true, hideInForm: false }];
}
