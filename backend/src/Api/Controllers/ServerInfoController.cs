using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Api.Controllers;

/// <summary>
/// Controller for server information endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class ServerInfoController : ControllerBase
{
    /// <summary>
    /// Gets the server's IP address.
    /// </summary>
    /// <returns>The server IP address information.</returns>
    [HttpGet("ip")]
    public ActionResult<ServerIpDto> GetServerIp()
    {
        try
        {
            var hostName = Dns.GetHostName();
            var hostEntry = Dns.GetHostEntry(hostName);
            
            var ipAddress = hostEntry.AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            
            var ipString = ipAddress?.ToString() ?? "127.0.0.1";
            
            return new ServerIpDto(ipString);
        }
        catch (Exception)
        {
            return new ServerIpDto("127.0.0.1");
        }
    }
}
