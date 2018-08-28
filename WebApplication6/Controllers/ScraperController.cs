using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class ScraperController : ApiController
    {

        Scrapper[] products = new Scrapper[]
{
            new Scrapper { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Scrapper { Id = 2, Name = "Apple ", Category = "Fruits", Price = 3.75M },
            new Scrapper { Id = 3, Name = "Orange", Category = "Fruits", Price = 16.99M }
};

        public IEnumerable<Scrapper> GetAllProducts()
        {
            return products;
        }
            
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        public IHttpActionResult GetUrlSource(string url, string url2)
        {
            url = url.Substring(0, 4) != "http" ? "http://" + url : url;
            string htmlCode = "";
            var webcontent = new Content();
            using (WebClient client = new WebClient())
            {
                try
                {
                    htmlCode = client.DownloadString(url);
                    webcontent.htmlcontent = htmlCode;
                }
                catch (Exception ex)
                {
                    return NotFound();

                }
            }
            return Ok(webcontent);
        }


    }
}
