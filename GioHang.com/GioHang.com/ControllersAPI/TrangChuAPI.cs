using GioHang.com.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace GioHang.com.ControllersAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangChuAPI : ControllerBase
    {
        private readonly TmdtContext db;
        public TrangChuAPI(TmdtContext context)
        {
            db = context;
        }
        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            // lay thong tin gio hang theo ID
            var giohang = db.GioHangs
                .Where(gh => gh.MaGioHang == id)
                .Select(gh => new
                {
                    gh.MaGioHang,
                    gh.MaTk,
                    gh.SoLuongSanPham,
                    ChiTietGioHang = gh.ChiTietGioHangs.Select(ct => new
                    {
                        ct.MaChiTietGioHang,
                        ct.MaSanPham,
                        ct.MaGioHang
                    })
                }).FirstOrDefault();
            if(giohang == null)
            {
                return NotFound("gio hang khong tin tai");
            }
                
            return Ok(giohang);
        }

    }
}
