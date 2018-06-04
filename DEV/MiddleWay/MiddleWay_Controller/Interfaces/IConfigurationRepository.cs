using MiddleWay_Controller.IntegrationDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Interfaces
{
    public interface IConfigurationRepository
    {
        List<Configurations> GetConfiguration();
    }
}
