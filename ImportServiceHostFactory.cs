using D3K.Diagnostics.NLogExtensions;
using D3K.Diagnostics.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Injection;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Interception;
using Unity.Lifetime;
using Unity;

using MGTS.WFM.AgentRateImportLib;
using MGTS.WFM.AgentRateImportLib.AgentRateRepository;
using MGTS.WFM.AgentRateImportLib.AgentServices;
using MGTS.WFM.AgentRateImportLib.Settings;
using MGTS.WFM.AgentRateImportLib.SoRepositories;
using MGTS.WFM.AgentRateImportLib.SoServices;
using MGTS.WFM.AgentRateImportLib.Statistic;
using MGTS.WFM.Service.DependencyInjection;
using MGTS.WFM.So.Provider;
using MGTS.WFM.Unity;

using Lib = MGTS.WFM.AgentRateImportLib;

using SoServiceReference;

namespace MGTS.WFM.WCF.AgentRateImportService
{
    public class ImportServiceHostFactory : DependencyInjectionServiceHostFactory
    {
        protected override void RegisterDependencies()
        {
            RegisterDependencies(Container);
        }

        public void RegisterDependencies(IUnityContainer container)
        {
            var ii = new Interceptor<InterfaceInterceptor>();
            var ll = new InterceptionBehavior<MethodLogInterceptionBehavior>("log");
            var hl = new InterceptionBehavior<MethodLogInterceptionBehavior>("hashCodeLog");
            var tb = new InterceptionBehavior<MethodIdentityInterceptionBehavior>("pid");
            var eb = new InterceptionBehavior<ErrorHandlingBehavior>();

            container
                .AddNewExtension<Interception>()

                .RegisterMethodLogInterceptionBehavior<NLogLogListenerFactory>("log", "Debug")
                .RegisterMethodIdentityInterceptionBehavior<NLogLogContext>("pid", "pid")
                .RegisterHashCodeMethodLogInterceptionBehavior<NLogLogListenerFactory>("hashCodeLog", "Debug")

                .RegisterType<IImportService, ImportService>(ii, tb, ll)

                .RegisterType<Lib.IImportService, StatisticImportService>(new InjectionProperty("Target", new ResolvedParameter<Lib.IImportService>("importService")), ii, tb, ll)
                .RegisterType<Lib.IImportService, Lib.ImportService>("importService", ii, tb, ll)

                .RegisterType<IAgentRepository, AgentRepository>(ii, tb, ll)
                .RegisterType<IAgentService, CorrZeroRateAgentService>(new InjectionProperty("Target", new ResolvedParameter<IAgentService>("agentService")), ii, tb, ll)
                .RegisterType<IAgentService, AgentService>("agentService", ii, tb, eb, ll)

                .RegisterType<ISoStvRegionsMappingRepositoryEx, SoStvRegionsMappingRepositoryEx>(ii, tb, ll)
                .RegisterType<ISoEngineerRepositoryEx, SoEngineerRepositoryEx>(ii, tb, ll)
                .RegisterType<ISoSkillsFactory, SoSkillsFactory>(ii, tb, ll)
                .RegisterType<ISoSkillsComparer, SoSkillsComparer>(ii, tb, ll)
                .RegisterType<ISoEngineerService, StatisticSoEngineerService>(new InjectionProperty("Target", new ResolvedParameter<ISoEngineerService>("soEngineerService")), ii, tb, ll)
                .RegisterType<ISoEngineerService, SoEngineerService>("soEngineerService", ii, tb, ll)

                .RegisterType<ISoStvRegionsMappingRepository, CachingSoStvRegionsMappingRepository>(new PerResolveLifetimeManager(), new InjectionProperty("Target", new ResolvedParameter<ISoStvRegionsMappingRepository>("soStvRegionsMappingRepository")), ii, hl)
                .RegisterType<ISoStvRegionsMappingRepository, SoStvRegionsMappingRepository>("soStvRegionsMappingRepository", ii, tb, ll)
                .RegisterType<ISoEngineerRepository, CachingSoEngineerRepository>(new PerResolveLifetimeManager(), new InjectionProperty("Target", new ResolvedParameter<ISoEngineerRepository>("soEngineerRepository")), ii, hl)
                .RegisterType<ISoEngineerRepository, SoEngineerRepository>("soEngineerRepository", ii, tb, ll)

                .RegisterType<IProvider<CustStvRegionsMapping>, Provider<CustStvRegionsMapping>>(ii, ll)
                .RegisterType<IProvider<Engineer>, Provider<Engineer>>(ii, ll)

                .RegisterType<IServiceOptimizationServiceClientSettings, XmlServiceOptimizationServiceClientSettings>(ii, ll)
                .RegisterType<ServiceOptimizationService, ServiceOptimizationServiceClient>(new InjectionConstructor(new ResolvedParameter<IServiceOptimizationServiceClientSettings>()),ii, tb, ll)

                // Statistic
                .RegisterType<IStatisticReporter, MultiStatisticReporter>(ii, ll)
                .RegisterType<IStatisticReporter, MailStatisticReporter>("mailStatisticReporter", ii, ll)
                .RegisterType<IMailStatisticReporterSettings, XmlMailStatisticReporterSettings>(ii, ll)
                .RegisterType<IStatisticReportFactory, StatisticReportFactory>(ii, ll)
                .RegisterType<IMailCredentialSettings, XmlMailCredentialSettings>(ii, ll)

                .RegisterType<StatisticBag>(new PerResolveLifetimeManager())
                .RegisterType<IDateTimeProvider, DateTimeProvider>(ii, ll)
                .RegisterType<IStatisticBag, StatisticBag>(ii, ll)
                .RegisterType<IStatisticWatcher, StatisticBag>(ii, ll)
                .RegisterType<IStatisticInfo, StatisticBag>(ii, ll)
              ;
        }
    }
}
