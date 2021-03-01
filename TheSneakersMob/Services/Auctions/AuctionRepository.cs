using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheSneakersMob.Infrastructure.Data;
using TheSneakersMob.Models;
using TheSneakersMob.Services.Common;

namespace TheSneakersMob.Services.Auctions
{
    public class AuctionRepository
    {
        private readonly ApplicationDbContext _context;

        public AuctionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResponse<AuctionSummaryDto>> GetAuctionsAsync(GetAuctionsParameters parameters)
        {
            var query = _context.Auctions.AsNoTracking()
                .Where(a => a.Buyer == null);

            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                var category = parameters.Category.Trim();
                query = query.Where(a => a.Product.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Subcategoy))
            {
                var subcategory = parameters.Subcategoy.Trim();
                query = query.Where(a => a.Product.SubCategory.Name == subcategory);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Style))
            {
                var success = Enum.TryParse(parameters.Style.Trim(), out Style style);
                query = success ? query.Where(a => a.Product.Style == style) : query;
            }

            if (!string.IsNullOrWhiteSpace(parameters.Brand))
            {
                var brand = parameters.Brand.Trim();
                query = query.Where(a => a.Product.Brand.Name == brand);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Size))
            {
                var size = parameters.Size.Trim();
                query = query.Where(a => a.Product.Size.Description == size);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Condition))
            {
                var success = Enum.TryParse(parameters.Condition.Trim(), out Condition condition);
                query = success ? query.Where(a => a.Product.Condition == condition) : query;
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                var searchQuery = parameters.SearchQuery.Trim();
                query = query.Where(a => a.Product.Title.Contains(searchQuery)
                    || a.Product.Description.Contains(searchQuery));
            }


            var selectQuery = query
                .OrderByDescending(a => a.Created)
                .Select(a => new AuctionSummaryDto
                {
                    Id = a.Id,
                    Brand = a.Product.Brand.Name,
                    Title = a.Product.Title,
                    Description = a.Product.Description,
                    Condition = a.Product.Condition.ToString(),
                    Size = a.Product.Size.Description,
                    InitialPrize = a.InitialPrize.ToString(),
                    DirectBuyPrize = a.DirectBuyPrice.ToString(),
                    Bids = a.Bids.Select(b => new BidSummaryDto { 
                        Amount = b.Amount.ToString()
                    }).ToList(),
                    MainPictureUrl = a.Product.Photos.Any() ? a.Product.Photos.First().Url : null
                });


            return await PagedResponse<AuctionSummaryDto>.ToPagedResponse(selectQuery, parameters.PageNumber, parameters.PageSize);
        }
    }
}
