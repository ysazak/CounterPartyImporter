export class PagedTable<T> {
    public count: number;
    public page: number;
    public pageSize: number;
    public data: Array<T>;
  }