using MiddleWay_DTO.ServiceInterfaces;
using MiddleWay_DTO.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleWay_DTO.Models;

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

        #endregion Functions
    }
}
