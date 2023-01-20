using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using SqlKata;
using SqlKata.Execution;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product,int>, IProductRepositoryAsync
    {


        public async Task<bool> IsUniqueBarcodeAsync(string barcode)
        {
            var result =  (await _unitOfWork.Query(_table).SelectRaw("COUNT(Barcode)").Where("Barcode", barcode).GetAsync<int>()).ToArray()[0];
            return result > 0;
        }

        public ProductRepositoryAsync(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : base(unitOfWork, authenticatedUser,"Products")
        {
        }
    }
}
