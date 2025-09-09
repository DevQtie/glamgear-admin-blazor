# GlamGearAdmin
GlamGear admin web application using ASP.NET Core (.NET 9) with Blazor

# Changelog

## Important note: Always utilize the Bootstrap v5.3.3 as it is already embedded to the project structure, and it should be used properly.

## 1.12.3.0-alpha

- The `CGGSchema1.0.3` was migrated with an additional `ActiveSubmenu` field to handle side menu toggling, which will make the state of the selected submenu persistent across page reloads.

## 1.12.2.0-alpha

- Toggle theme (light, dark, and auto).
- Toggle left-side navigation menu visibility.
- Implemented `method="post"` in ReviewUser Razor page's EditForm.

## 1.12.1.0-alpha

- Additional field for overall user list.
- Additional field for unverified user list.
- Additional field for verified user list.

## 1.12.0.0-alpha

- Note: If it is actually a simple model (for other Razor pages), this should not be used. I've left it as is for reference.
- I've documented this project's module. Please visit: https://stackoverflow.com/q/79748674/14041392 for reference.
- Handle validations.
- Verify user.
- Modify user remark
- Modify user role.
- Implemented the `Microsoft.AspNetCore.Components.DataAnnotations` to handle complex data class model. However, please read this warning message from the Microsoft's official docs: https://learn.microsoft.com/en-us/aspnet/core/blazor/forms/validation?view=aspnetcore-9.0#blazor-data-annotations-validation-package.

## 1.11.0.0-alpha

- Simulation of single and multiple image file uploads and database saving.

## 1.10.0.1-alpha

- Implementing small text throughout the @Body layout in `MainLayout.razor`.

## 1.10.0.0-alpha

- Review User module.
- Global method definitions inside the `SQLServerInnerHelper.cs` with optional parameters to mitigate overheads.

## 1.9.0.0-alpha

- User list module with minor enhancements.

## 1.8.1.1-alpha

- Footer and body layout appearance fixes.

## 1.8.1.0-alpha

- Additional footer in the `MainLayout.razor`
- Additional counting animations on the numeric values of the dashboard for total products, total orders, total users, and total sales.

## 1.8.0.0-alpha

- Upgraded to Bootstrap v5.3.7.
- Additional `InternetCheckerService.cs` as a service to check internet connectivity.
- Additional Blazor Bootstrap Charts.
- Dashboard layout revised.
- `MainLayout.razor` revised.
- StackOverFlow layout inspired.

## 1.7.6.0-alpha

- I added modal confirmation notice before logging out.

## 1.7.5.1-alpha

- Sidemenu layout refinements and revision.

## 1.7.5.0-alpha

- Logout with navigation-authorization security bug issue fixed.

## 1.7.4.0-alpha

- [COMPLETED] Separate the layout of the login page from the main layout with sidemenu. It should retain the authentication as functional, with bug fixes.

## 1.7.3.1-alpha

- TODO: Separate the layout of the login page from the main layout with sidemenu. It should retain the authentication as functional.

## 1.7.3.0-alpha

- Issues with launch URL, 404 not found in deployed web app on IIS resolved.

## 1.7.2.0-alpha

- The approach for getting the records of tables isn't the same as getting the response if the implicit transaction is executed because of handling the `OUTPUT` parameter response.
- Implementing `OUTPUT` parameter to resolve bug in getting the exact response from the stored procedure.

## 1.7.1.0-alpha

- Prevent navigating back upon successful deletion of records.

## 1.7.0.0-alpha

- Successful integration of Bootstrap Toasts.
- Successful implementation of Bootstrap Javascript.
- Deletion of records module added.
- Showing of details module added.

## 1.6.1.0-alpha

- Optimized `SQLServerHelper` class and `MinimalDbSettings` class to enforce code scalability, manageability, and maintainability.
- 1.6.0.0-alpha follow up changelog.

## 1.6.0.0-alpha

- Implementing #region and #endregion for block of codes management.
- The recommended `FromSql` currently supports static `SqlParameter` in the meantime.
- Implementing `SQLServerHelper` class and `MinimalDbSettings` class for code scalability and manageability.

## 1.5.0.1-alpha

- TODO: The `Auth.razor` page shouldn't be visible to user, if not authenticated, I prefer not to manage the content visibility through <Authorized> and <NotAuthorized> tags-->
- Authentication and redirection fixes.

## 1.5.0.0-alpha

- Added user authentication code generated using `dotnet aspnet-codegenerator blazor-identity -dbProvider sqlserver -dc GlamGearAdmin.Data.SQLServer.BlazorSqlServerAuthContext -lf`, for instruction reference: https://stackoverflow.com/a/79701566/14041392

## 1.4.0.1-alpha

- Header top and bottom.
- Show all per page bug fixed
- 1.4.0.0 TODO: (FIXED) A 'paper cut' bug occurs when the page number is out of bounds, especially if the total page count changes dynamically (if play around with per page against next page); kindly visit at `/sql_server/crud/admin_pages`.

## 1.4.0.0-alpha

- TODO: (Subject for fix) A 'paper cut' bug occurs when the page number is out of bounds, especially if the total page count changes dynamically (if play around with per page against next page); kindly visit at /sql_server/crud/admin_pages.
- Show all page by collaboration of ChatGPT and DevQt: https://chatgpt.com/s/t_6874cd7fc520819180ad4c3d7be53e5a
- Sorting function; assisted by ChatGPT with minor bug fixes by DevQt: https://chatgpt.com/s/t_6874ba90d3808191839d07d65f0e4830 
- Space between layout by utilizing the Bootstrap v5.3.3, assisted by ChatGPT: https://chatgpt.com/s/t_6874abf840188191a3b10382aad4ed5f
- Beautified search input; assisted by Copilot: https://copilot.microsoft.com/shares/vprbpLKbBM74VchNRz1Fj
- Per page with 15, 30, and 50 options with 15 as default.
- Pagination with client-side markup structure
- Basic implementation of SQLServerHelper.
- Optimized and improved Index.razor to display data fetched from SQL Server database.
- Successful connection to SQL Server 2017.

## 1.3.0.0-alpha

- Refactoring search feature from client-side rendering (CSR) into server-side rendering (SSR) using SignalR.

## 1.2.0.3-alpha

- BUG FIXED: Added `<Content Include="">` tag in `GlamGearAdmin.csproj` to include the SQLite database file generated by this command: `dotnet publish -c Release -r win-x64`

## 1.2.0.2-alpha

- Added `<Content Include="">` tag in `GlamGearAdmin.csproj` to include the SQLite database file generated by this command: `dotnet publish -c Release -r win-x64`
- CGGSchema1.0.2 with data model validation.

## 1.2.0.1-alpha

- Side nav menu layout fixes.
- Dashboard.razor bug fixes.

## 1.2.0.0-alpha

- SQLite database integrated with currently `Data/Migrations/CGGSchema1.0.0` file naming convention through `dotnet ef migrations add CGGSchema1.0.0 --output-dir Data/Migrations`.
- With initial `CRUD\AdminPages` features for Admin data model.

## 1.1.1.0-alpha

- Additional arrow-down and arrow-up indicator in the main menu with sub-menu.

## 1.1.0.2-alpha

- README.md file relocated.

## 1.1.0.1-alpha

- Folder hierarchy reorganized.

## 1.1.0.0-alpha

- Initial GlamGear admin web app static side menu, sub-menu, and their pages.

## 1.0.0.0-alpha

- Initial Blazor web app release.
