using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.RecentViewProductCarousel.Components
{
    [ViewComponent(Name = "RecentViewProductCarousel")]
    public class ExampleWidgetViewComponent : NopViewComponent
    {
        #region Fields

        private readonly IProductModelFactory _productModelFactory;
        private readonly IProductService _productService;
        private readonly IRecentlyViewedProductsService _recentlyViewedProductsService;
        private readonly CatalogSettings _catalogSettings;

        #endregion

        #region Ctor

        public ExampleWidgetViewComponent(IProductModelFactory productModelFactory, IProductService productService, 
            IRecentlyViewedProductsService recentlyViewedProductsService, CatalogSettings catalogSettings)
        {
            _productModelFactory = productModelFactory;
            _productService = productService;
            _recentlyViewedProductsService = recentlyViewedProductsService;
            _catalogSettings = catalogSettings;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var products = await _recentlyViewedProductsService.GetRecentlyViewedProductsAsync(_catalogSettings.RecentlyViewedProductsNumber);

            var model = new List<Web.Models.Catalog.ProductOverviewModel>();
            model.AddRange(await _productModelFactory.PrepareProductOverviewModelsAsync(products));

            return View("~/Plugins/Widgets.RecentViewProductCarousel/Views/PublicInfo.cshtml", model);
        }
    }
}
