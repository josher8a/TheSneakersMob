using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSneakersMob.Infrastructure.Data;
using TheSneakersMob.Models;
using TheSneakersMob.Services.Common;

namespace TheSneakersMob.Services.Sells
{
    public class SellRepository
    {
        private readonly ApplicationDbContext _context;

        public SellRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResponse<SellSummaryDto>> GetSellsAsync (GetSellsParameters parameters)
        {
            var query = _context.Sells.AsNoTracking()
                .Where(s => s.BuyerId == null);

            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                var category = parameters.Category.Trim();
                query = query.Where(s => s.Product.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Subcategoy))
            {
                var subcategory = parameters.Subcategoy.Trim();
                query = query.Where(s => s.Product.SubCategory.Name == subcategory);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Style))
            {
                var success = Enum.TryParse(parameters.Style.Trim(), out Style style);
                query = success ? query.Where(s => s.Product.Style == style) : query;
            }

            if (!string.IsNullOrWhiteSpace(parameters.Brand))
            {
                var brand = parameters.Brand.Trim();
                query = query.Where(s => s.Product.Brand.Name == brand);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Size))
            {
                var size = parameters.Size.Trim();
                query = query.Where(s => s.Product.Size.Description == size);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Condition))
            {
                var success = Enum.TryParse(parameters.Condition.Trim(), out Condition condition);
                query = success ? query.Where(s => s.Product.Condition == condition) : query;
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                var searchQuery = parameters.SearchQuery.Trim();
                query = query.Where(s => s.Product.Title.Contains(searchQuery)
                    || s.Product.Description.Contains(searchQuery));
            }


            var selectQuery = query
                .OrderByDescending(s => s.Created)
                .Select(s => new SellSummaryDto
                {
                    Id = s.Id,
                    Brand = s.Product.Brand.Name,
                    Title = s.Product.Title,
                    Description = s.Product.Description,
                    Condition = s.Product.Condition.ToString(),
                    Size = s.Product.Size.Description,
                    Price = s.Price.ToString(),
                    MainPictureUrl = s.Product.Photos.Any() ? s.Product.Photos.First().Url : null
                });


            return await PagedResponse<SellSummaryDto>.ToPagedResponse(selectQuery, parameters.PageNumber, parameters.PageSize);
        }
    }
}
