using FirstMVCApp.Infrastructure;
using FirstMVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FirstMVCApp.Controllers
{
    public class CustomersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new CustomersViewModel();
            return View(model);
        }

        ICustomerRepository _repository;
        public CustomersController(ICustomerRepository repo) => _repository = repo;
        

        [HttpPost]
        public async Task<IActionResult> Index(CustomersViewModel model)
        {
            IEnumerable<Customer> custList = null; 

            if(model.SelectedFilter == "Country")
            {
                //Task<IEnumerable<Customer>> task = _repository.GetCustomersByCountryAsync(model.SearchTerm);
                //task.Start(); 
                //Some aother tasks to executed.......
                custList = await _repository.GetCustomersByCountryAsync(model.SearchTerm);
            }
            else if (model.SelectedFilter == "City")
            {
                custList = await _repository.GetCustomersByCityAsync(model.SearchTerm);
            }
            else if (model.SelectedFilter == "Text")
            {
                custList = await _repository.GetAllCustomersAsync(model.SearchTerm);
            }
            if(custList==null) 
                custList = new List<Customer>();

            model.Customers = custList.ToList(); 
                
                model.Customers = custList?.ToList() ?? new List<Customer>();

            return View(model);
        }
    }
}
