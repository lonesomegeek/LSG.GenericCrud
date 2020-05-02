import { ColumnDef } from './column-def';

export interface Crud {
    id: string;
    entityName: string;
    columnDefs: ColumnDef[];
}
