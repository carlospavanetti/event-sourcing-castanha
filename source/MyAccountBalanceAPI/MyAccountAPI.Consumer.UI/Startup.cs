﻿namespace MyAccountAPI.Consumer.UI
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using MyAccountAPI.Domain.ServiceBus;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Reflection;
    using System.Threading;
    using MyAccountAPI.Consumer.Application.DomainEventHandlers.Customers;
    using System.IO;
    using System.Linq;
    using System.Runtime.Loader;
    using Autofac.Configuration;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        IServiceProvider serviceProvider;

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            LoadInfrastructureAssemblies();

            services.AddMediatR(typeof(RegisteredEventHandler).GetTypeInfo().Assembly);

            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ConfigurationModule(Configuration));

            serviceProvider = new AutofacServiceProvider(builder.Build());

            return serviceProvider;
        }

        private void LoadInfrastructureAssemblies()
        {
            string[] fileNames = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly)
                .Where(filePath => Path.GetFileName(filePath).StartsWith("MyAccountAPI.Consumer.Infrastructure", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            foreach (string file in fileNames)
                AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
        }

        public void Run()
        {
            IMediator mediator = serviceProvider.GetService<IMediator>();
            ISubscriber subscriber = serviceProvider.GetService<ISubscriber>();

            subscriber.Listen(mediator);

            while (true)
            {
                Console.WriteLine($"{DateTime.Now.ToString()} Waiting for events..");
                Thread.Sleep(1000 * 60);
            }
        }
    }
}
