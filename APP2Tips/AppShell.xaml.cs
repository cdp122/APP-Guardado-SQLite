namespace APP2Tips
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Register routes for pages used with Shell navigation
            Routing.RegisterRoute("stacktechdetail", typeof(Pages.StackTechDetailPage));
            Routing.RegisterRoute("tipslist", typeof(Pages.Tips.TipsListPage));
            Routing.RegisterRoute("tipdetail", typeof(Pages.Tips.TipDetailPage));
        }
    }
}
