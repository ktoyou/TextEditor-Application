using Microsoft.Extensions.DependencyInjection;
using System;
using TextEditor.Models;
using TextEditor.ViewModels;

namespace TextEditor.Services
{
    internal static class ServiceContainer
    {
        private static readonly IServiceProvider _provider;

        static ServiceContainer()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<ISettingsManager, SettingsManager>();
            services.AddSingleton<EditorViewModel>();

            _provider = services.BuildServiceProvider();
        }

        public static T GetViewModel<T>() where T : BaseViewModel => _provider.GetService<T>();
    }
}
