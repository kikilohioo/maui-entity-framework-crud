using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComShopApp.Interfaces;

public interface INavigationService
{
    Task GoToAsync(string uri);
    Task GoToAsync(string uri, IDictionary<string, object> parameters);
}
