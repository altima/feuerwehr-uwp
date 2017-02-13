using AlarmDisplay.Services;
using Prism.Unity.Windows;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace AlarmDisplay
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Prism.Unity.Windows.PrismUnityApplication" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IXamlMetadataProvider" />
    public sealed partial class App : PrismUnityApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Override this method with the initialization logic of your application. Here you can initialize services, repositories, and so on.
        /// </summary>
        /// <param name="args">The <see cref="T:Windows.ApplicationModel.Activation.IActivatedEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            Container.RegisterType<IDnssdService, DnssdService>();
            Container.RegisterType<IHttpdService, HttpdService>();
            Container.RegisterInstance<IGpioService>(new GpioService(EventAggregator), new ContainerControlledLifetimeManager());
            Container.RegisterType<IAlarmTimerService, AlarmTimerService>();

            return base.OnInitializeAsync(args);
        }

        /// <summary>
        /// Override this method with logic that will be performed after the application is initialized. For example, navigating to the application's home page.
        /// Note: This is called whenever the app is launched normally (start menu, taskbar, etc.) but not when resuming.
        /// </summary>
        /// <param name="args">The <see cref="T:Windows.ApplicationModel.Activation.LaunchActivatedEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            //Container.Resolve<IHttpdService>().Restart();
            //Container.Resolve<IGpioService>().Init();
            //Container.Resolve<IAlarmTimerService>().Restart();

            NavigationService.Navigate("Main", null);
            Window.Current.Activate();
            return Task.FromResult<object>(null);
        }
    }
}
