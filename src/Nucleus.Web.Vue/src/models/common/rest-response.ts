interface IRestResponse<T> {
    isError: boolean;
    errors: IErrorResponse[],
    content: T,
};