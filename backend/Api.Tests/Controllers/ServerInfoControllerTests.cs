using Api.Controllers;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Tests.Controllers;

public sealed class ServerInfoControllerTests
{
    [Fact]
    public void GetServerIp_ReturnsServerIpDto()
    {
        var controller = new ServerInfoController();

        var result = controller.GetServerIp();

        Assert.NotNull(result);
        Assert.IsType<ActionResult<ServerIpDto>>(result);
    }

    [Fact]
    public void GetServerIp_ReturnsValidIpAddress()
    {
        var controller = new ServerInfoController();

        var result = controller.GetServerIp();
        var okResult = result.Result as OkObjectResult;
        var serverIpDto = result.Value;

        Assert.NotNull(serverIpDto);
        Assert.NotNull(serverIpDto.Ip);
        Assert.NotEmpty(serverIpDto.Ip);
    }

    [Fact]
    public void GetServerIp_IpAddressIsNotNull()
    {
        var controller = new ServerInfoController();

        var result = controller.GetServerIp();
        var serverIpDto = result.Value;

        Assert.NotNull(serverIpDto);
        Assert.False(string.IsNullOrEmpty(serverIpDto.Ip));
    }
}
