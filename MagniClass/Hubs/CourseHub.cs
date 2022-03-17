using Microsoft.AspNetCore.SignalR;

namespace MagniClass.Hubs
{
    public class CourseHub : Hub
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
