interface IPagedListInput {
    filter?: string;
    // todo: change prop name to sortBy
    sorting?: string;
    pageIndex?: number;
    pageSize?: number;
}