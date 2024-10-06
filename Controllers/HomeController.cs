using ClothingPro.BusinessLayer.BusinessService;
using ClothingPro.BusinessLayer.DTO;
using ClothingPro.BusinessLayer.Helper;
using ClothingPro.BusinessLayer.Interface;
using ClothingPro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClothingPro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStockService _stockService;
        private readonly IMenuHeaderService _menuHeaderService;
        private readonly IBannerService _bannerService;
        private readonly IColorImagesService _colorImagesService;

        public HomeController(ILogger<HomeController> logger, IStockService stockService, IMenuHeaderService menuHeaderService, IBannerService bannerService, IColorImagesService colorImagesService)
        {
            _logger = logger;
            _stockService = stockService;
            _menuHeaderService = menuHeaderService;
            _bannerService = bannerService;
            _colorImagesService = colorImagesService;
        }

        public IActionResult Index(string FromDate="",string Todate="")
        {
            var menuheaderList = _menuHeaderService.GetAllMenuHeader();
            var stocklist = _stockService.GetAllKindOfStock();
            if (FromDate == "" || Todate == "")
            {
                FromDate = DateTime.Now.ToString();
                Todate = DateTime.Now.ToString();
            }
            var latest = _stockService.GetLatestDetail(FromDate, Todate);
            stocklist.stockLatestList = latest;
            stocklist.MenuHeaderLists = menuheaderList;
            stocklist.BannerList = _bannerService.GetAllBanner();
            return View(stocklist);
        }

        public IActionResult StockLatetsItems(string FromDate = "", string Todate = "")
        {
            StockDTO stockDTO = new StockDTO();
            if (FromDate == "" || FromDate == "undefined" || FromDate == null  || Todate == "" || Todate ==null || Todate == "undefined")
            {
                //FromDate = DBNullHandler.FormatDateTime(DateTime.Now.ToString());
                FromDate = DBNullHandler.FormatDateTime(DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd"));
                Todate = DBNullHandler.FormatDateTime(DateTime.Now.AddDays(10).ToString());
            }
            var latestItems = _stockService.GetLatestDetail(FromDate, Todate);
            //ViewBag.FromDate = FromDate;
            //ViewBag.ToDate = Todate;
            ViewBag.FromDate = DateTime.Parse(FromDate).ToString("yyyy-MM-dd");
            ViewBag.ToDate = DateTime.Parse(Todate).ToString("yyyy-MM-dd");

            stockDTO.stockLatestList = latestItems;
            return View("StockLatest", stockDTO);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
