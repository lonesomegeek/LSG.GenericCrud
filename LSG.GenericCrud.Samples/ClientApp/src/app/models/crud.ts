import { ColumnDef } from './column-def';

export interface CrudBase {
    id: string;
    entityName: string;
    columnDefs: ColumnDef[];
}

