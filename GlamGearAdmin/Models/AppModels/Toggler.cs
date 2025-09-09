namespace GlamGearAdmin.Models.AppModels;

public class SidebarToggler
{
    public bool CollapseNavMenu { get; set; } // TODO: Toggle sidebar visibility

    // public event Action? OnChange;

    // public void ToggleNav()
    // {
    //     CollapseNavMenu = !CollapseNavMenu;
    //     OnChange?.Invoke();
    // }
}

public enum NavSubmenu //* source code original reference: https://stackoverflow.com/a/66690427/14041392 *@
{
    None,
    FirstSubM,
    SecondSubM,
    ThirdSubM,
}