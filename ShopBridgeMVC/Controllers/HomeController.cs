using Newtonsoft.Json;
using ShopBridgeMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace ShopBridgeMVC.Controllers
{
    public class HomeController : Controller
    {

        private static string Webapiurl = "https://localhost:44301/api/";
        public async Task<ActionResult> Index()
        {
            List<ProductViewModel> List = new List<ProductViewModel>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(Webapiurl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                var responsemessage = await client.GetAsync(requestUri: "Product");
                if (responsemessage.IsSuccessStatusCode)
                {
                    var itemlist = responsemessage.Content.ReadAsStringAsync().Result;
                    List = JsonConvert.DeserializeObject<List<ProductViewModel>>(itemlist);
                }
            }
            return View(List);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int productid)

        {
            ProductViewModel Product = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(Webapiurl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                string url = "Product?Productid=" + productid;
                var responsemessage = await client.GetAsync(requestUri: "Product?Productid=" + productid);
                if (responsemessage.IsSuccessStatusCode)
                {
                    var item = responsemessage.Content.ReadAsStringAsync().Result;
                    Product = JsonConvert.DeserializeObject<ProductViewModel>(item);
                }
            }
            return View(Product);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(ProductViewModel Product)
        {
            if (ModelState.IsValid)
            {
                string CustomMessage = string.Empty;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.BaseAddress = new Uri(Webapiurl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                    var responsemessage = await client.PostAsJsonAsync(requestUri: "Product", Product);
                    if (responsemessage.IsSuccessStatusCode)
                    {
                        var item = responsemessage.Content.ReadAsStringAsync().Result;
                        CustomMessage = JsonConvert.DeserializeObject<string>(item);
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProductViewModel Product)
        {
            if (ModelState.IsValid)
            {
                string CustomMessage = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(Webapiurl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                var responsemessage = await client.PutAsJsonAsync(requestUri: "Product?Productid=" + Product.ID, Product);
                if (responsemessage.IsSuccessStatusCode)
                {
                    var item = responsemessage.Content.ReadAsStringAsync().Result;
                    CustomMessage = JsonConvert.DeserializeObject<string>(item);
                }
            }
            return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int productid)
        {


            string CustomMessage = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(Webapiurl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                var responsemessage = await client.DeleteAsync(requestUri: "Product?Productid=" + productid);
                if (responsemessage.IsSuccessStatusCode)
                {
                    var item = responsemessage.Content.ReadAsStringAsync().Result;
                    CustomMessage = JsonConvert.DeserializeObject<string>(item);
                }
            }
            return RedirectToAction("Index");

        }
    }
}