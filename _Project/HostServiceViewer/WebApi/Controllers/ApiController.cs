using Business;
using Business.Entities;
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
        private MongoDbManager _mongoDbManager;

        public ApiController()
        {
            _networkManager = new NetworkManager();
            _mongoDbManager = new MongoDbManager();
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return TrueResult("devam");

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
            if (string.IsNullOrEmpty(ip) || !ip.Contains(".") || ip.Split('.').Length != 4 || port == 0)
                return FalseResult();



            var result = _networkManager.Telnet(ip, port);
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


        [HttpGet("list")]
        public IActionResult List()
        {
            var testList = _mongoDbManager.SelectActive();
            return TrueResult(testList);

        }

        [HttpGet("selectall")]
        public IActionResult SelectAll()
        {
            var testList = _mongoDbManager.SelectAll();
            return TrueResult(testList);

        }
        [IgnoreAntiforgeryToken]
        [HttpPost("insert")]
        public IActionResult Insert(Test test)
        {
            var result = _mongoDbManager.Insert(test);
            return TrueResult(result);

        }
        [HttpPost("update")]
        public IActionResult Update(Test test)
        {
            var result = _mongoDbManager.Update(test);
            return TrueResult(result);

        }
        [IgnoreAntiforgeryToken]
        [HttpPost("delete")]
        public IActionResult Delete(Test test)
        {
            _mongoDbManager.Delete(test);
            return TrueResult();

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
