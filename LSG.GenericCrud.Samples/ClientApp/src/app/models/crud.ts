import { ColumnDef } from './column-def';

export interface CrudBase {
    id: string;
    entityName: string;
    routeName: string;
    columnDefs: ColumnDef[];
    showHistory: boolean;
}

