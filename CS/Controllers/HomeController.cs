using System;
using System.Web.Mvc;
using E4425.Models;
using DevExpress.Web.Mvc;
using System.Linq;
namespace E4425.Controllers
{
    public class HomeController : Controller
    {
        WorldCitiesEntities entity = new WorldCitiesEntities();
        public ActionResult Index()
        {
            return View(entity.Customers.ToList());
        }
        public ActionResult GridViewPartial()
        {
            return PartialView(entity.Customers.ToList());
        }
        public ActionResult GridViewEditPartial(Customer customerInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entity.Customers.Attach(customerInfo);
                    var entry = entity.Entry(customerInfo);
                    entry.Property(e => e.CityId).IsModified = true;
                    entry.Property(e => e.CountryId).IsModified = true;
                    entry.Property(e => e.CustomerName).IsModified = true;
                    // uncomment the next line to enable database updates
                    //   entity.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("GridViewPartial", entity.Customers.ToList());
        }
        public ActionResult GridViewInsertPartial(Customer customerInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entity.Customers.Add(customerInfo);
                    // uncomment the next line to enable database updates
                    // entity.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("GridViewPartial", entity.Customers.ToList());
        }
        public ActionResult GridViewDeletePartial(int CustomerId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entity.Customers.Remove(entity.Customers.Find(CustomerId));
                    // uncomment the next line to enable database updates
                    //entity.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("GridViewPartial", entity.Customers.ToList());
        }
        public ActionResult ComboBoxCountryPartial()
        {
            return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CountryComboBoxProperties);
        }
        public ActionResult ComboBoxCityPartial()
        {
            return GridViewExtension.GetComboBoxCallbackResult(ComboBoxPropertiesProvider.Current.CityComboBoxProperties);
        }
    }

}