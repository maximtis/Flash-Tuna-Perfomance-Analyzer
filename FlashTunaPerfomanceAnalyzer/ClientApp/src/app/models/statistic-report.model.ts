export class StatisticReportModel
{
    constructor(discoveredMethods: number,
    errorsCount: number,
    problemsCount: number)
    {
      this.discoveredMethods = discoveredMethods;
      this.errorsCount = errorsCount;
      this.problemsCount = problemsCount;
    };

    discoveredMethods: number;
    errorsCount: number;
    problemsCount: number;
}

