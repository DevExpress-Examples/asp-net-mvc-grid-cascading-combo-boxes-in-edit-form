<!-- default file list -->
*Files to look at*:

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
<!-- default file list end -->
# GridView - How to implement cascaded combo boxes in the EditForm
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e4425/)**
<!-- run online end -->

<p>This example is an illustration of the <a href="https://www.devexpress.com/Support/Center/p/KA18675">KA18675: MVC ComboBox Extension - How to implement cascaded combo boxes</a> KB Article. Refer to the Article for an explanation.<br><br><strong>UPDATED:</strong><br><br></p>
<p>Starting with <strong>v16.1</strong>, it's not necessary to define the second combo box using the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebMvcMVCxGridViewColumn_SetEditItemTemplateContenttopic">MVCxGridViewColumn.SetEditItemTemplateContent</a> method to enable callbacks. <br>Use a new API instead

* <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebMvcMVCxGridViewColumn_EditorPropertiestopic">MVCxGridViewColumn.EditorProperties</a> 
* <a href="https://documentation.devexpress.com/#AspNet/clsDevExpressWebMvcMVCxColumnComboBoxPropertiestopic">MVCxColumnComboBoxProperties</a> 
* <a href="http://help.devexpress.com/#AspNet/DevExpressWebMvcGridExtensionBase_GetComboBoxCallbackResulttopic">GetComboBoxCallbackResult</a> <br> <br>You can find detailed steps by clicking the "Show Implementation Details" link below.</p>


<h3>Description</h3>

1.&nbsp;Use the&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebMvcMVCxGridViewColumn_EditorPropertiestopic">MVCxGridViewColumn.EditorProperties</a> method to define an editor at the column level.&nbsp;<br>
```cs
//CS
settings.Columns.Add(c => c.CountryId, country =>
{
	country.Caption = "Country";
	country.EditorProperties().ComboBox(cs => cs.Assign(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties));
});
```
```vb
''VB
settings.Columns.Add(Function(c) c.CountryId, Sub(country)
												  country.Caption = "Country"
												  country.EditorProperties().ComboBox(Sub(cs)
																						  cs.Assign(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties)
																					  End Sub)
											  End Sub)

```
<br>2. Use the&nbsp; <a href="http://help.devexpress.com/#AspNet/DevExpressWebMvcGridExtensionBase_GetComboBoxCallbackResulttopic">GetComboBoxCallbackResult</a>&nbsp;method to handle a combo box callback on the server.<br>
```cs
//CS
public ActionResult ComboBoxCountryPartial()
{
    return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties);
}
```
```vb
''VB
Public Function ComboBoxCountryPartial() As ActionResult
       Return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties)
End Function
```
<br><br>3. &nbsp;Use the&nbsp;&nbsp;<a href="https://documentation.devexpress.com/#AspNet/clsDevExpressWebMvcMVCxColumnComboBoxPropertiestopic">MVCxColumnComboBoxProperties</a>&nbsp;class to create combo box settings.&nbsp;The <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebMvcMVCxColumnComboBoxProperties_BindListtopic">MVCxColumnComboBoxProperties.BindList</a> &nbsp;method should be used to bind a column to data.&nbsp;<br>
```cs
//CS
MVCxColumnComboBoxProperties countryComboBoxProperties;
 public MVCxColumnComboBoxProperties CountryComboBoxProperties {
            get
            {
                if (countryComboBoxProperties == null)
                    countryComboBoxProperties = CreateCountryComboBox();
                return countryComboBoxProperties;
            }
  }
 protected MVCxColumnComboBoxProperties CreateCountryComboBox(){
            MVCxColumnComboBoxProperties cs = new MVCxColumnComboBoxProperties();
            cs.CallbackRouteValues = new { Controller = "Home", Action = "ComboBoxCountryPartial" };
            cs.Width = Unit.Percentage(100);
            cs.TextField = "CountryName";
            cs.ValueField = "CountryId";
            cs.ValueType = typeof(int);
            cs.IncrementalFilteringDelay = 1000;
            cs.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cs.FilterMinLength = 2;
            cs.CallbackPageSize = 20;
            cs.ClientSideEvents.SelectedIndexChanged = "CountriesCombo_SelectedIndexChanged";
            cs.BindList(WorldCities.Countries.ToList());
            return cs;
  }
```
```vb
''VB
Private _countryComboBoxProperties As MVCxColumnComboBoxProperties
        Public ReadOnly Property CountryComboBoxProperties() As MVCxColumnComboBoxProperties
            Get
                If _countryComboBoxProperties Is Nothing Then
                    _countryComboBoxProperties = CreateCountryComboBox()
                End If
                Return _countryComboBoxProperties
            End Get
End Property
Protected Function CreateCountryComboBox() As MVCxColumnComboBoxProperties
			Dim cs As New MVCxColumnComboBoxProperties()
			cs.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "ComboBoxCountryPartial"}
			cs.Width = Unit.Percentage(100)
			cs.TextField = "CountryName"
			cs.ValueField = "CountryId"
			cs.ValueType = GetType(Integer)
			cs.IncrementalFilteringDelay = 1000
			cs.IncrementalFilteringMode = IncrementalFilteringMode.Contains
			cs.FilterMinLength = 2
			cs.CallbackPageSize = 20
			cs.ClientSideEvents.SelectedIndexChanged = "CountriesCombo_SelectedIndexChanged"
			cs.BindList(WorldCities.Countries.ToList())
			Return cs
End Function
```


