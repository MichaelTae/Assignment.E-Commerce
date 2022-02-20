using Assignment.MVC.Helpers;
using Assignment.MVC.Models;
using Assignment.MVC.Models.CartModels;
using Assignment.MVC.Models.OrderModels;
using Assignment.MVC.Models.ProductModels;
using Assignment.MVC.Models.ViewModels;
using Assignment.StoreApi.Models.CreateModels;
using Assignment.StoreApi.Models.Entities;
using Assignment.StoreApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Assignment.MVC.Controllers
{
    
    public class HomeController : Controller
    {
        
        
       
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var prodModel = new ProductViewModel();
            prodModel.Products = new List<ProductModel>();
            using(var client = new HttpClient())
            {
                prodModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7034/api/Product?key=SecurityKey");
            }
            ViewBag.ProductList = prodModel.Products;
            ViewBag.ShoppingCart = SessionHelper.GetObjectAsJson<List<CartItem>>(HttpContext.Session, "shoppingCart");


            return View(prodModel);
        }
        public async Task<IActionResult> AddToCart(int id)
        {
            var prodModel = new ProductViewModel();
            prodModel.Products = new List<ProductModel>();

            using(var client = new HttpClient())
            {
                prodModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7034/api/Product?key=SecurityKey");
            }


            ViewBag.Product = prodModel.Products;
            ViewBag.ShoppingCart = SessionHelper.GetObjectAsJson<List<CartItem>>(HttpContext.Session, "shoppingCart");

            if (SessionHelper.GetObjectAsJson<List<CartItem>>(HttpContext.Session, "shoppingCart") == null)
            {
                var shoppingCart = new List<CartItem>();             
                shoppingCart.Add(new CartItem() { Product = prodModel.Products.FirstOrDefault(x => x.Id == id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "shoppingCart", shoppingCart);
            }
            else
            {
               
                List<CartItem> shoppingCart = SessionHelper.GetObjectAsJson<List<CartItem>>(HttpContext.Session, "shoppingCart");
                int index = ItemExists(id);
                
                    shoppingCart.Add(new CartItem() { Product = prodModel.Products.FirstOrDefault(x => x.Id == id), Quantity = 1 });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "shoppingCart", shoppingCart);
            }

            return RedirectToAction("Index");
        }

        public int ItemExists(int id)
        {
            List<CartItem> shoppingCart = SessionHelper.GetObjectAsJson<List<CartItem>>(HttpContext.Session, "shoppingCart");
            for (int i = 0; i < shoppingCart.Count; i++)
            {
                if (shoppingCart[i].Product.Id == id)
                    return i;
            }

            return -1;
        }

        
        public async Task<ActionResult<List<CartItem>>> PlaceOrder()
        {
            List<CartItem> shoppingCart = SessionHelper.GetObjectAsJson<List<CartItem>>(HttpContext.Session, "shoppingCart");
            OrderEntity order = new OrderEntity();
            OrderRowEntity orderRow = new OrderRowEntity();
            order.CustomerName = User.Identity.Name;
            order.OrderCreated = DateTime.Now;
            order.Quantity = shoppingCart.Count;
            foreach(var item in shoppingCart)
            {
                order.TotalPrice += item.Product.Price;
            }
            
             ViewBag.TotalPrice = order.TotalPrice;

            using(var client = new HttpClient())
            {
                await client.PostAsJsonAsync("https://localhost:7034/api/Order?key=SecurityKey", order);

            }
                          
            foreach(var item in shoppingCart)
            {
                
                orderRow.ProductId = item.Product.Id;                             
                orderRow.Quantity = item.Quantity;
                orderRow.Price = item.Product.Price;

                using (var client = new HttpClient())
                {
                    await client.PostAsJsonAsync("https://localhost:7034/api/OrderRow?key=SecurityKey", orderRow);
                }
            }

            HttpContext.Session.Clear();
            return View(shoppingCart);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


    
    



}