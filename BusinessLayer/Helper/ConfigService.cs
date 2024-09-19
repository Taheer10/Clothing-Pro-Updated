using ClothingPro.BusinessLayer.BusinessService;
using ClothingPro.BusinessLayer.Interface;

namespace ClothingPro.BusinessLayer.Helper;
public class ConfigService
{
    public static void ConfigureIService(IServiceCollection services)
    {
        //services.AddTransient<IStaffService, StaffService>();
        //services.AddTransient<IRoomTypeService, RoomTypeService>();
        services.AddSingleton<IStockService,StockService>();
        services.AddSingleton<IMenuHeaderService,MenuHeaderService>();
        services.AddSingleton<ICompanyService,CompanyService>();


    }
}
