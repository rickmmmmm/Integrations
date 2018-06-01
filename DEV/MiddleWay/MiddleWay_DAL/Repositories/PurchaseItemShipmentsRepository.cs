using MiddleWay_DAL.DataProvider;
using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
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

        public void addShipmentInfo()
        {
            string query = "INSERT INTO [dbo].[tblTechPurchaseItemShipments]";
            query += "([PurchaseItemDetailUID] ";
            query += ",[ShippedToSiteUID] ";
            query += ",[TicketNumber] ";
            query += ",[QuantityShipped] ";
            query += ",[TicketedByUserID] ";
            query += ",[TicketedDate] ";
            query += ",[StatusUID] ";
            query += ",[CreatedByUserID] ";
            query += ",[CreatedDate] ";
            query += ",[LastModifiedByUserID] ";
            query += ",[LastModifiedDate]) ";
            query += "SELECT ttpid.[PurchaseItemDetailUID], ttpid.[SiteAddedSiteUID], NULL, ttpid.QuantityOrdered, NULL, NULL, ttpid.StatusUID, 0, getdate(), 0, getdate() ";
            query += "FROM tblTechPurchaseItemShipments ttpis ";
            query += "RIGHT JOIN tblTechPurchaseItemDetails ttpid on ttpis.[PurchaseItemDetailUID] = ttpid.[PurchaseItemDetailUID] ";
            query += "WHERE ttpis.PurchaseItemDetailUID is null";

            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                DbErrorEventArgs args = new DbErrorEventArgs();
                args.InterfaceMessage = "ERROR adding shipment information.";
                args.ExceptionMessage = e.Message;
            }


        }

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
