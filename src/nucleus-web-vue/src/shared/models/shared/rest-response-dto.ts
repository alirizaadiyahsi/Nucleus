interface IRestResponseDto<T> {
    isError: boolean;
    errors: INameValueDto[];
    content: T;
}