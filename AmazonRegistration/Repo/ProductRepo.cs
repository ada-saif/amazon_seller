using AmazonRegistration.Interface;
using AmazonRegistration.Model;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace AmazonRegistration.Repo
{
    namespace AmazonRegistration.Repo
    {
        public class ProductRepo : IProduct
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ApplicationDbContext db;
            private readonly IConfiguration _config;
            public ProductRepo(ApplicationDbContext db, IConfiguration config, IHttpContextAccessor _httpContextAccessor)
            {
                this.db = db;
                _config = config;
                this._httpContextAccessor = _httpContextAccessor;
            }

            public object GetProduct()
            {

                Response response = new Response();
                var totalrecords = 0;
                var filterRecord = 0;
                int pageSize = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Form["length"].FirstOrDefault() ?? "0");
                var start = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Form["start"].FirstOrDefault());
                var length = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Form["length"].FirstOrDefault());
                int skip = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Form["start"].FirstOrDefault() ?? "0");
                var draw = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Form["draw"].FirstOrDefault());
                var searchValue = _httpContextAccessor.HttpContext.Request.Form["search[value]"].FirstOrDefault();
                var sortColumn = _httpContextAccessor.HttpContext.Request.Form["columns[" + _httpContextAccessor.HttpContext.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = _httpContextAccessor.HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
                var connectionString = _config.GetConnectionString("Amazon");
                var sql = "select * from public.tbl_product";
                var countSql = "SELECT COUNT(*) FROM tbl_product";
                var searchParam = string.IsNullOrEmpty(searchValue) ? string.Empty : $"'%{searchValue.ToLower()}%'";
                var searchSql = " WHERE LOWER(a_s_i_n) iLIKE " + searchParam +
                " OR LOWER(marketplace) iLIKE " + searchParam +
                " OR LOWER(brand) iLIKE " + searchParam +
                " OR LOWER(browse_classification_name) iLIKE " + searchParam +
                " OR LOWER(browse_classification_id) iLIKE " + searchParam +
                " OR LOWER(color) iLIKE " + searchParam +
                " OR LOWER(item_name) iLIKE " + searchParam +
                " OR LOWER(manufacturer) iLIKE " + searchParam +
                " OR LOWER(model_number) iLIKE " + searchParam +
                " OR LOWER(part_number) iLIKE " + searchParam +
                " OR LOWER(size) iLIKE " + searchParam +
                " OR LOWER(style) iLIKE " + searchParam +
                " OR LOWER(website_display_group) iLIKE " + searchParam +
                " OR LOWER(website_display_group_name) iLIKE " + searchParam;


                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    if (searchValue == "")
                    {

                        using (var command = new NpgsqlCommand(countSql, connection))
                        {
                            totalrecords = Convert.ToInt32(command.ExecuteScalar());
                            filterRecord = totalrecords;
                        }
                    }

                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        countSql += searchSql;
                        using (var command = new NpgsqlCommand(countSql, connection))
                        {
                            totalrecords = Convert.ToInt32(command.ExecuteScalar());
                        }
                        using (var command = new NpgsqlCommand(countSql, connection))
                        {
                            filterRecord = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }

                    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                    {

                        sql += $" ORDER BY {sortColumn} {sortColumnDirection}";
                        filterRecord = totalrecords;
                    }
                    //else
                    //{
                    //    sortColumn = "a_s_i_n";
                    //    sql += $" ORDER BY {sortColumn} {sortColumnDirection}";
                    //    filterRecord = totalrecords;

                    //}
                    sql += $" OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY";

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        using (NpgsqlDataReader? reader = command.ExecuteReader())
                        {
                            var productList = new List<Product>();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var product = new Product
                                    {
                                        ASIN = reader.IsDBNull(0) ? "" : reader.GetString(0),
                                        MarketplaceId = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                        Marketplace = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                        AdultProduct = reader.GetBoolean(3),
                                        Autographed = reader.GetBoolean(4),
                                        Brand = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                        BrowseClassificationName = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                        BrowseClassificationId = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                        Color = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                        ItemClassification = reader.IsDBNull(9) ? "" : reader.GetString(10),
                                        ItemName = reader.IsDBNull(10) ? "" : reader.GetString(10),
                                        Manufacturer = reader.IsDBNull(11) ? "" : reader.GetString(11),
                                        Memorabilia = reader.GetBoolean(12),
                                        ModelNumber = reader.IsDBNull(13) ? "" : reader.GetString(13),
                                        PackageQuantity = reader.GetInt32(14),
                                        PartNumber = reader.IsDBNull(15) ? "" : reader.GetString(15),
                                        ReleaseDate = reader.IsDBNull(16) ? null : reader.GetDateTime(16),
                                        Size = reader.IsDBNull(17) ? "" : reader.GetString(17),
                                        Style = reader.IsDBNull(18) ? "" : reader.GetString(18),
                                        TradeInEligible = reader.GetBoolean(19),
                                        WebsiteDisplayGroup = reader.IsDBNull(20) ? "" : reader.GetString(20),
                                        WebsiteDisplayGroupName = reader.IsDBNull(21) ? "" : reader.GetString(21)
                                    };

                                    productList.Add(product);
                                }
                            }

                            var returnObj = new
                            {
                                draw = draw,
                                recordsTotal = totalrecords,
                                recordsFiltered = filterRecord,
                                data = productList
                            };

                            return returnObj;
                        }
                    }
                }
                return null;

            }



        }
    }

}
