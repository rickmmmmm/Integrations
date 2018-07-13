using TIPWeb_Controller.DataProvider;
using TIPWeb_Controller.EF_DAL;
using MiddleWay_DTO.Models.TIPWeb;
using System.Linq;

namespace TIPWeb_Controller.Repositories
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

        public int GetAreaUidFromName(string areaName)
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
                        //join createdBy in _context.TblUser
                        //    on areas.CreatedByUserId equals createdBy.UserId
                        //join modifiedBy in _context.TblUser
                        //    on areas.LastModifiedByUserId equals modifiedBy.UserId
                        where areas.AreaName.Trim().ToLower() == area
                        select new AreasModel
                        {
                            AreaUid = areas.AreaUid,
                            AreaName = areas.AreaName,
                            CreatedByUserId = areas.CreatedByUserId, // createdBy.RealName,
                            CreatedDate = areas.CreatedDate,
                            LastModifiedByUserId = areas.LastModifiedByUserId, // modifiedBy.RealName,
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
