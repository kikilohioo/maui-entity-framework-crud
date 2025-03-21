using ComShopApp.DataAccess;
using ComShopApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComShopApp.Handler
{
    public class SearchProductHandler : SearchHandler
    {
        ShopDBContext dBContext;

        public SearchProductHandler()
        {
            this.dBContext = new ShopDBContext();
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
                return;
            }

            var results = dBContext.Products.Where(p => p.Name.ToLowerInvariant().Contains(newValue.ToLowerInvariant())).ToList();

            ItemsSource = results;

            base.OnQueryChanged(oldValue, newValue);
        }

        protected override async void OnItemSelected(object item)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?id={((Product)item).Id}");
        }
    }
}
