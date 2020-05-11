import { CrudBase } from './crud';
import { ColumnDef } from './column-def';

export class User implements CrudBase {
    id: string;
    name: string;

    showHistory: boolean = false;

    entityName: string = "users";
    routeName: string = "users-test";
    
    columnDefs: ColumnDef[] = [
        { headerName: 'Id', field: 'id', sortable: true, hide: true },
        { headerName: 'Name', field: 'name', sortable: true, hide: false }
    ];
    
}
