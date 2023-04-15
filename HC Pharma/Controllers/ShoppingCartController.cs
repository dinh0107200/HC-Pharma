﻿using HC_Pharma.Models;
using HC_Pharma.ViewModel;
using Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace HC_Pharma.Controllers
{
    [RoutePrefix("gio-hang")]
    public class ShoppingCartController : BaseController
    {
        public ConfigSite ConfigSite => (ConfigSite)HttpContext.Application["ConfigSite"];
        private static string Smtp => WebConfigurationManager.AppSettings["smtp"];
        private static string Email => WebConfigurationManager.AppSettings["email"];
        private static string Password => WebConfigurationManager.AppSettings["password"];
        private static int SmtpPort => Convert.ToInt32(WebConfigurationManager.AppSettings["smtpport"]);

        //List<int?> DistrictIds = new List<int?>() { 1, 732, 13, 78, 76, 15, 14, 79, 77, 85, 87, 80 };
        [Route("thong-tin")]
        public ActionResult Index(string returnUrl)
        {
            var cart = ShoppingCart.GetCart(HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            ViewBag.ReturnUrl = returnUrl;
            return View(viewModel);
        }
        [HttpPost, Route("thong-tin")]
        public ActionResult Index(FormCollection fc)
        {
            var records = fc.GetValues("RecordId");
            var quantities = fc.GetValues("Quantity");

            if (records == null || quantities == null)
            {
                return RedirectToActionPermanent("Index");
            }
            for (var i = 0; i < records.Length; i++)
            {
                var recordId = Convert.ToInt32(records[i]);
                var quantity = Convert.ToInt32(quantities[i]);

                var cartItem = _unitOfWork.CartRepository.GetById(recordId);
                if (cartItem == null || cartItem.Count == quantity || quantity < 1) continue;

                cartItem.Count = quantity;
                _unitOfWork.Save();
            }
            return RedirectToActionPermanent("Index");
        }
        [Route("thanh-toan")]
        public ActionResult CheckOut()
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            if (!cart.GetCartItems().Any())
            {
                return RedirectToAction("Index");
            }
            var carts = cart.GetCartItems();

            var itemCarts = carts.Select(a => new CheckOutViewModel.CartItem
            {
                CartItems = a,
            });
            var model = new CheckOutViewModel
            {
                Order = new Order
                {
                    TypePay = 1,
                    Transport = 1,
                },
                CartItems = itemCarts,
                CartTotal = cart.GetTotal(),
                CitySelectList = CitySelectList,
            };
            return View(model);
        }
        [Route("thanh-toan")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CheckOut(CheckOutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var carts = ShoppingCart.GetCart(HttpContext);
                var item = carts.GetCartItems();

                if (carts.GetTotal() < 100000)
                {
                    return RedirectToAction("Index");
                }

                model.Order.CityId = model.CityId;
                model.Order.DistrictId = model.DistrictId;
                //model.Order.WardId = model.WardId;
                _unitOfWork.OrderRepository.Insert(model.Order);
                _unitOfWork.Save();

                model.Order.MaDonHang = DateTime.Now.ToString("yyyyMMddHHmm") + "C" + model.Order.Id;

                foreach (var odetails in from cart1 in item
                                         let product = _unitOfWork.ProductRepository.GetById(cart1.ProductId)
                                         select new OrderDetail
                                         {
                                             OrderId = model.Order.Id,
                                             ProductId = cart1.ProductId,
                                             Quantity = cart1.Count,
                                             Price = (int?)cart1.Price,
                                         })
                {
                    _unitOfWork.OrderDetailRepository.Insert(odetails);
                }
                _unitOfWork.Save();

                //Thanh toán CK
                var district = _unitOfWork.DistrictRepository.GetById(model.DistrictId);
                var tongtien = 0m;

                var typepay = "Thanh toán khi nhận hàng";
                switch (model.Order.TypePay)
                {
                    case 1:
                        typepay = "Tiền mặt";
                        break;
                    case 2:
                        typepay = "Chuyển khoản";
                        break;
                }
                var typetransport = "Hình thức vận chuyển";
                switch (model.Order.Transport)
                {
                    case 1:
                        typetransport = "Đến địa chỉ người nhận";
                        break;
                    case 2:
                        typetransport = "Khách đến nhận hàng";
                        break;
                    case 3:
                        typetransport = "Qua bưu điện";
                        break;
                    case 4:
                        typetransport = "Hình thức khác";
                        break;
                }
                var sb = "<p>Cảm ơn quý khách đã đặt hàng tại website " + Request.Url?.Host + "</p>" +
                         "<p style='font-size:16px'>Thông tin đơn hàng gửi từ website " + Request.Url?.Host + "</p>";
                sb += "<p>Mã đơn hàng: <strong>" + model.Order.CreateDate.ToString("yyMMddHHmmss") + "</strong></p>";

                sb += "<p>Họ và tên: <strong>" + model.Order.CustomerInfo.Fullname + "</strong></p>";
                sb += "<p>Địa chỉ: <strong>" + model.Order.CustomerInfo.Address + ", " + district?.Name + ", " + district?.City.Name + "</strong></p>";
                sb += "<p>Email: <strong>" + model.Order.CustomerInfo.Email + "</strong></p>";
                sb += "<p>Điện thoại: <strong>" + model.Order.CustomerInfo.Mobile + "</strong></p>";
                sb += "<p>Yêu cầu thêm: <strong>" + model.Order.CustomerInfo.Body + "</strong></p>";

                sb += "<p>Ngày đặt hàng: <strong>" + model.Order.CreateDate.ToString("dd-MM-yyyy HH:ss") + "</strong></p>";
                sb += "<p>Hình thức thanh toán: <strong>" + typepay + "</strong></p>";
                sb += "<p>Hình thức vận chuyển: <strong>" + typetransport + "</strong></p>";
                sb += "<p>Thông tin đơn hàng</p>";
                sb += "<table border='1' cellpadding='10' style='border:1px #ccc solid;border-collapse: collapse'>" +

                      "<tr>" +
                      "<th>Ảnh sản phẩm</th>" +
                      "<th>Tên sản phẩm</th>" +
                      "<th>Số lượng</th>" +
                      "<th>Giá tiền</th>" +
                      "<th>Thành tiền</th>" +
                      "</tr>";
                foreach (var odetails in model.Order.OrderDetails)
                {
                    var thanhtien = Convert.ToDecimal(odetails.Price * odetails.Quantity);
                    tongtien += thanhtien;

                    var img = "NO PICTURE";
                    if (odetails.Product.ListImage != null)
                    {
                        img = "<img src='" + Request.Url?.GetLeftPart(UriPartial.Authority) + "/images/products/" + odetails.Product.ListImage.Split(',')[0] + "?w=100' />";
                    }
                    sb += "<tr>" +
                          "<td>" + img + "</td>" +
                          "<td><a href='" + Request.Url?.GetLeftPart(UriPartial.Authority) + Url.Action("ProductDetail", "Home", new { proId = odetails.ProductId, name = HtmlHelpers.ConvertToUnSign(null, odetails.Product.Name) }) + "' >" + odetails.Product.Name + "</a>";
                    sb += "</td>" +
                          "<td style='text-align:center'>" + odetails.Quantity + "</td>" +
                          "<td style='text-align:center'>" + Convert.ToDecimal(odetails.Price).ToString("N0") + "</td>" +
                          "<td style='text-align:center'>" + thanhtien.ToString("N0") + " đ</td>" +
                          "</tr>";
                }
                sb += "<tr><td colspan='5' style='text-align:right'><strong>Tổng tiền: " + tongtien.ToString("N0") + " đ</strong></td></tr>";
                sb += "</table>";
                sb += "<p>Cảm ơn bạn đã tin tưởng và mua hàng của chúng tôi.</p>";

                var orderId = DateTime.Now.ToString("yyMMddHHmmss");

                Task.Run(() =>
                {
                    HtmlHelpers.SendEmail(Smtp, "[" + orderId + "] Đơn đặt hàng từ website " + Request.Url?.Host, sb,
                        ConfigSite.Email, Email, Email, Password, "Đặt Hàng Online", model.Order.CustomerInfo.Email, ConfigSite.Email, port: SmtpPort);
                });

                return RedirectToAction("CheckOutComplete", new { orderId });
            }
            var cart = ShoppingCart.GetCart(HttpContext);
            model.CartTotal = cart.GetTotal();
            model.CitySelectList = model.CitySelectList;
            if (model.CityId > 0)
            {
                model.DistrictSelectList = DistrictSelectList(model.CityId);
            }
            //if (model.DistrictId != null)
            //{
            //    model.WardSelectList = DistrictSelectList(model.DistrictId);
            //}
            return View(model);
        }

        [Route("thanh-toan-thanh-cong")]
        public ActionResult CheckOutComplete(string orderId)
        {
            EmptyCart();
            ViewBag.OrderId = orderId;
            return View();
        }

        public ActionResult EmptyCart()
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            cart.EmptyCart();
            return RedirectToAction("Index");
        }

        [Route("them-vao-gio-hang")]
        public JsonResult AddToCart(int productId, int quantity = 1)
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            decimal? price = null;

            var addedProduct = _unitOfWork.ProductRepository.GetQuery(a => a.Id == productId).SingleOrDefault();
            if (addedProduct?.PriceSale != null)
            {
                price = addedProduct.PriceSale;
            }
            else if (addedProduct?.Price != null)
            {
                price = addedProduct.Price;
            }
            try
            {
                cart.AddToCart(productId, price, quantity);
                var data = new
                {
                    result = 1,
                    count = cart.GetCount(),
                    CartTotal = cart.GetTotal().ToString("N0"),
                };
                return Json(data);
            }
            catch
            {
                var data = new
                {
                    result = 0,
                    count = cart.GetCount()
                };
                return Json(data);
            }
        }

        [HttpPost]
        public void AddProduct(int sid = 0, int pid = 0, int quantity = 0)
        {
            var product = _unitOfWork.ProductRepository.GetById(pid);
            if (product == null) return;
            var cart = _unitOfWork.CartRepository.GetById(sid);
            if (cart == null) return;
            cart.Count = quantity;
            _unitOfWork.Save();
        }
        [HttpPost]
        public JsonResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(HttpContext);

            // Get the name of the album to display confirmation
            var productName = _unitOfWork.CartRepository.GetById(id).Product.Name;

            // Remove from cart
            var itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = productName + " đã được xóa khỏi giỏ hàng của bạn.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                Status = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        public PartialViewResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            var model = new CartSummaryViewModel
            {
                Carts = cart.GetCartItems(),
                Count = cart.GetCount(),
                TotalMoney = cart.GetTotal()
            };
            return PartialView("CartSummary", model);
        }
        [HttpPost]
        public JsonResult UpdateCartV2(int productId, int changeValue)
        {
            try
            {
                var cart = ShoppingCart.GetCart(HttpContext);
                var addedProduct = cart.GetCartItems().FirstOrDefault(a => a.RecordId == productId);
                if (addedProduct != null)
                {
                    var itemCount = cart.UpdateToCart(addedProduct, changeValue);
                    var totalMoneyItem = addedProduct.Price * itemCount;
                    var statistic = new CartStatistic
                    {
                        Status = 0,
                        Msg = "Cập nhật thành công",
                        itemCont = itemCount,
                        totalItem = cart.GetCount(),
                        totalMoneyItem = totalMoneyItem ?? 0,
                        totalMoney = cart.GetTotal(),
                    };
                    return Json(statistic);
                }
                return Json(new CartStatistic
                {
                    Status = 0,
                    totalItem = 0,
                    totalMoney = 0,
                    Msg = "Cập nhật thành công",
                });
            }
            catch (Exception)
            {
                return Json(new CartStatistic
                {
                    Status = 1,
                    totalMoney = 0,
                    itemCont = 0,
                    totalItem = 0,
                    Msg = "Cập nhật không thành công",
                });
            }
        }
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}