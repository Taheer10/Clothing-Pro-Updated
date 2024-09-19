using ClothingPro.DataAccessLayer.DbAccess;
using ClothingPro.DataAccessLayer.Model;

namespace ClothingPro.BusinessLayer.Repository
{
    public class StockRepository : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;


        public StockRepository()
        {
            _unitOfWork = new UnitOfWork();
        }
        public List<Stock> GetStockList()
        {
            try
            {
                var result = _unitOfWork.StockRepository.GetAll().ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public Stock GetByStockId(int? stId)
        //{
        //    try
        //    {
        //        if (stId != null)
        //        {
        //            var result = _unitOfWork.StockRepository.FindBy(x => x.StId == stId);
        //            return result;
        //        }
        //        return null;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public Stock GetByStockId(int stId)
        {
            try
            {
                if (stId == null)
                {
                    throw new ArgumentNullException(nameof(stId), "Stock ID cannot be null");
                }

                var result = _unitOfWork.StockRepository.FindBy(x => x.StId == stId);
                if (result == null)
                {
                    throw new KeyNotFoundException($"No stock found with ID {stId}");
                }

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (if you have a logging framework)
                throw new Exception("An error occurred while retrieving the stock by ID", ex);
            }
        }


        public Stock GetByStockBarcodeId(int? barcodeId)
        {
            try
            {
                if (barcodeId != null)
                {
                    var result = _unitOfWork.StockRepository.FindBy(x => x.StCode == barcodeId.ToString());
                    return result;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CreateStock(Stock model)
        {
            try
            {

                var result = _unitOfWork.StockRepository.Add(model).StId;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditStock(Stock model)
        {
            try
            {
                _unitOfWork.StockRepository.Update(model);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddStockRange(IEnumerable<Stock> model)
        {
            try
            {
                _unitOfWork.StockRepository.AddRange(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EditStockRange(IEnumerable<Stock> model)
        {
            try
            {
                _unitOfWork.StockRepository.EditRange(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteStock(int stId)
        {
            try
            {
                var StockModel = _unitOfWork.StockRepository.FindBy(x => x.StId == stId);

                _unitOfWork.StockRepository.Delete(StockModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
