using System.Linq;

using MGTS.WFM.AgentRateImportLib.AgentRateRepository.Ef.Model;

namespace MGTS.WFM.AgentRateImportLib.AgentRateRepository
{
    public class AgentRepository: IAgentRepository
    {
        public WfmStvAgent[] GetAgents()
        {
            using (var agentRateEntities = new AgentRateEntities())
            {
                var res = agentRateEntities.Agents.ToArray();

                return res;
            }
        }
    }
}
