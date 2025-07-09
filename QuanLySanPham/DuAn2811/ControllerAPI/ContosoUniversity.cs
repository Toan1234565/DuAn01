using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Nest;
using System.Security.Cryptography;

namespace Server._2811._2004.ControllerAPI
{
    public class ContosoUniversity
    {
        public class PaginatedList<T> : List<T>
        {
            public int PageIndex { get; private set; } // chi so hien taij
            public int TotaPages { get; private set; } //tong so trang 

            public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
            {
                PageIndex = pageIndex;
                TotaPages = (int)Math.Ceiling(count / (double)pageSize); //Xác định số trang(làm tròn lên) dựa trên số bản ghi.
                this.AddRange(items); // Thêm danh sách bản ghi của trang hiện tại vào danh sách PaginatedList<T>.
            }

            public bool HasPreviosPage => PageIndex > 1;
            public bool HasNextpage => PageIndex < TotaPages;
            
            public static async Task<PaginatedList<T>> CreateAsync(
                IQueryable<T> source, int pageIndex, int pageSize)
            {
                var count = await source.CountAsync(); // lấy thổng ban ghi 
                var item = await source.Skip(
                    (pageIndex - 1) * pageSize) // bỏ qua bản ghi trc đó 
                    .Take(pageSize).ToListAsync(); // chỉ lấy số lượng bản ghi của trang hiện tại.
                return new PaginatedList<T>(item, count, pageIndex, pageSize); // chứa danh sách bản ghi theo từng trang 
            }

        }
    }
}
