Imports Microsoft.VisualBasic
Imports System.Web.Mvc
Imports E4425.Models
Imports DevExpress.Web.Mvc
Imports System.Linq
Imports System

Namespace E4425.Controllers
    Public Class HomeController
        Inherits Controller
        Private entity As New WorldCitiesEntities()
        Public Function Index() As ActionResult
            Return View(entity.Customers.ToList())
        End Function
        Public Function GridViewPartial() As ActionResult
            Return PartialView(entity.Customers.ToList())
        End Function
        Public Function GridViewEditPartial(ByVal customerInfo As Customer) As ActionResult
            If ModelState.IsValid Then
                Try
                    entity.Customers.Attach(customerInfo)
                    Dim entry = entity.Entry(customerInfo)
                    entry.Property(Function(e) e.CityId).IsModified = True
                    entry.Property(Function(e) e.CountryId).IsModified = True
                    entry.Property(Function(e) e.CustomerName).IsModified = True
                    ' uncomment the next line to enable database updates
                    ' entity.SaveChanges();
                Catch e As Exception
                    ViewData("EditError") = e.Message
                End Try
            Else
                ViewData("EditError") = "Please, correct all errors."
            End If
            Return PartialView("GridViewPartial", entity.Customers.ToList())
        End Function
        Public Function GridViewInsertPartial(ByVal customerInfo As Customer) As ActionResult
            If ModelState.IsValid Then
                Try
                    entity.Customers.Add(customerInfo)
                    ' uncomment the next line to enable database updates
                    ' entity.SaveChanges();
                Catch e As Exception
                    ViewData("EditError") = e.Message
                End Try
            Else
                ViewData("EditError") = "Please, correct all errors."
            End If
            Return PartialView("GridViewPartial", entity.Customers.ToList())
        End Function
        Public Function GridViewDeletePartial(ByVal CustomerId As Integer) As ActionResult
            If ModelState.IsValid Then
                Try
                    entity.Customers.Remove(entity.Customers.Find(CustomerId))
                    'uncomment the next line to enable database updates
                    'entity.SaveChanges();
                Catch e As Exception
                    ViewData("EditError") = e.Message
                End Try
            Else
                ViewData("EditError") = "Please, correct all errors."
            End If
            Return PartialView("GridViewPartial", entity.Customers.ToList())
        End Function
        Public Function ComboBoxCountryPartial() As ActionResult
            Return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties)
        End Function
        Public Function ComboBoxCityPartial() As ActionResult
            Return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CityComboBoxProperties)
        End Function
    End Class
End Namespace