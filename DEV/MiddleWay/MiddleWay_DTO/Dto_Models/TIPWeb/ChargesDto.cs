using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.DTO_Models.TIPWeb
{
    public class ChargesDto
    {
        public int ChargeUid { get; set; }
        public decimal? ChargeAmount { get; set; }
        public ICollection<ChargePaymentsDto> Payments { get; set; }
    }

}
