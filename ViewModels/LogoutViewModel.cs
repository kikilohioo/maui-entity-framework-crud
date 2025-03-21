using CommunityToolkit.Mvvm.Input;
using ComShopApp.Services;
using ComShopApp.Utils;
using ComShopApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComShopApp.ViewModels;

public partial class LogoutViewModel : ViewModelGlobal
{
    private SecurityService _securityService;

    public LogoutViewModel(SecurityService securityService)
    {
        _securityService = securityService;
    }
}
