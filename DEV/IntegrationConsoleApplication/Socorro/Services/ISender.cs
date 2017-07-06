using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISender
    {
        void send(IMessage message);
        void sendAsync(IMessage message);
    }
}
