using System;
using System.Collections.Generic;
using System.Text;
using MiddleWay_DTO.TIPWeb_Models;
using MiddleWay_DTO.View_Models;

namespace MiddleWay_DTO.RepositoryInterfaces
{
    public interface IChargePaymentsRepository
    {
        ChargesViewModel GetChargeAmountByChargeID(int chargeID);

        List<ChargePaymentsViewModel> GetPaymentsByChargeID(int chargeID);

        void insertPaymentDetails(List<ChargePaymentsModel> payments);

        void insertPaymentDetail(ChargePaymentsModel payment);
    }
}
