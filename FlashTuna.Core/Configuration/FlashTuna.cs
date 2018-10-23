using FlashTuna.Core.Attributes;
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
        static ITimeLine _timeLine;
        private static IEnumerable<MethodInfo> _meteredMethods;
        public static FlashTunaBuilder CreateBuilder()
        {
            return new FlashTunaBuilder();
        }
        public static void Initialize()
        {
            _timeLine = new TimeLine.TimeLine();
        }

        public static void Initialize(FlashTunaBuilder builder)
        {
            Initialize();
            _builder = builder;
            CurrentTimeLine.SetStorageProvider(_builder.DbConxtext);
            InitializeMetrics();
        }
        public static async Task<string> PrintMetricsResult()
        {
            string data = "";
            var completed = await CurrentTimeLine.ExtractMetricResult();
            foreach (var item in completed)
            {
                data += item.ToMetricString() + Environment.NewLine;
            }
            return data;
        }
        private static void InitializeMetrics()
        {
            Assembly clientAssembly = _builder.TargetAssembly;
            var types = clientAssembly.GetTypes();
            var methods = types.SelectMany(t => t.GetMethods());
            var hasAttr = methods.Where(m => m.GetCustomAttributes(typeof(OperationMetricAttribute)).Count() > 0);
            _meteredMethods = hasAttr.ToList();
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
                return _timeLine;
            }
        }
        internal static IEnumerable<MethodInfo> MeteredMethods
        {
            get { return _meteredMethods; }
        }

        public class FlashTunaBuilder
        {
            private IFlashTunaDbContext _dbConxtext;
            private string _moduleName;
            internal string ModuleName { get => _moduleName; set => _moduleName = value; }
            internal IFlashTunaDbContext DbConxtext { get => _dbConxtext; set => _dbConxtext = value; }
            internal Assembly TargetAssembly { get => _targetAssembly; set => _targetAssembly = value; }

            private Assembly _targetAssembly;

            public FlashTunaBuilder SetStorage(IFlashTunaDbContext storageProvider)
            {
                if (_dbConxtext != null)
                    throw new InvalidOperationException("Set Storage can be called only one time, and cannot be changed dynamically!");
                _dbConxtext = _dbConxtext??storageProvider;
                return this;
            }
            public FlashTunaBuilder SetModuleName(string moduleName)
            {
                _moduleName = moduleName;
                return this;
            }
            public FlashTunaBuilder SetTargetAssembly(Assembly target)
            {
                _targetAssembly = target;
                return this;
            }
            public FlashTunaBuilder Build()
            {
                return this;
            }

        }

    }


    public class FlashTunaConfiguration
    {
       

    }
    
}
