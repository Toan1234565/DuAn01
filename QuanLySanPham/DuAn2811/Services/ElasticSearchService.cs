using DuAn2811_.Models;
using Nest;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server._2811._2004.Services
{
    public class ElasticSearchService
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<ElasticSearchService> _logger;

        public ElasticSearchService(ILogger<ElasticSearchService> logger)
        {
            _logger = logger;
            var settings = new ConnectionSettings(new Uri("http://localhost:7247"))
                .DefaultIndex("SanPham")
                .EnableDebugMode() // Hiển thị thông tin chi tiết về truy vấn
                .DisableDirectStreaming(); // Giúp hiển thị lỗi chi tiết hơn
            _elasticClient = new ElasticClient(settings);
        }

        public async Task<List<SanPham>> SearchProductsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Từ khóa tìm kiếm không hợp lệ.");
            }

            try
            {
                var response = await _elasticClient.SearchAsync<SanPham>(s => s
                    .Query(q => q.Bool(b => b
                        .Should(
                            q.Match(m => m.Field(f => f.TenSanPham).Query(query)),
                            q.Fuzzy(f => f.Field(f => f.TenSanPham).Value(query).Fuzziness(Fuzziness.Auto))
                        )
                    ))
                ).ConfigureAwait(false); // Tối ưu hóa hiệu suất

                if (!response.IsValid)
                {
                    _logger.LogError($"Lỗi Elasticsearch: {response.DebugInformation}");
                    throw new InvalidOperationException("Lỗi Elasticsearch, vui lòng kiểm tra lại.");
                }

                if (response.Documents == null || response.Documents.Count == 0)
                {
                    _logger.LogWarning($"Không tìm thấy sản phẩm nào phù hợp với từ khóa: {query}");
                    return new List<SanPham>();
                }

                _logger.LogInformation($"Tìm thấy {response.Documents.Count} sản phẩm cho từ khóa '{query}'.");
                return response.Documents.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi gọi Elasticsearch: {ex.Message}");
                throw;
            }
        }
    }
}
