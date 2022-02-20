using Assignment.MVC.Helpers;
using Assignment.MVC.Models;
using Assignment.MVC.Models.CartModels;
using Assignment.MVC.Models.ProductModels;
using Assignment.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.MVC.Controllers
{
    public class ProductController : Controller
    {
       


        
        public async Task <IActionResult>Details(int Id)
        {
            var viewModel = new ProductDetailsModel();
            viewModel.Product = new ProductModel();

            using (var client = new HttpClient())
            {
                viewModel.Product = await client.GetFromJsonAsync<ProductModel>("https://localhost:7034/api/Product/" + Id + "?key=SecurityKey");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Computers(string value)
        {
            var viewModel = new ProductViewModel();
            viewModel.Products = new List<ProductModel>();

            using (var client = new HttpClient())
            {
               

                viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7034/api/Product/subCategory?subCategory=" + $"{value}" + "&key=SecurityKey");

            }

            

            return View(viewModel);
            
        }

        public async Task<IActionResult> Accessories(string value)
        {
            var viewModel = new ProductViewModel();
            viewModel.Products = new List<ProductModel>();

            using (var client = new HttpClient())
            {               

                viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7034/api/Product/subCategory?subCategory=" + $"{value}" + "&key=SecurityKey");

            }
            return View(viewModel);
        }
        public async Task<IActionResult> ComputerComponents(string value)
        {
            var viewModel = new ProductViewModel();
            viewModel.Products = new List<ProductModel>();

            using (var client = new HttpClient())
            {
                
                viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7034/api/Product/subCategory?subCategory=" + $"{value}" + "&key=SecurityKey");

            }
            return View(viewModel);
        }


        public async Task<IActionResult> SearchBar(string value)
        {
            var viewModel = new ProductViewModel();
            viewModel.Products = new List<ProductModel>();

            using (var client = new HttpClient())
            {

                viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7034/api/Product/subCategory?subCategory=" + $"{value}" + "&key=SecurityKey");

            }
            return View(viewModel);

        }


    }




}
