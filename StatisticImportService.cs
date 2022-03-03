using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGTS.WFM.AgentRateImportLib.Statistic
{
    public class StatisticImportService : IImportService
    {
        readonly IStatisticWatcher _statisticWatcher;
        readonly IStatisticInfo _statisticInfo;
        readonly IStatisticReporter _statisticReporter;

        public StatisticImportService(
            IStatisticWatcher statisticWatcher,
            IStatisticInfo statisticInfo,
            IStatisticReporter statisticReporter)
        {
            _statisticWatcher = statisticWatcher ?? throw new ArgumentNullException();
            _statisticInfo = statisticInfo ?? throw new ArgumentNullException();
            _statisticReporter = statisticReporter ?? throw new ArgumentNullException();
        }

        public IImportService Target { get; set; }

        public void Import()
        {
            if (Target == null)
                throw new ArgumentNullException();

            _statisticWatcher.Start();

            Target.Import();

            _statisticWatcher.Stop();

            _statisticReporter.Report(_statisticInfo);
        }
    }
}
