using Microsoft.Extensions.DependencyInjection;

namespace APP2Tips
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // AppShell defines the startup page and navigation. Do not set MainPage (deprecated).
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());
            return window;
        }
    }
}