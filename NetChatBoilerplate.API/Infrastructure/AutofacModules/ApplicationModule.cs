namespace NetChatBoilerplate.API.Infrastructure.AutofacModules
{
    using Autofac;
    using NetChatBoilerplate.Domain.AggregatesModel.Chat;
    using NetChatBoilerplate.Domain.AggregatesModel.Profile;
    using NetChatBoilerplate.Infrastructure.Repositories.Chat;
    using NetChatBoilerplate.Infrastructure.Repositories.Profile;

    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Chat
            builder.RegisterType<MessageRepository>()
                .As<IMessageRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ParticipantRepository>()
                .As<IParticipantRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RoomRepository>()
                .As<IRoomRepository>()
                .InstancePerLifetimeScope();

            // Profile
            builder.RegisterType<DoctorRepository>()
                .As<IDoctorRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PatientRepository>()
                .As<IPatientRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
