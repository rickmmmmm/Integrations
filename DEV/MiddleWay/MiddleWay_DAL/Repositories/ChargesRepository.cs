using MiddleWay_DAL.DataProvider;
using MiddleWay_DAL.EF_DAL;
using MiddleWay_DTO.RepositoryInterfaces;
using MiddleWay_DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MiddleWay_DTO.TIPWeb_Models;
using MiddleWay_DTO.Enumerations;

namespace MiddleWay_DAL.Repositories
{
    public class ChargesRepository : IChargesRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ChargesRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public List<ChargeExportFile> ExportChargesToInTouch()
        {
            try
            {
                var chargeExportList = (from chargeExport in
                                            (from charges in _context.TblUnvCharges
                                             join chargePayments in _context.TblUnvChargePayments
                                                 on charges.ChargeUid equals chargePayments.ChargeUid
                                             join chargeTypes in _context.TblUnvChargeTypes
                                                 on charges.ChargeTypeUid equals chargeTypes.ChargeTypeUid
                                             join students in _context.TblStudents
                                                 on new
                                                 {
                                                     EntityUID = charges.EntityUid,
                                                     EntityTypeUID = charges.EntityTypeUid
                                                 } equals new
                                                 {
                                                     EntityUID = students.StudentsUid,
                                                     EntityTypeUID = (int)EntityTypeEnum.Student
                                                 }
                                             join campuses in _context.TblCampuses
                                                 on students.CampusId equals campuses.CampusId
                                             join items in _context.TblTechItems
                                                 on charges.ItemUid equals items.ItemUid
                                             //join inventory in _context.TblTechInventory
                                             //    on items.ItemUid equals inventory.ItemUid
                                             select new
                                             {
                                                 ChargeUid = charges.ChargeUid,
                                                 StudentId = students.StudentId,
                                                 ItemName = items.ItemName,
                                                 ItemBarCode = "",
                                                 ItemCollection = chargeTypes.Name,
                                                 FineLocationCode = campuses.CampusName,
                                                 Notes = charges.Notes,
                                                 CreatedDate = charges.CreatedDate,
                                                 ChargeAmount = charges.ChargeAmount,
                                                 PaymentAmount = chargePayments.ChargeAmount
                                             })
                                        group chargeExport by new
                                        {
                                            chargeExport.ChargeUid,
                                            chargeExport.StudentId,
                                            chargeExport.ItemName,
                                            chargeExport.ItemBarCode,
                                            chargeExport.ItemCollection,
                                            chargeExport.FineLocationCode,
                                            chargeExport.Notes,
                                            chargeExport.CreatedDate,
                                            chargeExport.ChargeAmount
                                        } into chargeExportGroup
                                        where (chargeExportGroup.Key.ChargeAmount - chargeExportGroup.Sum(x => x.PaymentAmount)) > 0
                                        select new ChargeExportFile
                                        {
                                            FineId = chargeExportGroup.Key.ChargeUid,
                                            StudentId = chargeExportGroup.Key.StudentId,
                                            ItemTitle = chargeExportGroup.Key.ItemName,
                                            ItemBarcode = chargeExportGroup.Key.ItemBarCode,
                                            ItemCollection = chargeExportGroup.Key.ItemCollection,
                                            FineLocationCode = chargeExportGroup.Key.FineLocationCode,
                                            FineDescription = chargeExportGroup.Key.Notes,
                                            FineCreatedDate = chargeExportGroup.Key.CreatedDate,
                                            FineAmount = chargeExportGroup.Key.ChargeAmount - chargeExportGroup.Sum(x => x.PaymentAmount)
                                        }).ToList();

                return chargeExportList;
            }
            catch
            {
                throw;
            }
            //List<ChargeExportFile> charges = new List<ChargeExportFile>();

