export class MetricsResultModel
{
    constructor(id: number,
        tag: string,
        moduleName: string,
        className: string,
        methodName: string,
        startPoint: Date,
        endPoint: Date,
        milliseconds: number)
    {
        this.tag = tag;
        this.moduleName = moduleName;
        this.className = className;
        this.methodName = methodName;
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.id = id;
        this.milliseconds = milliseconds;
    };
        tag: string;
        moduleName: string;
        className: string;  
        methodName: string;
        startPoint: Date;
        endPoint: Date;
        milliseconds: number;
        id: number;
}

