using MiddleWay_DAL.DataProvider;
using MiddleWay_DAL.EF_DAL;
using MiddleWay_DTO.TIPWeb_Models;
using System.Linq;

namespace MiddleWay_DAL.Repositories
{
    public class AreasRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public AreasRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public int getAreaUIDFromName(string areaName)
        {
            try
            {
                var area = areaName.Trim().ToLower();
                return (from areas in _context.TblUnvAreas
                        where areas.AreaName.Trim().ToLower() == area
                        select areas.AreaUid).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public AreasModel getArea(string areaName)
        {
            try
            {
                var area = areaName.Trim().ToLower();
                return (from areas in _context.TblUnvAreas
                        join createdBy in _context.TblUser
                            on areas.CreatedByUserId equals createdBy.UserId
                        join modifiedBy in _context.TblUser
                            on areas.LastModifiedByUserId equals modifiedBy.UserId
                        where areas.AreaName.Trim().ToLower() == area
                        select new AreasModel
                        {
                            AreaID = areas.AreaUid,
                            AreaName = areas.AreaName,
                            CreatedByUser = createdBy.RealName,
                            CreatedDate = areas.CreatedDate,
                            LastModifiedByUser = modifiedBy.RealName,
                            LastModifiedDate = areas.LastModifiedDate
                        }).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        #endregion Select Functions

        #region Insert Functions

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
