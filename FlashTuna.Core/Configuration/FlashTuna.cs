using FlashTuna.Core.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

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

        public class FlashTunaBuilder
        {
            private ITimeLine _timeLine;
            private string _moduleName;

            public string ModuleName { get => _moduleName; set => _moduleName = value; }
            internal ITimeLine TimeLine { get => _timeLine; set => _timeLine = value; }

            public FlashTunaBuilder SetTimeLine(ITimeLine timeLine)
            {
                _timeLine = timeLine;
                return this;
            }
            public FlashTunaBuilder SetModuleName(string moduleName)
            {
                _moduleName = moduleName;
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
