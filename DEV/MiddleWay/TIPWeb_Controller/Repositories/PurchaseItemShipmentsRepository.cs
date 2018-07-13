using TIPWeb_Controller.DataProvider;
using TIPWeb_Controller.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace TIPWeb_Controller.Repositories
{
    public class PurchaseItemShipmentsRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public PurchaseItemShipmentsRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        #endregion Select Functions

        #region Insert Functions

        public void AddShipmentInfo()
        {
            string query = "INSERT INTO [dbo].[tblTechPurchaseItemShipments]";
            query += "([PurchaseItemDetailUid] ";
            query += ",[ShippedToSiteUid] ";
            query += ",[TicketNumber] ";
            query += ",[QuantityShipped] ";
            query += ",[TicketedByUserID] ";
            query += ",[TicketedDate] ";
            query += ",[StatusUid] ";
            query += ",[CreatedByUserID] ";
            query += ",[CreatedDate] ";
            query += ",[LastModifiedByUserID] ";
            query += ",[LastModifiedDate]) ";
            query += "SELECT ttpid.[PurchaseItemDetailUid], ttpid.[SiteAddedSiteUid], NULL, ttpid.QuantityOrdered, NULL, NULL, ttpid.StatusUid, 0, getdate(), 0, getdate() ";
            query += "FROM tblTechPurchaseItemShipments ttpis ";
            query += "RIGHT JOIN tblTechPurchaseItemDetails ttpid on ttpis.[PurchaseItemDetailUid] = ttpid.[PurchaseItemDetailUid] ";
            query += "WHERE ttpis.PurchaseItemDetailUid is null";

            //if (_conn.State == ConnectionState.Closed)
            //{
            //    _conn.Open();
            //}

            try
            {
                //SqlCommand cmd = new SqlCommand(query, _conn);
                //cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //DbErrorEventArgs args = new DbErrorEventArgs();
                //args.InterfaceMessage = "ERROR adding shipment information.";
                //args.ExceptionMessage = e.Message;
            }


        }

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
