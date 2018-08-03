using TIPWeb_Controller.DataProvider;
using TIPWeb_Controller.EF_DAL;
using MiddleWay_DTO.Models.TIPWeb_Controller;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.RepositoryInterfaces.TIPWeb;
using MiddleWay_DTO.View_Models;

namespace TIPWeb_Controller.Repositories
{
    public class ChargePaymentsRepository : IChargePaymentsRepository
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

        public ChargesViewModel GetChargeAmountByChargeId(int chargeId)
        {
            try
            {
                var charge = (from chargePayments in
                                (from charges in _context.TblUnvCharges
                                 join chargePayments in _context.TblUnvChargePayments
                                    on charges.ChargeUid equals chargePayments.ChargeUid into paymentsGroup
                                 from chargePayments in paymentsGroup.DefaultIfEmpty()
                                 where charges.ChargeUid == chargeId
                                 select new
                                 {
                                     ChargeId = chargeId,
                                     ChargeAmount = charges.ChargeAmount,
                                     ChargePaymentUid = chargePayments.ChargePaymentUid,
                                     PaymentAmount = chargePayments.ChargeAmount,
                                     PaymentDate = chargePayments.CreatedDate
                                 })
                                  //group charges by new { ChargeUid = charges.ChargeUid, ChargeAmount = charges.ChargeAmount, PaymentAmount = chargePayments.ChargeAmount } into chargesGroup
                              group chargePayments by chargePayments.ChargeId into chargePaymentsGroup
                              select new ChargesViewModel
                              {
                                  ChargeUid = chargePaymentsGroup.Key,
                                  ChargeAmount = chargePaymentsGroup.Max(x => x.ChargeAmount),
                                  Payments = (from payments in chargePaymentsGroup
                                              select new ChargePaymentsViewModel
                                              {
                                                  ChargePaymentUid = payments.ChargePaymentUid,
                                                  ChargeUid = chargeId,
                                                  ChargeAmount = payments.ChargeAmount,
                                                  PaymentDate = payments.PaymentDate
                                              }).ToList()
                              }).FirstOrDefault();

                return charge;
            }
            catch
            {
                throw;
            }
            //ChargesModel returnCharge = new ChargesModel();

            //string query = "SELECT chg.ChargeAmount, ISNULL((SELECT SUM(ISNULL(pmt.ChargeAmount,0)) FROM tblUnvChargePayments pmt WHERE pmt.ChargeUid = chg.ChargeUid),0) as PaidAmount FROM tblUnvCharges chg WHERE ChargeUid = " + chargeId.ToString();

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(query, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    returnCharge.ChargeUid = chargeId;
            //    returnCharge.ChargeAmount = (decimal)reader[0];
            //    returnCharge.Payments = getPaymentsByChargeId(chargeId);
            //}

            //return returnCharge;
        }

        public List<ChargePaymentsViewModel> GetPaymentsByChargeId(int chargeId)
        {
            try
            {
                var chargePaymentsList = (from chargePayments in _context.TblUnvChargePayments
                                          where chargePayments.ChargeUid == chargeId
                                          select new ChargePaymentsViewModel
                                          {
                                              ChargePaymentUid = chargePayments.ChargePaymentUid,
                                              ChargeUid = chargeId,
                                              ChargeAmount = chargePayments.ChargeAmount,
                                              Void = chargePayments.Void,
                                              PaymentDate = chargePayments.CreatedDate
                                          }).ToList();

                return chargePaymentsList;
            }
            catch
            {
                throw;
            }
            //var payments = new List<ChargePaymentsModel>();

            //string query = " WHERE ChargeUid = " + chargeId.ToString();

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(query, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    var payment = new ChargePaymentsModel();

            //    payment.ParentCharge.ChargeUid = chargeId;
            //    payment.ChargeAmount = (decimal)reader[1];
            //    payment.PaymentDate = (DateTime)reader[2];
            //}

            //return payments;
        }

        #endregion Select Functions

        #region Insert Functions

        public void insertPaymentDetails(List<ChargePaymentsModel> payments)
        {
            foreach (var payment in payments)
            {
                insertPaymentDetail(payment);
            }
        }

        public void insertPaymentDetail(ChargePaymentsModel payment)
        {
            try
            {
                var paymentDetail = new TblUnvChargePayments
                {
                    //ChargePaymentUid = payment.,
                    ApplicationUid = (int)ApplicationCode.TIPWebIT,
                    ChargeUid = payment.ChargeUid,
                    //PaymentSiteUid = payment.,
                    ChargeAmount = payment.ChargeAmount,
                    //Void = payment.Void,
                    //Description = payment.,
                    //Notes = payment.,
                    CreatedByUserId = 0,
                    CreatedDate = DateTime.Now,
                    LastModifiedByUserId = 0,
                    LastModifiedDate = DateTime.Now
                };

                _context.TblUnvChargePayments.Add(paymentDetail);

                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
            //string query = "INSERT INTO tblUnvChargePayments (ApplicationUid, ChargeUid, ChargeAmount, CreatedDate, CreatedByUserID, LastModifiedDate, LastModifiedByUserID) ";
            //query += "VALUES ({0}, {1}, {2}, '{3}', {4}, '{5}', {6})";

            //SqlCommand cmd = new SqlCommand(string.Format(query, 2, import.ParentCharge.ChargeUid, import.ChargeAmount, DateTime.Now.ToString(), 0, DateTime.Now.ToString(), 0), _conn);

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
