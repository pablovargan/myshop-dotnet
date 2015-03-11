using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using myshopService.DataObjects;
using myshopService.Models;

namespace myshopService.Controllers
{
    public class OrderController : TableController<Order>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            myshopContext context = new myshopContext();
            DomainManager = new EntityDomainManager<Order>(context, Request, Services);
        }

        // GET tables/Order
        public IQueryable<Order> GetAllOrder()
        {
            return Query(); 
        }

        // GET tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Order> GetOrder(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Order> PatchOrder(string id, Delta<Order> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Order
        public async Task<IHttpActionResult> PostOrder(Order item)
        {
            Order current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Order/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteOrder(string id)
        {
             return DeleteAsync(id);
        }

    }
}