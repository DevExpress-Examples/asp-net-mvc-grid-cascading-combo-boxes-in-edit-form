@Code
    ViewBag.Title = "GridView - How to implement cascading comboboxes in the EditForm"
End Code
<script type="text/javascript">
    var customCallback = false;
    var editingStart = false;
    var previousValue = null;
    function onBeginCallback(s, e) {
        if (e.command = "STARTEDIT")
            editingStart = true;
    }
    function onEndCallback(s, e) {
        if (editingStart) {
            customCallback = true;
            var editor = grid.GetEditor('CityId');
            if (editor) {
                previousValue = editor.GetValue();
                editor.PerformCallback();
            }
        }
    }
    function CountriesCombo_SelectedIndexChanged(s, e) {
        customCallback = true;
        grid.GetEditor('CityId').PerformCallback();
    }
    function CitiesCombo_BeginCallback(s, e) {
        e.customArgs['CountryId'] = grid.GetEditor('CountryId').GetValue();
    }
    function CitiesCombo_EndCallback(s, e) {
        var editor = MVCxClientComboBox.Cast(s);
        if (customCallback) {
            customCallback = false;
            if (previousValue) {
                editor.SetValue(previousValue);
                previousValue = null;
            } else {
                editor.SetSelectedIndex(0);
            }
        }
    }
</script>
<form>
    @Html.Partial("GridViewPartial")
</form>
