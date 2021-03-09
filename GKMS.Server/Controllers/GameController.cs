using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GKMS.Common;
using GKMS.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GKMS.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        private GameService GameService { get; set; }

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
            GameService = new GameService();
        }

        [HttpGet]
        public IEnumerable<IGame> Get()
        {
            return GameService.Get();
        }

        [HttpGet]
        [Route("GetKey/{gameType}")]
        public ContentResult GetKey(string gameType)
        {
            return Content(GameService.GetKey(gameType), "text/plain", Encoding.ASCII);
        }
    }
}
