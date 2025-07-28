namespace FirstMVCApp.Infrastructure
{
    public class DependencyClass
    {

        private string _id = Guid.NewGuid().ToString(); 
        public string GetId()
        {
            return _id;
        }
    }
}
