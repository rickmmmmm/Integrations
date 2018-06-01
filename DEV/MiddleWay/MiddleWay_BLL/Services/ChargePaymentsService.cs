﻿using MiddleWay_DTO.ServiceInterfaces;
using MiddleWay_DTO.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleWay_DTO.Models;
using MiddleWay_DTO.TIPWeb_Models;

namespace FileSystemTasks
{
    public class ChargePaymentsService : IChargePaymentsService
    {
        #region Private Variables and Properties

        private IChargePaymentsRepository _chargePaymentsRepository;

        #endregion Private Variables and Properties

        #region Constructor

        public ChargePaymentsService(IChargePaymentsRepository chargePaymentsRepository)
        {
            _chargePaymentsRepository = chargePaymentsRepository;
        }

        #endregion Constructor

        #region Functions

        public List<ChargePaymentsModel> mapPaymentDetails(List<PaymentImportFile> imports)
        {
            List<ChargePaymentsModel> payload = new List<ChargePaymentsModel>();

            foreach (var import in imports)
            {

                ChargePaymentsModel payment = new ChargePaymentsModel
                {
                    ParentCharge = new Charge { ChargeUID = import.FineId },
                    ChargeAmount = Convert.ToDecimal(import.Amount),
                    PaymentDate = import.Date,
                    Void = import.Type.ToLower() == "adjustment" ? true : false
                };

                payload.Add(payment);

            }

            return payload;
        }

        public List<PaymentImportFile> serializeChargePaymentsFile(string fileName, string delimiter)
        {
            using (StreamReader reader = File.OpenText(fileName))
            {
                var payload = new List<PaymentImportFile>();
                var csv = new CsvReader(reader);

                csv.Configuration.Delimiter = delimiter; // ConfigurationManager.AppSettings["delimiter"];
                csv.Configuration.HasHeaderRecord = false;
                //csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
                csv.Configuration.IgnoreQuotes = true;

                while (csv.Read())
                {

                    PaymentImportFile newLine = new PaymentImportFile
                    {
                        FineId = csv.GetField<int>(Convert.ToInt32(ConfigurationManager.AppSettings["FineId"])),
                        Amount = csv.GetField<string>(Convert.ToInt32(ConfigurationManager.AppSettings["PaymentAmount"])),
                        Type = csv.GetField<string>(Convert.ToInt32(ConfigurationManager.AppSettings["PaymentType"])),
                        Date = csv.GetField<DateTime>(Convert.ToInt32(ConfigurationManager.AppSettings["PaymentDate"]))
                    };

                    payload.Add(newLine);
                }

                return payload;
            }
        }

        #endregion Functions

        #region Get Functions

        #endregion Get Functions

        #region Add Functions

        #endregion Add Functions

        #region Change Functions

        #endregion Change Functions

        #region Remove Functions

        #endregion Remove Functions
    }
}
