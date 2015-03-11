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
    public class OrderLineController : TableController<OrderLine>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            myshopContext context = new myshopContext();
            DomainManager = new EntityDomainManager<OrderLine>(context, Request, Services);
        }

        // GET tables/OrderLine
        public IQueryable<OrderLine> GetAllOrderLine()
        {
            return Query(); 
        }

        // GET tables/OrderLine/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<OrderLine> GetOrderLine(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/OrderLine/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<OrderLine> PatchOrderLine(string id, Delta<OrderLine> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/OrderLine
        public async Task<IHttpActionResult> PostOrderLine(OrderLine item)
        {
            OrderLine current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/OrderLine/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteOrderLine(string id)
        {
             return DeleteAsync(id);
        }

    }
}