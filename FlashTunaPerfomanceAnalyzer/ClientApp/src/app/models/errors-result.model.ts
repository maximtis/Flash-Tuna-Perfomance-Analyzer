export class ErrorsResultModel
{
    constructor(
        methodName: string,
      errorsCount: number)
    {

        this.methodName = methodName;
      this.errorsCount = errorsCount
    };

        methodName: string;
        errorsCount:number;
}

