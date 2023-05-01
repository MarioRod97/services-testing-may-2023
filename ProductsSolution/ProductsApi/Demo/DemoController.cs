using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Demo;

public class DemoController : ControllerBase
{
    private readonly ISystemClock _clock;

    public DemoController(ISystemClock clock)
    {
        _clock = clock;
    }

    [HttpGet("/demo")]
    public async Task<ActionResult> GetTheDemo()
    {
        var currentTime = _clock.GetCurrent();
        var response = new DemoResponse
        {
            Message = "Hello from the other side!",
            CreatedAt = _clock.GetCurrent(),
            GettingCloseToQuittingTime = currentTime.Hour >= 16
        };
        
        return Ok(response);
    } // This class is garbage collected
}

public interface ISystemClock
{
    DateTimeOffset GetCurrent();
}

public class SystemClock : ISystemClock
{
    public DateTimeOffset GetCurrent()
    {
        return DateTimeOffset.Now;
    }
}
