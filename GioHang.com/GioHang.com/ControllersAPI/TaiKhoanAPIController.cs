using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace GioHang.com.ControllersAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanAPIController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public TaiKhoanAPIController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
