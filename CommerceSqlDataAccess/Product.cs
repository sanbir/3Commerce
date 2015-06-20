
namespace Ploeh.Samples.Commerce.Data.Sql
{
    public partial class Product
    {
        public Domain.Product ToDomainProduct()
        {
            Domain.Product p = new Domain.Product {Name = this.Name, UnitPrice = this.UnitPrice};
            return p;
        }
    }
}
