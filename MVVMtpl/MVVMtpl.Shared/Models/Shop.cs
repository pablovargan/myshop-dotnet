namespace MVVMtpl.Models
{
    using MVVMtpl.Base;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Shop : ObservableObject
    {
        private bool isPublic;
        private ICollection<Order> orders;

        [JsonProperty("public")]
        public bool IsPublic
        {
            get { return this.isPublic; }
            set { this.Set(ref this.isPublic, value); }
        }
        [JsonProperty("orders")]
        public ICollection<Order> Orders
        {
            get { return this.orders; }
            set { this.Set(ref this.orders, value); }
        }
    }
}
