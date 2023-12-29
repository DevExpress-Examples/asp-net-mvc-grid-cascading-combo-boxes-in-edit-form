<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128550193/16.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4425)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Grid View for ASP.NET MVC - How to implement cascading combo boxes in the grid's edit form

This example demonstrates how to create cascading combo box editors and use them to edit grid data.

## Overview

Follow the steps below to implement cascading combo boxes in the grid's edit form:

1. Call a column's [MVCxGridViewColumn.EditorProperties](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.MVCxGridViewColumn.EditorProperties) method to add a combo box editor to the column.

    ```cshtml
    settings.Columns.Add(c => c.CountryId, country =>{
		country.Caption = "Country";
		country.EditorProperties().ComboBox(cs => cs.Assign(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties));
	});
	settings.Columns.Add(c => c.CityId, city =>{
		city.Caption = "City";
		city.EditorProperties().ComboBox(cs => cs.Assign(ComboBoxPropertiesProvider.Current.CityComboBoxProperties));
	});
    ```

2. Add a [MVCxColumnComboBoxProperties](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.MVCxColumnComboBoxProperties) object to specify an editor's settings and call the [MVCxColumnComboBoxProperties.BindList](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.MVCxColumnComboBoxProperties.BindList.overloads) method to bind the column to a data source.

    ```csharp
    MVCxColumnComboBoxProperties countryComboBoxProperties;
    public MVCxColumnComboBoxProperties CountryComboBoxProperties {
        get {
            if(countryComboBoxProperties == null)
                countryComboBoxProperties = CreateCountryComboBox();
            return countryComboBoxProperties;
        }
    }
    protected MVCxColumnComboBoxProperties CreateCountryComboBox() {
        MVCxColumnComboBoxProperties cs = new MVCxColumnComboBoxProperties();
        cs.CallbackRouteValues = new { Controller = "Home", Action = "ComboBoxCountryPartial" };
        // ...
        cs.ClientSideEvents.SelectedIndexChanged = "CountriesCombo_SelectedIndexChanged";
        cs.BindList(WorldCities.Countries.ToList());
        return cs;
    }
    ```

3. Specify the secondary editor's `CallbackRouteValue` parameters.

4. Handle the primary editor's `SelectedIndexChanged` event. In the handler, call the secondary editor's `PerformCallback` method to update the editor's data.

    ```js
    function CountriesCombo_SelectedIndexChanged(s, e) {
        customCallback = true;
        grid.GetEditor('CityId').PerformCallback();
    }
    ```

5. Handle the secondary editor's client-side `BeginCallback` event and pass the selected value of the secondary editor as a parameter.

    ```js
    function CitiesCombo_BeginCallback(s, e) {
        e.customArgs['CountryId'] = grid.GetEditor('CountryId').GetValue();
    }
    ```

6. Use the grid's [GetComboBoxCallbackResult](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.GridExtensionBase.GetComboBoxCallbackResult.overloads) method to get the result of callback processing.

    ```csharp
    public ActionResult ComboBoxCountryPartial(){
        return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties);
    }
    ```

7. Call the secondary editor's `CallbackRouteValues.Action` method to populate the editor with values based on the passed parameter.

## Files to Review

* [HomeController.cs](./CS/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/Controllers/HomeController.vb))
* [Global.asax](./CS/Global.asax) (VB: [Global.asax](./VB/Global.asax))
* [Global.asax.cs](./CS/Global.asax.cs) (VB: [Global.asax.vb](./VB/Global.asax.vb))
* [City.cs](./CS/Models/City.cs) (VB: [City.vb](./VB/Models/City.vb))
* [ComboBoxPropertiesProvider.cs](./CS/Models/ComboBoxPropertiesProvider.cs) (VB: [ComboBoxPropertiesProvider.vb](./VB/Models/ComboBoxPropertiesProvider.vb))
* [Country.cs](./CS/Models/Country.cs) (VB: [Country.vb](./VB/Models/Country.vb))
* [Customer.cs](./CS/Models/Customer.cs) (VB: [Customer.vb](./VB/Models/Customer.vb))
* [WoldCitiesModel.Context.cs](./CS/Models/WoldCitiesModel.Context.cs) (VB: [WoldCitiesModel.Context.vb](./VB/Models/WoldCitiesModel.Context.vb))
* [WoldCitiesModel.cs](./CS/Models/WoldCitiesModel.cs) (VB: [WoldCitiesModel.vb](./VB/Models/WoldCitiesModel.vb))
* [GridViewPartial.cshtml](./CS/Views/Home/GridViewPartial.cshtml)
* [Index.cshtml](./CS/Views/Home/Index.cshtml)

## Documentation

* [MVC ComboBox Extension - How to implement cascaded combo boxes](https://supportcenter.devexpress.com/ticket/details/ka18675/mvc-combobox-extension-how-to-implement-cascaded-combo-boxes)
* [Passing Values to a Controller Action through Callbacks](https://docs.devexpress.com/AspNetMvc/9941/common-features/callback-based-functionality/passing-values-to-a-controller-action-through-callbacks)

## More Examples

* [ComboBox for ASP.NET MVC - How to implement cascading combo boxes](https://github.com/DevExpress-Examples/asp-net-mvc-cascading-combo-boxes)
