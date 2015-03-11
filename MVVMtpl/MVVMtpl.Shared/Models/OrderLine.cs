namespace MVVMtpl.Models
{
    using MVVMtpl.Base;
    using Newtonsoft.Json;
    using SQLite;

    [SQLite.Table("OrderLine")]
    public class OrderLine : ObservableObject
    {
        private string id;
        private Product product;

        private string orderId;
        [JsonProperty("id")]
        public string Id
        {
            get { return this.id; }
            set { this.Set(ref this.id, value); }
        }

        [JsonIgnore]
        public string OrderId
        {
            get { return this.orderId; }
            set { this.Set(ref this.orderId, value); }
        }

        [Ignore]
        [JsonProperty("product")]
        public Product Product
        {
            get { return this.product; }
            set { this.Set(ref this.product, value); }
        }
    }
}
