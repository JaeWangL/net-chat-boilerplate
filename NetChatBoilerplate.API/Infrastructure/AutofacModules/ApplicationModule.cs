namespace NetChatBoilerplate.API.Infrastructure.AutofacModules
{
    using Autofac;
    using NetChatBoilerplate.Domain.AggregatesModel.Doctor;
    using NetChatBoilerplate.Infrastructure.Repositories;

    public class ApplicationModule : Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            this.QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DoctorRepository>()
                .As<IDoctorRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
