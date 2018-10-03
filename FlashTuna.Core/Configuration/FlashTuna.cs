using FlashTuna.Core.Attributes.Common;
using FlashTuna.Core.Storage.DataBase;
using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlashTuna.Core.Configuration
{
    public static class FlashTuna
    {

        static FlashTunaBuilder _builder;

        public static FlashTunaBuilder CreateBuilder()
        {
            return new FlashTunaBuilder();
        }
        public static void Initialize()
        {

        }

        public static void Initialize(FlashTunaBuilder builder)
        {
            _builder = builder;
        }
        public static async Task<string> PrintMetricsResult()
        {
            string data = "";
            foreach(var item in await CurrentTimeLine.ExtractMetricResult())
            {
                data += item.ToMetricString() + Environment.NewLine;
            }
            return data;
        }

        internal static string ModuleName
        {
            get
            {
                return _builder.ModuleName;
            }
        }
        internal static ITimeLine CurrentTimeLine
        {
            get
            {
                return _builder.TimeLine;
            }
        }
        internal static IEnumerable<MethodInfo> MeteredMethods
        {
            get { return _builder.MeteredMethods; }
        }

        public class FlashTunaBuilder
        {
            private ITimeLine _timeLine;
            private string _moduleName;
            private IEnumerable<MethodInfo> _meteredMethods;
            internal string ModuleName { get => _moduleName; set => _moduleName = value; }
            internal ITimeLine TimeLine { get => _timeLine; set => _timeLine = value; }
            internal IEnumerable<MethodInfo> MeteredMethods { get => _meteredMethods; set => _meteredMethods = value; }

            public FlashTunaBuilder SetStorage(IFlashTunaDbContext storageProvider)
            {
                _timeLine = new TimeLine.TimeLine(storageProvider);
                return this;
            }
            public FlashTunaBuilder SetModuleName(string moduleName)
            {
                _moduleName = moduleName;
                return this;
            }
            public FlashTunaBuilder Build(Type targetAssemblyClass)
            {
                Assembly clientAssembly = targetAssemblyClass.Assembly;

                var ass = clientAssembly;
                var types = ass.GetTypes();
                var methods = types.SelectMany(t => t.GetMethods());
                var hasAttr = methods.Where(m => m.GetCustomAttributes(typeof(PerfomanceMetricAttribute), true).Length > 0);

                _meteredMethods = hasAttr;
                return this;
            }

        }

    }


    public class FlashTunaConfiguration
    {
       

    }
    
}
