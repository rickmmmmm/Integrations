using MiddleWay_DTO.Models.MiddleWay_BLL;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL
{
    public interface INotificationsService
    {
        void Send(MessageModel message);
        void SendAsync(MessageModel message);
    }
}
