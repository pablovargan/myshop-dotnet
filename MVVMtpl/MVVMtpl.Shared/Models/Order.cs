namespace MVVMtpl.Models
{
    using MVVMtpl.Base;
    using Newtonsoft.Json;
    using SQLite;
    using System.Collections.Generic;

    [Table("Order")]
    public class Order : ObservableObject
    {
        private string id;
        private List<OrderLine> orderlines;

        [PrimaryKey]
        [JsonProperty("id")]
        public string Id
        {
            get { return this.id; }
            set { this.Set(ref this.id, value); }
        }

        [Ignore]
        [JsonProperty("orderlines")]
        public List<OrderLine> OrderLines
        {
            get { return this.orderlines; }
            set { this.Set(ref this.orderlines, value); }
        }
    }
}
