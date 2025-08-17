using Project.Application.DTOs;

namespace Project.Application.Mapping.Product
{
    public static class ProductMapper
    {
        public static List<ProductResponseDto> GetAllProductMapper(this List<Domain.ViewClasses.ViewProductData> viewProductDatas)
        {
            return viewProductDatas.GroupBy(p => p.ProductID)
                .Select(g => new ProductResponseDto
                {
                    ProductID = g.Key,
                    ProductName = g.FirstOrDefault()?.ProductName,
                    QuantityPerUnit = g.FirstOrDefault()?.QuantityPerUnit,
                    UnitPrice = g.FirstOrDefault()?.UnitPrice ?? 0,
                    UnitsInStock = g.FirstOrDefault()?.UnitsInStock ?? 0,
                    UnitsOnOrder = g.FirstOrDefault()?.UnitsOnOrder ?? 0,
                    ReorderLevel = g.FirstOrDefault()?.ReorderLevel ?? 0,
                    Discontinued = g.FirstOrDefault()?.Discontinued ?? false,
                    Categories = g.GroupBy(c => c.CategoryGuid)
                    .Select(cg => new Domain.Categories
                    {
                        CategoryGuid = cg.Key,
                        CategoryID = cg.Select(c => c.CategoryID).FirstOrDefault() ?? 0,
                        CategoryName = cg.Select(c => c.CategoryName).FirstOrDefault() ?? string.Empty,
                        Description = cg.Select(c => c.Description).FirstOrDefault() ?? string.Empty,
                        Picture = cg.Select(c => c.Picture).FirstOrDefault() ?? Array.Empty<byte>()
                    }).ToList(),
                    Suppliers = g.GroupBy(s => s.SupplierID)
                    .Select(sg => new Domain.Supplier
                    {
                        SupplierID = sg.Key ?? 0,
                        Address = sg.Select(s => s.Address).FirstOrDefault() ?? string.Empty,
                        City = sg.Select(s => s.City).FirstOrDefault() ?? string.Empty,
                        CompanyName = sg.Select(s => s.CompanyName).FirstOrDefault() ?? string.Empty,
                        ContactName = sg.Select(s => s.ContactName).FirstOrDefault() ?? string.Empty,
                        ContactTitle = sg.Select(s => s.ContactTitle).FirstOrDefault() ?? string.Empty,
                        Country = sg.Select(s => s.Country).FirstOrDefault() ?? string.Empty,
                        Fax = sg.Select(s => s.Fax).FirstOrDefault() ?? string.Empty,
                        HomePage = sg.Select(s => s.HomePage).FirstOrDefault() ?? string.Empty,
                        Phone = sg.Select(s => s.Phone).FirstOrDefault() ?? string.Empty,
                        PostalCode = sg.Select(s => s.PostalCode).FirstOrDefault() ?? string.Empty,
                        Region = sg.Select(s => s.Region).FirstOrDefault() ?? string.Empty,
                    }).ToList()
                }).ToList();
        }
        public static ProductResponseDto GetSingleProductMapper(this List<Domain.ViewClasses.ViewProductData> viewProductDatas)
        {
            return viewProductDatas.GroupBy(p => p.ProductID)
                .Select(g => new ProductResponseDto
                {
                    ProductID = g.Key,
                    ProductName = g.FirstOrDefault()?.ProductName,
                    QuantityPerUnit = g.FirstOrDefault()?.QuantityPerUnit,
                    UnitPrice = g.FirstOrDefault()?.UnitPrice ?? 0,
                    UnitsInStock = g.FirstOrDefault()?.UnitsInStock ?? 0,
                    UnitsOnOrder = g.FirstOrDefault()?.UnitsOnOrder ?? 0,
                    ReorderLevel = g.FirstOrDefault()?.ReorderLevel ?? 0,
                    Discontinued = g.FirstOrDefault()?.Discontinued ?? false,
                    Categories = g.GroupBy(c => c.CategoryGuid)
                    .Select(cg => new Domain.Categories
                    {
                        CategoryGuid = cg.Key,
                        CategoryID = cg.Select(c => c.CategoryID).FirstOrDefault() ?? 0,
                        CategoryName = cg.Select(c => c.CategoryName).FirstOrDefault() ?? string.Empty,
                        Description = cg.Select(c => c.Description).FirstOrDefault() ?? string.Empty,
                        Picture = cg.Select(c => c.Picture).FirstOrDefault() ?? Array.Empty<byte>()
                    }).ToList(),
                    Suppliers = g.GroupBy(s => s.SupplierID)
                    .Select(sg => new Domain.Supplier
                    {
                        SupplierID = sg.Key ?? 0,
                        Address = sg.Select(s => s.Address).FirstOrDefault() ?? string.Empty,
                        City = sg.Select(s => s.City).FirstOrDefault() ?? string.Empty,
                        CompanyName = sg.Select(s => s.CompanyName).FirstOrDefault() ?? string.Empty,
                        ContactName = sg.Select(s => s.ContactName).FirstOrDefault() ?? string.Empty,
                        ContactTitle = sg.Select(s => s.ContactTitle).FirstOrDefault() ?? string.Empty,
                        Country = sg.Select(s => s.Country).FirstOrDefault() ?? string.Empty,
                        Fax = sg.Select(s => s.Fax).FirstOrDefault() ?? string.Empty,
                        HomePage = sg.Select(s => s.HomePage).FirstOrDefault() ?? string.Empty,
                        Phone = sg.Select(s => s.Phone).FirstOrDefault() ?? string.Empty,
                        PostalCode = sg.Select(s => s.PostalCode).FirstOrDefault() ?? string.Empty,
                        Region = sg.Select(s => s.Region).FirstOrDefault() ?? string.Empty,
                    }).ToList()
                }).FirstOrDefault()!;
        }
    }
}
