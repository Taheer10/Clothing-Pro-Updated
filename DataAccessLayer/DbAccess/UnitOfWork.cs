using ClothingPro.DataAccessLayer.DbAccess;
using ClothingPro.DataAccessLayer.DataContext;
using ClothingPro.DataAccessLayer.Model;
using ClothingPro.BusinessLayer.DTO;



namespace ClothingPro.DataAccessLayer.DbAccess;

public class UnitOfWork : IDisposable
{
    public ClothingProContext _dbContext;

    public UnitOfWork()
    {
        _dbContext = new ClothingProContext();
    }

    //private IRepository<Department> _departmentRepository;
    private IRepository<Stock> _stockRepository;
    private IRepository<Setting> _settingRepository;
    private IRepository<MenuHeader> _menuHeaderRepository;
    private IRepository<Company> _companyRepository;
    private IRepository<Banner> _bannerRepository;
    private IRepository<ColorImages> _colorImagesRepository;






    #region Repository
    public IRepository<Stock> StockRepository
    {
        get
        {
            return _stockRepository = _stockRepository ?? new GenericRepository<Stock>(_dbContext);
        }
    }
    public IRepository<Setting> SettingRepository
    {
        get
        {
            return _settingRepository = _settingRepository ?? new GenericRepository<Setting>(_dbContext);
        }
    }

    public IRepository<MenuHeader> MenuHeaderRepository
    {
        get
        {
            return _menuHeaderRepository = _menuHeaderRepository ?? new GenericRepository<MenuHeader>(_dbContext);
        }
    }

    public IRepository<Company> CompanyRepository
    {
        get
        {
            return _companyRepository = _companyRepository ?? new GenericRepository<Company>(_dbContext);
        }
    }

    public IRepository<Banner> BannerRepository
    {
        get
        {
            return _bannerRepository = _bannerRepository ?? new GenericRepository<Banner>(_dbContext);
        }
    }

    public IRepository<ColorImages> ColorImagesRepository
    {
        get
        {
            return _colorImagesRepository = _colorImagesRepository ?? new GenericRepository<ColorImages>(_dbContext);
        }
    }


    #endregion



    #region Public member methods...
    /// <summary>
    /// Save method.
    /// </summary>
    /// 
    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public EntityDatabaseTransaction BeginTransaction()
    {
        return new EntityDatabaseTransaction(_dbContext);
    }

    #endregion

    #region Implementing IDiosposable...

    #region private dispose variable declaration...
    private bool disposed = false;
    #endregion

    /// <summary>
    /// Protected Virtual Dispose method
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this.disposed = true;
    }

    /// <summary>
    /// Dispose method
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

}
