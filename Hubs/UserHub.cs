using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs;

public class UserHub : Hub
{
    public static int TotalViews { set; get; } = 0;
    public static int TotalUsers { set; get; } = 0;

    public override Task OnConnectedAsync()
    {
        TotalUsers++;
        Clients.All.SendAsync("UpdateTotalUsers", TotalUsers).GetAwaiter();
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        TotalUsers--;
        Clients.All.SendAsync("UpdateTotalUsers", TotalUsers).GetAwaiter();
        return base.OnDisconnectedAsync(exception);
    }

    public async Task NewWindowLoaded()
    {
        TotalViews++;
        await Clients.All.SendAsync("updateTotalViews", TotalViews);
    }
    
    
}