# GridView - How to implement cascaded combo boxes in the EditForm


<p>This example is an illustration of the <a href="https://www.devexpress.com/Support/Center/p/KA18675">KA18675: MVC ComboBox Extension - How to implement cascaded combo boxes</a> KB Article. Refer to the Article for an explanation.<br><br><strong>UPDATED:</strong><br><br></p>
<p>Starting with <strong>v16.1</strong>, it's not necessary to define the second combo box using the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebMvcMVCxGridViewColumn_SetEditItemTemplateContenttopic">MVCxGridViewColumn.SetEditItemTemplateContent</a> method to enable callbacks. <br>Use a new API instead

* <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebMvcMVCxGridViewColumn_EditorPropertiestopic">MVCxGridViewColumn.EditorProperties</a> 
* <a href="https://documentation.devexpress.com/#AspNet/clsDevExpressWebMvcMVCxColumnComboBoxPropertiestopic">MVCxColumnComboBoxProperties</a> 
* <a href="http://help.devexpress.com/#AspNet/DevExpressWebMvcGridExtensionBase_GetComboBoxCallbackResulttopic">GetComboBoxCallbackResult</a> <br> <br>You can find detailed steps by clicking the "Show Implementation Details" link below.</p>


<h3>Description</h3>

1.&nbsp;Use the&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebMvcMVCxGridViewColumn_EditorPropertiestopic">MVCxGridViewColumn.EditorProperties</a> method to define an editor at the column level.&nbsp;<br>
<code lang="cs">   settings.Columns.Add(c =&gt; c.CountryId, country =&gt;{
        country.Caption = "Country";
        country.EditorProperties().ComboBox(cs =&gt; cs.Assign(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties));
    });</code>
<code lang="vb">settings.Columns.Add(Function(c) c.CountryId, Sub(country)
                                                          country.Caption = "Country"
                                                          country.EditorProperties().ComboBox(Sub(cs)
                                                                  cs.Assign(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties)
                                                          End Sub)
End Sub)                                                                                    </code>
<br>2. Use the&nbsp; <a href="http://help.devexpress.com/#AspNet/DevExpressWebMvcGridExtensionBase_GetComboBoxCallbackResulttopic">GetComboBoxCallbackResult</a>&nbsp;method to handle a combo box callback on the server.<br>
<code lang="cs">public ActionResult ComboBoxCountryPartial(){
       return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties)
}</code>
<code lang="vb">Public Function ComboBoxCountryPartial() As ActionResult
       Return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties)
End Function</code>
<br><br>3. &nbsp;Use the&nbsp;&nbsp;<a href="https://documentation.devexpress.com/#AspNet/clsDevExpressWebMvcMVCxColumnComboBoxPropertiestopic">MVCxColumnComboBoxProperties</a>&nbsp;class to create combo box settings.&nbsp;The <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebMvcMVCxColumnComboBoxProperties_BindListtopic">MVCxColumnComboBoxProperties.BindList</a> &nbsp;method should be used to bind a column to data.&nbsp;<br>
<code lang="cs"> MVCxColumnComboBoxProperties countryComboBoxProperties;
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
  }</code>
<br>
<code lang="vb">Private _countryComboBoxProperties As MVCxColumnComboBoxProperties
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
End Function</code>

<br/>


