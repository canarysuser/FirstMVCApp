namespace FirstMVCApp.Models
{
    public class CustomersViewModel
    {
        public List<string> FilterCriteria { get; set; } = new List<string>
        {
            "Country", "City", "Text"
        };
        public string SelectedFilter { get; set; } = "Text"; 

        public string SearchTerm { get; set; } = string.Empty;

        public List<Customer> Customers { get; set; } = new List<Customer>();
    }

}
