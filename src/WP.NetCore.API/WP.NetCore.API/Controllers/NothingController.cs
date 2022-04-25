using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NothingController : BaseController
    {
        private readonly INothingService nothingService;

        public NothingController(INothingService nothingService)
        {
            this.nothingService = nothingService;
        }


        /// <summary>
        /// 获取每日微语
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Nothing>> Get()
        {
            var data = await nothingService.GetTodayNothingData();
            return Ok(data);
        }
    }
}
