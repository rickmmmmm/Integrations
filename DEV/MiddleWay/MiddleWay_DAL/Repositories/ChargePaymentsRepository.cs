using MiddleWay_DAL.DataProvider;
using MiddleWay_DAL.EF_DAL;
using MiddleWay_DTO.TIPWeb_Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class ChargePaymentsRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ChargePaymentsRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public ChargesModel getChargeAmountByChargeId(int chargeId)
        {
            ChargesModel returnCharge = new ChargesModel();

            string query = "SELECT chg.ChargeAmount, ISNULL((SELECT SUM(ISNULL(pmt.ChargeAmount,0)) FROM tblUnvChargePayments pmt WHERE pmt.ChargeUID = chg.ChargeUID),0) as PaidAmount FROM tblUnvCharges chg WHERE ChargeUID = " + chargeId.ToString();

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(query, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                returnCharge.ChargeUID = chargeId;
                returnCharge.ChargeAmount = (decimal)reader[0];
                returnCharge.Payments = getPaymentsByChargeId(chargeId);
            }

            return returnCharge;
        }

        private List<ChargePaymentsModel> getPaymentsByChargeId(int chargeId)
        {
            var payments = new List<ChargePaymentsModel>();

            string query = " WHERE ChargeUID = " + chargeId.ToString();

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(query, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                var payment = new ChargePaymentsModel();

                payment.ParentCharge.ChargeUID = chargeId;
                payment.ChargeAmount = (decimal)reader[1];
                payment.PaymentDate = (DateTime)reader[2];
            }

            return payments;
        }

        #endregion Select Functions

        #region Insert Functions

        public void insertPaymentDetails(List<ChargePaymentsModel> imports)
        {
            foreach (var import in imports)
            {
                insertPaymentDetail(import);
            }
        }

        public void insertPaymentDetail(ChargePaymentsModel import)
        {

            string query = "INSERT INTO tblUnvChargePayments (ApplicationUID, ChargeUID, ChargeAmount, CreatedDate, CreatedByUserID, LastModifiedDate, LastModifiedByUserID) ";
            query += "VALUES ({0}, {1}, {2}, '{3}', {4}, '{5}', {6})";

            //SqlCommand cmd = new SqlCommand(string.Format(query, 2, import.ParentCharge.ChargeUID, import.ChargeAmount, DateTime.Now.ToString(), 0, DateTime.Now.ToString(), 0), _conn);

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //cmd.ExecuteNonQuery();
            //_conn.Close();
        }

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions

    }
}
