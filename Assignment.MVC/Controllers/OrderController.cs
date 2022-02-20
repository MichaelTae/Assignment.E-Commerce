using Assignment.MVC.Models;
using Assignment.MVC.Models.OrderModels;
using Assignment.MVC.Models.OrderRowModels;
using Assignment.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Assignment.MVC.Controllers
{
   
    public class OrderController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var orders = new OrderViewModel();          
            orders.OrderList = new List<OrderModel>();
            
           
            using (var client = new HttpClient())
            {
                orders.OrderList = await client.GetFromJsonAsync<List<OrderModel>>("https://localhost:7034/api/order?key=SecurityKey");
                
            }
           
            ViewBag.Orders = orders.OrderList;
           
            return View(orders);


        }
    }
}
