using System.Threading.Tasks;

namespace FlashTuna.Core.Common.Metric.Interfaces
{
    public interface IMetric
    {
        string ClassName { get; set; }
        string MethodName { get; set; }
        string Tag { get; set; }
        string ModuleName { get; set; }
        Task<IMetricCall> StartAsync();
    }
}
