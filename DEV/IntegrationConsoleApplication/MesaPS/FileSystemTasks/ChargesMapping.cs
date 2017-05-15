using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTasks;

namespace FileSystemTasks
{
    public class ChargesMapping
    {
        private IRepository _repo;
        public ChargesMapping(IRepository repo)
        {
            _repo = repo;
        }

        public List<ChargePayments> mapPaymentDetails(List<PaymentImportFile> imports)
        {
            List<ChargePayments> payload = new List<ChargePayments>();

            foreach (var import in imports)
            {

                    ChargePayments payment = new ChargePayments
                    {
                        ParentCharge = new Charge { ChargeUID = import.FineId },
                        ChargeAmount = import.Amount.MesaConvertChargeAmount(2),
                        PaymentDate = import.Date,
                        Void = import.Type.ToLower() == "adjustment" ? true : false
                    };

                    payload.Add(payment);
                
            }

            return payload;
        }
    }
}
