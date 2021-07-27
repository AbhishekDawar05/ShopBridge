using ShopBridgeWebapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShopBridgeWebapi.Controllers
{
    public class ProductController : ApiController
    {
        private ShopBridgeEntities db;
        public ProductController()
        {
            db = new ShopBridgeEntities();
        }

        [HttpGet]
        public HttpResponseMessage GetProduct()
        {
            var Products = db.Products.ToList();
            if (Products != null)
            { 
                return Request.CreateResponse(HttpStatusCode.OK,Products); 
            }
            else 
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, message:"Does not contain any product please insert it");
            }
            
        }

        [HttpGet]
        public HttpResponseMessage GetProduct([FromUri]int Productid)
        {
            if (!db.Products.Any(x => x.ID == Productid))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message: "Product Does not Exits for product id :-"+Productid);
            }
            else 
            {
                return Request.CreateResponse(HttpStatusCode.OK, db.Products.SingleOrDefault(x => x.ID == Productid));
            }
  
        }

        [HttpPost]
        public HttpResponseMessage PostProduct([FromBody] ProductModel CurrentProduct)
        {
            if (db.Products.Any(x => x.Name == CurrentProduct.Name))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Product is already Exits");
            }
            else 
            {
                Product productModel = new Product()
                {
                    Name = CurrentProduct.Name,
                    Price = CurrentProduct.Price,
                    Description = CurrentProduct.Description,
                    ManufacturedBY = CurrentProduct.ManufacturedBY,
                    Stock = CurrentProduct.Stock
                };
                db.Products.Add(productModel);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK,value:"Product is Successfully Added");
            }
           
        }


        [HttpPut]
        public HttpResponseMessage PutProduct([FromUri] int productid, [FromBody] ProductModel CurrentProduct) 
        {
            if (db.Products.Any(x => x.Name == CurrentProduct.Name && x.ID != productid))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Product Does not Exits for product id :-" + productid);
            }
            else
            {
                Product productModel = new Product();
                productModel = db.Products.SingleOrDefault(x=>x.ID==productid);
                productModel.Name = CurrentProduct.Name;
                productModel.Price = CurrentProduct.Price;
                productModel.Description = CurrentProduct.Description;
                productModel.ManufacturedBY = CurrentProduct.ManufacturedBY;
                productModel.Stock = CurrentProduct.Stock;
                db.SaveChanges();             

                return Request.CreateResponse(HttpStatusCode.OK, value: "Product is Successfully Updated");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteProduct([FromUri] int productid) 
        {
            if (!db.Products.Any(x=>x.ID==productid)) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Product Does Not Exits");
            }
            Product item = db.Products.SingleOrDefault(x => x.ID == productid);
            db.Products.Remove(item);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, value: "Product is Successfully Deleted");

        }
    }
}
