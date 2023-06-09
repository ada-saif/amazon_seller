﻿using AmazonRegistration.Interface;
using AmazonRegistration.Model;
using AmazonSellerApi.Model.OrderRelatedModel;
using AmazonSellerApi.SignRequest;
using Newtonsoft.Json;
using Npgsql;
using RestSharp;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace AmazonRegistration.Repo
{
    public class OrderRepo : Iorder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext db;
        private readonly IConfiguration _config;
        private readonly IGanrateAccessToken _tokenService;
        private readonly ISTSToken _stoken;
        public OrderRepo(ApplicationDbContext db, IConfiguration config, IHttpContextAccessor _httpContextAccessor, IGanrateAccessToken tokenService, ISTSToken _stoken)
        {
            this.db = db;
            _config = config;
            this._httpContextAccessor = _httpContextAccessor;
            _tokenService = tokenService;
            this._stoken = _stoken;
        }

        public object GetOrder()
        {
            Response response = new Response();
            var totalrecords = 0;
            var filterRecord = 0;

            int pageSize = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Form["length"].FirstOrDefault() ?? "0");
            var start = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Form["start"].FirstOrDefault());
            var length = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Form["length"].FirstOrDefault());
            int skip = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Form["start"].FirstOrDefault() ?? "0");
            var draw = _httpContextAccessor.HttpContext.Request.Form["draw"].FirstOrDefault();
            var searchValue = _httpContextAccessor.HttpContext.Request.Form["search[value]"].FirstOrDefault();
            var sortColumn = _httpContextAccessor.HttpContext.Request.Form[$"columns[{_httpContextAccessor.HttpContext.Request.Form["order[0][column]"].FirstOrDefault()}][name]"].FirstOrDefault();
            var sortColumnDirection = _httpContextAccessor.HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var connectionString = _config.GetConnectionString("Amazon");
            var sql = "SELECT amazon_order_id, marchant_id,seller_order_id,purchase_date,last_update_date,order_status,fulfillment_channel,sales_channel,ship_service_level,number_of_items_shipped,number_of_items_unshipped,payment_method,marketplace_id,shipment_service_level_category,easy_ship_shipment_status,order_type,earliest_ship_date,latest_ship_date,earliest_delivery_date,latest_delivery_date,is_business_order,is_prime,is_premium_order,is_global_express_enabled,is_replacement_order,is_estimated_ship_date_set,is_sold_by_a_b,is_i_b_a,is_i_s_p_u,is_access_point_order,has_regulated_items FROM tbl_order";
            var countSql = "SELECT COUNT(*) FROM tbl_order";
            var searchParam = string.IsNullOrEmpty(searchValue) ? string.Empty : $"%{searchValue.ToLower()}%";
            var searchSql = "WHERE LOWER(seller_order_id)  iLIKE " + searchParam +
                " OR LOWER(amazon_order_id) iLIKE " + searchParam +
                " OR LOWER(marchant_id) iLIKE " + searchParam +
                " OR LOWER(purchase_date) iLIKE " + searchParam +
                " OR LOWER(last_update_date) iLIKE " + searchParam +
                " OR LOWER(order_status) iLIKE " + searchParam +
                " OR LOWER(fulfillment_channel) iLIKE " + searchParam +
                " OR LOWER(sales_channel) iLIKE " + searchParam +
                " OR LOWER(ship_service_level) iLIKE " + searchParam +
                " OR LOWER(number_of_items_shipped) iLIKE " + searchParam +
                " OR LOWER(number_of_items_unshipped) iLIKE " + searchParam +
                " OR LOWER(payment_method) iLIKE " + searchParam +
                " OR LOWER(marketplace_id) iLIKE " + searchParam +
                " OR LOWER(shipment_service_level_category) iLIKE " + searchParam +
                " OR LOWER(easy_ship_shipment_status) iLIKE " + searchParam +
                " OR LOWER(OrderType) iLIKE " + searchParam +
                " OR LOWER(earliest_ship_date) iLIKE " + searchParam +
                " OR LOWER(latest_ship_date) iLIKE " + searchParam +
                " OR LOWER(earliest_delivery_date) iLIKE " + searchParam +
                " OR LOWER(latest_delivery_date) iLIKE " + searchParam +
                " OR LOWER(is_business_order) iLIKE " + searchParam +
                " OR LOWER(is_prime) iLIKE " + searchParam +
                " OR LOWER(is_premium_order) iLIKE " + searchParam +
                " OR LOWER(is_global_express_enabled) iLIKE " + searchParam +
                " OR LOWER(is_replacement_order) iLIKE " + searchParam +
                " OR LOWER(is_estimated_ship_date_set) iLIKE " + searchParam +
                " OR LOWER(is_sold_by_a_b) iLIKE " + searchParam +
                " OR LOWER(is_i_b_a) iLIKE " + searchParam +
                " OR LOWER(is_i_s_p_u) iLIKE " + searchParam +
                " OR LOWER(is_access_point_order) iLIKE " + searchParam +
                " OR LOWER(has_regulated_items) iLIKE " + searchParam;



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
                sql += $" OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY";


                using (var command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader? reader = command.ExecuteReader())
                    {
                        var orderList = new List<DataTableOrder>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var order = new DataTableOrder
                                {
                                    amazon_order_id = reader.IsDBNull(0) ? "" : reader.GetString(0),
                                    marchant_id = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    seller_order_id = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    purchase_date = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                    last_update_date = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    order_status = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                    fulfillment_channel = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                    sales_channel = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    ship_service_level = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                    number_of_items_shipped = reader.GetInt32(9),
                                    number_of_items_unshipped = reader.GetInt32(10),
                                    payment_method = reader.IsDBNull(11) ? "" : reader.GetString(11),
                                    marketplace_id = reader.IsDBNull(12) ? "" : reader.GetString(12),
                                    shipment_service_level_category = reader.IsDBNull(13) ? "" : reader.GetString(13),
                                    easy_ship_shipment_status = reader.IsDBNull(14) ? "" : reader.GetString(14),
                                    OrderType = reader.IsDBNull(15) ? "" : reader.GetString(15),
                                    earliest_ship_date = reader.IsDBNull(16) ? "" : reader.GetString(16),
                                    latest_ship_date = reader.IsDBNull(17) ? "" : reader.GetString(17),
                                    earliest_delivery_date = reader.IsDBNull(18) ? "" : reader.GetString(18),
                                    latest_delivery_date = reader.IsDBNull(19) ? "" : reader.GetString(19),
                                    is_business_order = reader.GetBoolean(20),
                                    is_prime = reader.GetBoolean(21),
                                    is_premium_order = reader.GetBoolean(22),
                                    is_global_express_enabled = reader.GetBoolean(23),
                                    is_replacement_order = reader.GetBoolean(24),
                                    is_estimated_ship_date_set = reader.GetBoolean(25),
                                    is_sold_by_a_b = reader.GetBoolean(26),
                                    is_i_b_a = reader.GetBoolean(27),
                                    is_i_s_p_u = reader.GetBoolean(28),
                                    is_access_point_order = reader.GetBoolean(29),
                                    has_regulated_items = reader.GetBoolean(30),
                                };

                                orderList.Add(order);
                            }
                        }

                        var returnObj = new
                        {
                            draw = draw,
                            recordsTotal = totalrecords,
                            recordsFiltered = filterRecord,
                            data = orderList
                        };
                        return returnObj;
                    }
                }
            }

            return null;

        }

        public async Task<OrderData> LoadDataFromAmazon(inputFeild input)
        {
            var response = new Response();

            if (input == null) { return null; }
            if (input.fromDate == null && input.toDate == null) { return null; }
            Dictionary<string, string> myDictionary = new Dictionary<string, string>(){
                                                       {"CreatedAfter",input.fromDate },
                                                       {"CreatedBefore",input.toDate}
             };
            string url = "";
            var query = myDictionary.Where(x => !string.IsNullOrEmpty(x.Value) || !string.IsNullOrWhiteSpace(x.Value)).ToDictionary(x => x.Key, x => x.Value);
            var qs = string.Join("&", query.OrderBy(q => q.Key).Select(q => q.Key + "=" + Uri.EscapeDataString(q.Value)));
            var headers = new Dictionary<string, string>()
            {
                {  "x-amz-access-token" , _tokenService.GetToken(input.sub_id) .Result.access_token}
            };
            var rr = OrderSignRequest.SignRequest(Method.Get, url, qs, "", null, _stoken.GetToken(input.sub_id).Result);
            RestClient rc = new RestClient();
            var resp = rc.ExecuteGet(rr);
            var data = JsonConvert.DeserializeObject<OrderData>(resp.Content);
            return data;
        }
        public string insertOrderData(OrderData order, int user)
        {
            var connectionString = _config.GetConnectionString("Amazon");
            var table = new DataTable();
            table.Columns.Add("profile_id", typeof(int));
            table.Columns.Add("amazon_order_id", typeof(int));
            table.Columns.Add("seller_order_id", typeof(string));
            table.Columns.Add("PurchaseDate", typeof(string));
            table.Columns.Add("LastUpdateDate", typeof(string));
            table.Columns.Add("SalesChannel", typeof(string));
            table.Columns.Add("OrderChannel", typeof(string));
            table.Columns.Add("ShipServiceLevel", typeof(string));
            table.Columns.Add("order_total", typeof(string));
            table.Columns.Add("number_of_items_shipped", typeof(int));
            table.Columns.Add("number_of_items_unshipped", typeof(int));
            table.Columns.Add("order_status", typeof(string));
            table.Columns.Add("fulfillment_channel", typeof(string));
            table.Columns.Add("payment_method", typeof(string));
            table.Columns.Add("marketplace_id", typeof(string));
            table.Columns.Add("shipment_service_level_category", typeof(string));
            table.Columns.Add("easy_ship_shipment_status", typeof(string));
            table.Columns.Add("cba_displayable_shipping_label", typeof(string));
            table.Columns.Add("order_type", typeof(string));
            table.Columns.Add("earliest_ship_date", typeof(string));
            table.Columns.Add("latest_ship_date", typeof(string));
            table.Columns.Add("earliest_delivery_date", typeof(string));
            table.Columns.Add("latest_delivery_date", typeof(string));
            table.Columns.Add("is_business_order", typeof(bool));
            table.Columns.Add("is_prime", typeof(bool));
            table.Columns.Add("is_premium_order", typeof(bool));
            table.Columns.Add("is_global_express_enabled", typeof(bool));
            table.Columns.Add("replaced_order_id", typeof(string));
            table.Columns.Add("is_replacement_order", typeof(bool));
            table.Columns.Add("promise_response_due_date", typeof(string));
            table.Columns.Add("is_estimated_ship_date_set", typeof(bool));
            table.Columns.Add("is_sold_by_a_b", typeof(bool));
            table.Columns.Add("is_i_b_a", typeof(bool));
            table.Columns.Add("buyer_invoice_preference", typeof(string));
            table.Columns.Add("is_i_s_p_u", typeof(bool));
            table.Columns.Add("is_access_point_order", typeof(bool));
            table.Columns.Add("seller_display_name", typeof(string));
            table.Columns.Add("has_regulated_items", typeof(bool));
            table.Columns.Add("electronic_invoice_status", typeof(string));

            foreach (var obj in order.payload.Orders)
            {
                table.Rows.Add(user, obj.AmazonOrderId, obj.SellerOrderId, obj.PurchaseDate, obj.LastUpdateDate,
                obj.OrderStatus, obj.FulfillmentChannel, obj.SalesChannel, obj.OrderChannel, obj.ShipServiceLevel,
                obj.NumberOfItemsShipped, obj.NumberOfItemsUnshipped, obj.PaymentMethod, obj.MarketplaceId,
                obj.ShipmentServiceLevelCategory, obj.EasyShipShipmentStatus, obj.CbaDisplayableShippingLabel, obj.OrderType,
                obj.EarliestShipDate, obj.EarliestDeliveryDate, obj.LatestDeliveryDate, obj.IsBusinessOrder, obj.IsPrime, obj.IsPremiumOrder,
                obj.IsGlobalExpressEnabled, obj.ReplacedOrderId, obj.IsReplacementOrder, obj.PromiseResponseDueDate, obj.IsEstimatedShipDateSet,
                obj.IsSoldByAB, obj.IsIBA, obj.BuyerInvoicePreference, obj.IsISPU, obj.IsAccessPointOrder, obj.SellerDisplayName, obj.SellerDisplayName,
                obj.HasRegulatedItems, obj.ElectronicInvoiceStatus);
            }
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var writer = connection.BeginBinaryImport($"COPY MyTable (profile_id, amazon_order_id,seller_order_id,PurchaseDate,LastUpdateDate,SalesChannel,OrderChannel,OrderChannel,ShipServiceLevel,order_total,number_of_items_shipped,number_of_items_unshipped,order_status,fulfillment_channel,payment_method,marketplace_id,shipment_service_level_category,easy_ship_shipment_status,cba_displayable_shipping_label,order_type,earliest_ship_date,latest_ship_date,earliest_delivery_date,latest_delivery_date,latest_delivery_date,is_business_order,is_prime,is_premium_order,is_premium_order,is_global_express_enabled,replaced_order_id,is_replacement_order,promise_response_due_date,is_estimated_ship_date_set,is_sold_by_a_b,is_i_b_a,buyer_invoice_preference,is_i_s_p_u,is_access_point_order,seller_display_name,has_regulated_items,electronic_invoice_status) FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        writer.StartRow();
                        writer.Write(row["profile_id"], NpgsqlTypes.NpgsqlDbType.Integer);
                        writer.Write(row["amazon_order_id"], NpgsqlTypes.NpgsqlDbType.Varchar);
                    }
                    writer.Complete();
                    return null;

                }

            }
        }
    }
}
