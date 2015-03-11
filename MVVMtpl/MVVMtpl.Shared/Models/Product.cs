namespace MVVMtpl.Models
{
    using MVVMtpl.Base;
    using Newtonsoft.Json;
    using Windows.UI.Xaml.Media.Imaging;
    using SQLite;

    [Table("Product")]
    public class Product : ObservableObject
    {
        private string id;
        private string name;
        private string description;
        private string image;
        private float price;
        private string orderId;

        [PrimaryKey]
        [JsonProperty("id")]
        public string Id
        {
            get { return this.id; }
            set { this.Set(ref this.id, value); }
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return this.name; }
            set { this.Set(ref this.name, value); }
        }

        [JsonProperty("description")]
        public string Description
        {
            get { return this.description; }
            set { this.Set(ref this.description, value); }
        }

        [JsonProperty("image")]
        public string Image
        {
            get { return this.image; }
            set { this.Set(ref this.image, value); }
        }

        [Ignore]
        public BitmapImage Picture { get; set; }
        [JsonProperty("price")]
        public float Price
        {
            get { return this.price; }
            set { this.Set(ref this.price, value); }
        }

        [JsonIgnore]
        public string OrderId
        {
            get { return this.orderId; }
            set { this.Set(ref this.orderId, value); }
        }
    }
}
