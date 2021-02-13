namespace SampleBlazorApp.Client.Shared
{
    public partial class NavMenu
    {
        public bool CollapseNavMenu { get; set; } = true;

        public string NavMenuCssClass => CollapseNavMenu ? "collapse" : null;

        public void ToggleNavMenu()
        {
            CollapseNavMenu = !CollapseNavMenu;
        }
    }
}
