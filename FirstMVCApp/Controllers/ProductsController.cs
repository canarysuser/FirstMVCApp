using FirstMVCApp.Infrastructure;
using FirstMVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstMVCApp.Controllers
{
    public class ProductsController : Controller
    {
        IProductRepository _productRepository;
        DependencyClass _dc; 
        public ProductsController(IProductRepository repository, DependencyClass dc)
        {
            _productRepository = repository;
            _dc = dc;
            //_productRepository = new ProductListRepository();
            //var obj = Activator.CreateInstance(typeof(ProductListRepository));
            //var ctrl = Activator.CreateInstance(typeof(ProductsController), obj) as ProductsController;
            //typeof(ProductsController).InvokeMember("Index", )
        }
        public string GetGuid()
        {
            return _dc.GetId();
        }

        //ProductsController c = new ProductsController()

        //HTTP GET: default 
        [HttpGet]
        public IActionResult Index()
        {
            var products = _productRepository.GetAllProducts();
            return View(model: products);
        }
        //HTTP GET: 
        [HttpGet]
        [ActionName("Details")]
        public IActionResult GetDetailsByProductId(int id)
        {
            var model = _productRepository.GetProductById(id);
            return View(model);
        }
        //URL: /Products/price/111
        //[ActionName("price")]
        //public void GetAllProductsByPrice()
        //{

        //}

        [HttpGet]
        public IActionResult Create()
        {
            var model = new Product();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            //Server side validation
            if (ModelState.IsValid)
            {
                _productRepository.AddProduct(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
            /*if (model.ProductName.Length < 3)
            {
                ModelState.AddModelError("ProductName",
                    "Product Name must be at least 3 characters long.");
            return View(model);
        }
            else
            {
                ModelState.AddModelError("", "");
            _productRepository.AddProduct(model);
            return RedirectToAction("Index");
            }*/

            // }
        }

        public IActionResult Edit(int id)
        {
            var model = _productRepository.GetProductById(id);
            if (model is not null)
                return View(model);
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(int id, Product model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _productRepository.UpdateProduct(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _productRepository.GetProductById(id);
            if (model is not null)
                return View(model);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
