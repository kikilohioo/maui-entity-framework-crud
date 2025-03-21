using ComShopApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComShopApp.Services;

public class NavigationService : INavigationService
{
    public Task GoToAsync(string uri)
    {
        return Shell.Current.GoToAsync(uri);
    }

    public Task GoToAsync(string uri, IDictionary<string, object> parameters)
    {
        return Shell.Current.GoToAsync(uri, parameters);
    }
}
