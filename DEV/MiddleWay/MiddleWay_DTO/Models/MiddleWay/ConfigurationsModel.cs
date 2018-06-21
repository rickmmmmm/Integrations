using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class ConfigurationsModel
    {
        public int ConfigurationUid { get; set; }
        public int ProcessUid { get; set; }
        public string ConfigurationName { get; set; }
        public string ConfigurationValue { get; set; }
        public bool Enabled { get; set; }
    }
}
