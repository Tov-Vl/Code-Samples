using System;
using System.Collections.Generic;
using System.Text;

using SoServiceReference;

namespace MGTS.WFM.AgentRateImportLib.SoRepositories
{
    public class CachingSoEngineerRepository : ISoEngineerRepository
    {
        private Engineer[] _cache;

        public ISoEngineerRepository Target { get; set; }

        public Engineer[] GetSoEngineers()
        {
            if (Target == null)
                throw new InvalidOperationException();

            if (_cache == null)
                _cache = Target.GetSoEngineers();

            return _cache;
        }
    }
}