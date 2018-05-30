using MiddleWay_DTO.Models;

namespace MiddleWay_DTO.RepositoryInterfaces
{
    public interface ISender
    {
        void send(MessageModel message);
        void sendAsync(MessageModel message);
    }
}
