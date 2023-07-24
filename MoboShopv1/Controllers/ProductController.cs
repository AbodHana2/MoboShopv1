using Microsoft.AspNetCore.Mvc;
using MoboShopv1.Models;
using MoboShopv1.Models.Interfaces;
using MoboShopv1.Models.ViewModels;

namespace MoboShopv1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unit;
        public ProductController(IUnitofWork unit)
        {
            _unit = unit;
        }

        public ActionResult Index()
        {
            var vm = new ProductViewModel();
            vm.Mobile = _unit.product.GetAllByType(ProductType.Mobile);
            vm.Hardware = _unit.product.GetAllByType(ProductType.Hardware);
            vm.Accessoris = _unit.product.GetAllByType(ProductType.Accessoris);
            return View(vm);
        }

        public ActionResult Details(int id)
        {
            if(id == 0)
            {
                return RedirectToAction("Index");
            }
            var product = _unit.product.GetOne(id);
            return View(product);
        }
    }
}
