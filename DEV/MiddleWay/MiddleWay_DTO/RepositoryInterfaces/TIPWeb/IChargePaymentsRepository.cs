using System;
using System.Collections.Generic;
using System.Text;
using MiddleWay_DTO.Models.TIPWeb;
using MiddleWay_DTO.View_Models;

namespace MiddleWay_DTO.RepositoryInterfaces.TIPWeb
{
    public interface IChargePaymentsRepository
    {
        ChargesViewModel GetChargeAmountByChargeID(int chargeID);

        List<ChargePaymentsViewModel> GetPaymentsByChargeID(int chargeID);

        void insertPaymentDetails(List<ChargePaymentsModel> payments);

        void insertPaymentDetail(ChargePaymentsModel payment);
    }
}