            //string returnQuery = "SELECT chg.ChargeUID, sd.StudentID, items.ItemName, '' as ItemBarCode, chgt.Name  as ItemCollection, camp.CampusName as FineLocationCode, chg.[Notes], chg.CreatedDate, chg.ChargeAmount - ISNULL((SELECT SUM(ISNULL(pmt.ChargeAmount,0)) FROM tblUnvChargePayments pmt WHERE pmt.ChargeUID = chg.ChargeUID),0) as ChargeAmount ";
            //returnQuery += "FROM tblUnvCharges chg ";
            //returnQuery += "JOIN tblUnvChargeTypes chgt ON chgt.ChargeTypeUID = chg.[ChargeTypeUID] ";
            //returnQuery += "JOIN tblStudents sd on chg.EntityUID = sd.StudentsUID and chg.entitytypeuid = 4 ";
            //returnQuery += "JOIN tblCampuses camp on camp.CampusID = sd.CampusID ";
            //returnQuery += "JOIN tblTechItems items on items.ItemUID = chg.ItemUID ";
            ////returnQuery += "JOIN tblTechInventory inv on inv.ItemUID = items.ItemUID ";
            //returnQuery += "WHERE chg.ChargeAmount - ISNULL((SELECT SUM(ISNULL(pmt.ChargeAmount,0)) FROM tblUnvChargePayments pmt WHERE pmt.ChargeUID = chg.ChargeUID),0) > 0 ";

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    try
            //    {
            //        //ChargeExportFile cef = new ChargeExportFile
            //        //{
            //        //    FineId = (int) reader[0],
            //        //    StudentId = (int) reader[1],
            //        //    ItemTitle = (string) reader[2],
            //        //    ItemBarcode = (string) reader[3],
            //        //    ItemCollection = (string) reader[4],
            //        //    FineLocationCode = (string) reader[5],
            //        //    FineDescription = (string) reader[6],
            //        //    FineCreatedDate = Convert.ToDateTime((string) reader[7]),
            //        //    FineAmount = Convert.ToDecimal((string) reader[7])
            //        //};
            //        ChargeExportFile cef = new ChargeExportFile();

            //        cef.FineId = (int)reader[0];
            //        cef.StudentId = (string)reader[1];
            //        cef.ItemTitle = (string)reader[2];
            //        cef.ItemBarcode = (string)reader[3];
            //        cef.ItemCollection = (string)reader[4];
            //        cef.FineLocationCode = (string)reader[5];
            //        cef.FineDescription = (string)reader[6];
            //        cef.FineCreatedDate = (DateTime)reader[7];
            //        cef.FineAmount = (decimal)reader[8];

            //        charges.Add(cef);

            //    }

            //    catch (Exception e)
            //    {
            //        DbErrorEventArgs args = new DbErrorEventArgs();
            //        args.InterfaceMessage = "ERROR getting charge export data for fine " + (string)reader[0];
            //        args.ExceptionMessage = e.Message;
            //        continue;

            //    }
            //}

            //reader.Close();
            //_conn.Close();

            //return charges;
        }

        public bool chargeExists(int chargeId)
        {
            try
            {
                var chargeExists = (from charges in _context.TblUnvCharges
                                    where charges.ChargeUid == chargeId
                                    select true).FirstOrDefault();

                return chargeExists;
            }
            catch
            {
                throw;
            }
            //int outputValue = 0;

            //string query = "SELECT count(ChargeUID) FROM tblUnvCharges WHERE ChargeUID = " + chargeId.ToString();

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(query, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    outputValue = (int)reader[0];
            //}

            //if (outputValue == 1)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        #endregion Select Functions

        #region Insert Functions

        #endregion Insert Functions

        #region Update Functions

        public void voidCharges(List<ChargePaymentsModel> voidedCharges)
        {
            try
            {
                var chargeUids = (from voided in voidedCharges
                                  select voided.ChargeUID);

                var chargesToVoid = (from charges in _context.TblUnvCharges
                                     where chargeUids.Contains(charges.ChargeUid)
                                     select charges);

                foreach (var charge in chargesToVoid)
                {
                    charge.Void = true;
                }

                _context.TblUnvCharges.UpdateRange(chargesToVoid);

                _context.SaveChanges();
            }
            catch // (Exception e)
            {
                //DbErrorEventArgs args = new DbErrorEventArgs();
                //args.InterfaceMessage = "ERROR updating data for fine " + charge.ChargeUID.ToString();
                //args.ExceptionMessage = e.Message;
                //continue;
                throw;
            }

            // string query = "UPDATE tblUnvCharges ";
            //query += "SET Void = 1 ";
            //query += "WHERE ChargeUID = {0}";

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}
            //_conn.Open();

            //foreach (var charge in voidedCharges)
            //{
            //SqlCommand cmd = new SqlCommand(string.Format(query, charge.ParentCharge.ChargeUID.ToString()), _conn);
            //cmd.ExecuteNonQuery();

            //}
            //_conn.Close();
        }

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions

    }
}
