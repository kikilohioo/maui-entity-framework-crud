using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComShopApp.Services;

public interface IDBPathService
{
    string GetPath(string filePath);
}
