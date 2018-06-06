using MiddleWay_DTO.Models;

namespace MiddleWay_DTO.RepositoryInterfaces
{
    public interface INotificationsService
    {
        void send(EmailMessageModel message);
        void sendAsync(EmailMessageModel message);
    }
}
