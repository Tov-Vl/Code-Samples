using System;
using System.Collections.Generic;
using System.Text;

using MGTS.WFM.AgentRateImportLib.AgentRateRepository;

namespace MGTS.WFM.AgentRateImportLib
{
    public class ImportService: IImportService
    {
        readonly IAgentRepository _agentRepository;
        readonly IAgentService _agentService;

        public ImportService(
            IAgentRepository agentRateRepository,
            IAgentService agentService)
        {
            _agentRepository = agentRateRepository ?? throw new ArgumentNullException();
            _agentService = agentService ?? throw new ArgumentNullException();
        }

        public void Import()
        {
            var agents = _agentRepository.GetAgents();

            foreach (var agent in agents)
                _agentService.Update(agent);
        }
    }
}
