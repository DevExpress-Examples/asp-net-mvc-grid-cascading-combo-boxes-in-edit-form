'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------


Imports Microsoft.VisualBasic
	Imports System
	Imports System.Collections.Generic
Namespace E4425.Models

	Partial Public Class Country
		Public Sub New()
			Me.Cities = New HashSet(Of City)()
			Me.Customers = New HashSet(Of Customer)()
		End Sub

		Private privateCountryId As Integer
		Public Property CountryId() As Integer
			Get
				Return privateCountryId
			End Get
			Set(ByVal value As Integer)
				privateCountryId = value
			End Set
		End Property
		Private privateCountryName As String
		Public Property CountryName() As String
			Get
				Return privateCountryName
			End Get
			Set(ByVal value As String)
				privateCountryName = value
			End Set
		End Property
		Private privateCapitalId? As Integer
		Public Property CapitalId() As Integer?
			Get
				Return privateCapitalId
			End Get
			Set(ByVal value? As Integer)
				privateCapitalId = value
			End Set
		End Property

		Private privateCities As ICollection(Of City)
		Public Overridable Property Cities() As ICollection(Of City)
			Get
				Return privateCities
			End Get
			Set(ByVal value As ICollection(Of City))
				privateCities = value
			End Set
		End Property
		Private privateCity As City
		Public Overridable Property City() As City
			Get
				Return privateCity
			End Get
			Set(ByVal value As City)
				privateCity = value
			End Set
		End Property
		Private privateCustomers As ICollection(Of Customer)
		Public Overridable Property Customers() As ICollection(Of Customer)
			Get
				Return privateCustomers
			End Get
			Set(ByVal value As ICollection(Of Customer))
				privateCustomers = value
			End Set
		End Property
	End Class
End Namespace
