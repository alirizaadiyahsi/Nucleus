using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nucleus.Utilities.Extensions.Collections
{
    /// <summary>
    /// This contains the extension methods for registering classes automatically
    /// https://github.com/JonPSmith/NetCore.AutoRegisterDi
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// This finds all the public, non-generic, non-nested classes in an assembly
        /// in the provided assemblies
        /// </summary>
        /// <param name="services">the NET Core dependency injection service</param>
        /// <param name="assemblies">Each assembly you want scanned </param>
        /// <returns></returns>
        public static AutoRegisterData RegisterAssemblyPublicNonGenericClasses(
            this IServiceCollection services, 
            params Assembly[] assemblies)
        {
            var allPublicTypes = assemblies
                .SelectMany(x =>
                    x.GetExportedTypes()
                        .Where(y => y.IsClass && !y.IsAbstract && !y.IsGenericType && !y.IsNested));

            return new AutoRegisterData(services, allPublicTypes);
        }

        /// <summary>
        /// This allows you to filter the classes in some way.
        /// For instance <code>Where(c =\> c.Name.EndsWith("Service")</code> would only register classes who's name ended in "Service"
        /// </summary>
        /// <param name="autoRegisterData"></param>
        /// <param name="predicate">A function that will take a type and return true if that type should be included</param>
        /// <returns></returns>
        public static AutoRegisterData Where(this AutoRegisterData autoRegisterData, Func<Type, bool> predicate)
        {
            if (autoRegisterData == null)
            {
                throw new ArgumentNullException(nameof(autoRegisterData));
            }
            autoRegisterData.TypeFilter = predicate;

            return new AutoRegisterData(autoRegisterData.Services, autoRegisterData.TypesToConsider.Where(predicate));
        }

        /// <summary>
        /// This registers the classes against any public interfaces (other than IDisposable) implemented by the class
        /// </summary>
        /// <param name="autoRegisterData">AutoRegister data produced by <see cref="RegisterAssemblyPublicNonGenericClasses"/></param> method
        /// <param name="lifetime">Allows you to define the lifetime of the service - defaults to ServiceLifetime.Transient</param>
        /// <returns></returns>
        public static IServiceCollection AsPublicImplementedInterfaces(
            this AutoRegisterData autoRegisterData, 
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (autoRegisterData == null)
            {
                throw new ArgumentNullException(nameof(autoRegisterData));
            }

            var classTypes = autoRegisterData.TypeFilter == null
                ? autoRegisterData.TypesToConsider
                : autoRegisterData.TypesToConsider.Where(autoRegisterData.TypeFilter);

            foreach (var classType in classTypes)
            {
                var interfaces = classType.GetTypeInfo().ImplementedInterfaces
                    .Where(i => i != typeof(IDisposable) && i.IsPublic);

                foreach (var interInterface in interfaces)
                {
                    autoRegisterData.Services.Add(new ServiceDescriptor(interInterface, classType, lifetime));
                }
            }

            return autoRegisterData.Services;
        }
    }

    /// <summary>
    /// This holds the data passed between the various stages of the AutoRegisterDi extension methods
    /// </summary>
    public class AutoRegisterData
    {
        /// <summary>
        /// RegisterAssemblyPublicNonGenericClasses uses this to create the initial data
        /// </summary>
        /// <param name="services"></param>
        /// <param name="typesToConsider"></param>
        public AutoRegisterData(IServiceCollection services, IEnumerable<Type> typesToConsider)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            TypesToConsider = typesToConsider ?? throw new ArgumentNullException(nameof(typesToConsider));
        }

        /// <summary>
        /// This carries the service register 
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// This holds the class types found by the RegisterAssemblyPublicNonGenericClasses
        /// </summary>
        public IEnumerable<Type> TypesToConsider { get; }

        /// <summary>
        /// This holds an options test method which will be applied using a Where clause to filter the classes
        /// If the TypeFiler is null, then no filtering is done
        /// </summary>
        public Func<Type, bool> TypeFilter { get; set; }
    }
}
