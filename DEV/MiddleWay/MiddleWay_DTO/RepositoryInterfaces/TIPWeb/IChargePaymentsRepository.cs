using System;
using System.Collections.Generic;
using System.Text;
using MiddleWay_DTO.Models.TIPWeb;
using MiddleWay_DTO.View_Models;

namespace MiddleWay_DTO.RepositoryInterfaces.TIPWeb
{
    public interface IChargePaymentsRepository
    {
        ChargesViewModel GetChargeAmountByChargeId(int chargeId);

        List<ChargePaymentsViewModel> GetPaymentsByChargeId(int chargeId);

        void insertPaymentDetails(List<ChargePaymentsModel> payments);

        void insertPaymentDetail(ChargePaymentsModel payment);
    }
}
