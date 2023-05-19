using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Repositories
{
    public interface ICompositionSellingRepository
    {
        IEnumerable<CompositionSellingViewModel> GettAllById(Guid id);
    }
}
