using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private NetworkManager _networkManager;

        public ApiController()
        {
            _networkManager = new NetworkManager();
        }


        [HttpGet("ping")]
        public IActionResult Ping(string ip)
        {
            if (string.IsNullOrEmpty(ip) || !ip.Contains(".") || ip.Split('.').Length != 4)
                return FalseResult();



            var result = _networkManager.Ping(ip);
            if (result)
            {
                return TrueResult();
            }
            else
            {
                return FalseResult();
            }

        }

        [HttpGet("telnet")]
        public IActionResult Telnet(string ip, int port)
        {
            if (string.IsNullOrEmpty(ip) || !ip.Contains(".") || ip.Split('.').Length != 4 || port==0)
                return FalseResult();



            var result = _networkManager.Telnet(ip,port);
            if (result)
            {
                return TrueResult();
            }
            else
            {
                return FalseResult();
            }
        }
        [HttpGet("page")]
        public IActionResult Page(string url)
        {
            if (string.IsNullOrEmpty(url))
                return FalseResult();



            var result = _networkManager.Page(url);
            if (result)
            {
                return TrueResult();
            }
            else
            {
                return FalseResult();
            }
        }


        public ObjectResult TrueResult(object obj = null)
        {
            if (obj == null)
                obj = true;

            return Ok(obj);//başarılı ise            
        }
        public ObjectResult FalseResult(object obj = null)
        {
            if (obj == null)
                obj = false;

            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status408RequestTimeout, obj);//başarısız ise            
        }
    }
}
