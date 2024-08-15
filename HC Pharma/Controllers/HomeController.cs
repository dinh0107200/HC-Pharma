using HC_Pharma.DAL;
using HC_Pharma.Models;
using HC_Pharma.ViewModel;
using Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace HC_Pharma.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public ConfigSite ConfigSite => (ConfigSite)HttpContext.Application["ConfigSite"];
        private static string Smtp => WebConfigurationManager.AppSettings["smtp"];
        private static string Email => WebConfigurationManager.AppSettings["email"];
        private static string Password => WebConfigurationManager.AppSettings["password"];
        private static int SmtpPort => Convert.ToInt32(WebConfigurationManager.AppSettings["smtpport"]);

        private IEnumerable<ProductCategory> ProductCategories => _unitOfWork.ProductCategoryRepository.Get(a => a.CategoryActive, q => q.OrderBy(a => a.CategorySort));
        private IEnumerable<ArticleCategory> ArticleCategories => _unitOfWork.ArticleCategoryRepository.Get(a => a.CategoryActive, q => q.OrderBy(a => a.CategorySort));

        #region Home
        public PartialViewResult Header()
        {

            var model = new HeaderViewModel
            {
                ProductCategories = ProductCategories.Where(a => a.ShowMenu),
                FilterCategories = ProductCategories,
                IntroduceCat = ArticleCategories.Where(a => a.ShowMenu && a.TypePost == TypePost.Introduce),
                ArticleCategories = ArticleCategories.Where(a => a.ShowMenu && a.TypePost == TypePost.Article),

            };
            return PartialView(model);
        }

        public PartialViewResult Footer()
        {
            var model = new FooterViewModel
            {
                Introduct = ArticleCategories.Where(a => a.ShowFooter && a.TypePost == TypePost.Introduce),
                Policy = ArticleCategories.Where(a => a.ShowFooter && a.TypePost == TypePost.Policy),
                ArticleCat = ArticleCategories.Where(a => a.ShowFooter && a.TypePost == TypePost.Article),

            };
            return PartialView(model);
        }
        public ActionResult Index()
        {
            var category = _unitOfWork.ProductCategoryRepository.Get(a => a.CategoryActive && a.ShowHome, o => o.OrderByDescending(a => a.CategorySort), 5);
            var product = _unitOfWork.ProductRepository.Get(a => a.Active && a.Home, o => o.OrderBy(a => a.Sort), 3);
            var artilce = _unitOfWork.ArticleRepository.Get(a => a.Active && a.Home, o => o.OrderByDescending(a => a.CreateDate), 3);
            var banner = _unitOfWork.BannerRepository.Get(a => a.Active, q => q.OrderBy(a => a.Sort));
            var feedback = _unitOfWork.FeedbackRepository.Get(a => a.Active);
            //var partner = _unitOfWork.PartnerRepository.Get(a => a.Active);
            var intro = _unitOfWork._IntroductRepository.Get(a => a.Active, o => o.OrderBy(a => a.Sort), 3);
            var model = new HomeViewModel
            {
                Categories = category,
                Products = product,
                Articles = artilce,
                Banner = banner,
                Feedbacks = feedback,
                //Partners = partner,
                Introducts = intro,
            };
            return View(model);
        }
        #endregion

        #region Product
        [Route("san-pham")]
        public ActionResult AllProduct(int? page, int catId = 0)
        {
            var pageNumber = page ?? 1;
            var products = _unitOfWork.ProductRepository.GetQuery(p => p.Active, o => o.OrderByDescending(p => p.CreateDate));
            var model = new CategoryProductViewModel
            {
                Products = products.ToPagedList(pageNumber, 9),
                ProductResult = products.Count(),
                Categories = ProductCategories,
                CatId = catId,
            };
            return View(model);
        }
        public PartialViewResult GetProduct(int? page, int catId = 0)
        {
            var pageNumber = page ?? 1;
            var pageSize = 9;
            var products = _unitOfWork.ProductRepository.GetQuery(l => l.Active, c => c.OrderByDescending(a => a.CreateDate)).AsNoTracking();
            if (catId > 0)
            {
                products = products.Where(a => a.ProductCategoryId == catId);
            }
            var model = new CategoryProductViewModel
            {
                Products = products.ToPagedList(pageNumber, pageSize),
                ProductResult = products.Count(),
                CatId = catId,
                BeginCount = (pageNumber - 1) * pageSize + 1,
                EndCount = pageNumber * pageSize,
            };
            return PartialView(model);
        }
        [Route("{url:regex(^(?!.*(vcms|uploader|article|banner|contact|productvcms)).*$)}", Order = 2)]
        public ActionResult ProductCategory(int? page, string url, int catId = 0)
        {
            var pageNumber = page ?? 1;
            var category = ProductCategories.FirstOrDefault(a => a.Url == url);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var products = _unitOfWork.ProductRepository.GetQuery(
                p => p.Active && (p.ProductCategoryId == category.Id || p.ProductCategory.ParentId == category.Id),
                c => c.OrderByDescending(p => p.CreateDate));
            var model = new CategoryProductViewModel
            {
                Products = products.ToPagedList(pageNumber, 9),
                Category = category,
                CatId = catId,
                Categories = ProductCategories,
                Url = url,
            };
            return View(model);
        }
        public PartialViewResult GetProductCategory(string url, int? page, int catId = 0)
        {
            var pageNumber = page ?? 1;
            var pageSize = 9;
            var category = ProductCategories.FirstOrDefault(a => a.Url == url);

            if (category == null)
            {
                category = ProductCategories.FirstOrDefault(a => a.Id == catId);
            }
            var products = _unitOfWork.ProductRepository.GetQuery(
                p => p.Active && (p.ProductCategoryId == category.Id || p.ProductCategory.ParentId == category.Id),
                c => c.OrderByDescending(p => p.CreateDate));
            var model = new CategoryProductViewModel
            {
                Products = products.ToPagedList(pageNumber, pageSize),
                Category = category,
                ProductResult = products.Count(),
                Url = url,
                CatId = catId,
                BeginCount = (pageNumber - 1) * pageSize + 1,
                EndCount = pageNumber * pageSize,
            };
            return PartialView(model);
        }
        [Route("{url}.html", Order = 1)]
        public ActionResult ProductDetail(string url)
        {
            var newproduct = _unitOfWork.ProductRepository.GetQuery(
                p => p.Active, c => c.OrderByDescending(p => p.CreateDate), 4);
            var product = _unitOfWork.ProductRepository.GetQuery(p => p.Url == url).FirstOrDefault();
            var products = _unitOfWork.ProductRepository.GetQuery(
                    p => p.Id != product.Id && p.Active && (p.ProductCategoryId == product.ProductCategoryId || p.ProductCategory.ParentId == product.ProductCategoryId),
                    o => o.OrderByDescending(p => Guid.NewGuid()), 4);
            var review = _unitOfWork.ReviewRepository.GetQuery(a => a.Active);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            var model = new ProductDetailViewModel
            {
                Reviews = review,
                Product = product,
                Products = products,
                Categories = ProductCategories,
                NewProduct = newproduct,
            };
            return View(model);
        }
        [ChildActionOnly]
        public PartialViewResult ReviewForm(string url)
        {
            var product = _unitOfWork.ProductRepository.GetQuery(a => a.Url == url).FirstOrDefault();
            var model = new ReviewFormViewModel
            {
                Product = product,
            };
            return PartialView(model);
            //return PartialView();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult ReviewForm(ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, msg = "Hãy điền đúng định dạng." });
            }
            _unitOfWork.ReviewRepository.Insert(model.Review);
            _unitOfWork.Save();
            return Json(new { status = true, msg = "Đánh giá của bạn sẽ hiển thị khi được phê duyệt" });
        }
        [Route("tim-san-pham")]
        public ActionResult SearchProduct(int? page, string keywords, int catId = 0)
        {
            var pageNumber = page ?? 1;
            var products = _unitOfWork.ProductRepository.GetQuery(p => p.Active && p.Name.Contains(keywords),
                            o => o.OrderByDescending(p => p.CreateDate));
            if (catId > 0)
            {
                products = products.Where(p => p.ProductCategoryId == catId);
            }


            if (string.IsNullOrEmpty(keywords))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new SearchProductViewModel
            {
                Categories = ProductCategories,
                Products = products.ToPagedList(pageNumber, 12),
                Keywords = keywords,
                CatId = catId,

            };
            return View(model);
        }
        public PartialViewResult GetSearchProduct(string keywords, int? page, int catId = 0)
        {
            var pageNumber = page ?? 1;
            var pageSize = 9;
            var products = _unitOfWork.ProductRepository.GetQuery(p => p.Active && p.Name.Contains(keywords),
                            o => o.OrderByDescending(p => p.CreateDate));
            if (catId > 0)
            {
                products = products.Where(p => p.ProductCategoryId == catId);
            }

            var model = new SearchProductViewModel
            {
                Categories = ProductCategories,
                Products = products.ToPagedList(pageNumber, pageSize),
                Keywords = keywords,
                CatId = catId,
                ProductResult = products.Count(),
                BeginCount = (pageNumber - 1) * pageSize + 1,
                EndCount = pageNumber * pageSize,
            };
            return PartialView(model);
        }
        #endregion

        #region Article
        [Route("blogs")]
        public ActionResult AllArticle(int? page)
        {
            var pageNumber = page ?? 1;
            var article = _unitOfWork.ArticleRepository.GetQuery(a => a.Active, o => o.OrderByDescending(a => a.CreateDate));
            var model = new AllArticleViewModel
            {
                Articles = article.ToPagedList(pageNumber, 12),
                Categories = ArticleCategories.Where(a => a.TypePost == TypePost.Article || a.TypePost == TypePost.Introduce),
            };
            return View(model);
        }
        [Route("blog/{url}.html", Order = 1)]
        public ActionResult ArticleDetail(string url)
        {
            var article = _unitOfWork.ArticleRepository.GetQuery(a => a.Url == url && a.Active).FirstOrDefault();
            if (article == null)
            {
                return RedirectToAction("Index");
            }

            var model = new ArticleDetailsViewModel
            {
                Article = article,
                Articles = _unitOfWork.ArticleRepository.GetQuery(p =>
                p.Active && (p.ArticleCategoryId == article.ArticleCategoryId && p.Id != article.Id), q => q.OrderByDescending(a => a.CreateDate), 6),
            };
            return View(model);
        }
        [ChildActionOnly]
        public PartialViewResult MenuArticle(int rootId = 0, int catId = 0)
        {
            var model = new MenuArticleViewModel
            {
                RootId = rootId,
                CatId = catId,
                ArticleCategories = ArticleCategories,
                Articles = _unitOfWork.ArticleRepository.GetQuery(l => l.Active, a => a.OrderByDescending(c => c.CreateDate), 5)
            };
            return PartialView(model);
        }
        [Route("blog/{url:regex(^(?!.*(vcms|uploader|article|banner|contact|product)).*$)}", Order = 2)]
        public ActionResult ArticleCategory(string url, int? page)
        {
            var category = _unitOfWork.ArticleCategoryRepository.GetQuery(a => a.CategoryActive && a.Url == url).FirstOrDefault();
            if (category == null)
            {
                return RedirectToAction("Index");
            }

            var articles = _unitOfWork.ArticleRepository.GetQuery(
                a => a.Active && (a.ArticleCategoryId == category.Id || a.ArticleCategory.ParentId == category.Id),
                q => q.OrderByDescending(a => a.CreateDate));
            var pageNumber = page ?? 1;

            if (articles.Count() == 1)
            {
                var fi = articles.First();
                return RedirectToAction("ArticleDetail", new { url = fi.Url });
            }
            var model = new ArticleCategoryViewModel
            {
                Category = category,
                Articles = articles.ToPagedList(pageNumber, 12),
                Categories = ArticleCategories.Where(a => a.TypePost == TypePost.Article || a.TypePost == TypePost.Introduce),
            };

            if (category.ParentId != null)
            {
                model.RootCategory = _unitOfWork.ArticleCategoryRepository.GetById(category.ParentId);
            }
            return View(model);
        }
        #endregion

        [Route("lien-he")]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult ContactForm(Contact model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, msg = "Hãy điền đúng định dạng." });
            }
            _unitOfWork.ContactRepository.Insert(model);
            _unitOfWork.Save();
            var subject = "Email liên hệ từ website: " + Request.Url?.Host;
            var body = $"<p>Tên người liên hệ: {model.FullName},</p>" +
                       $"<p>Số điện thoại: {model.Mobile},</p>" +
                       $"<p>Email: {model.Email},</p>" +
                       $"<p>Nội dung: {model.Body}</p>" +
                       $"<p>Đây là hệ thống gửi email tự động, vui lòng không phản hồi lại email này.</p>";
            //Task.Run(() => HtmlHelpers.SendEmail(Smtp, subject, body, "anhquang.tran@vico.vn", Email, Email, Password, "HCPHARMA.VN", port: SmtpPort));

            MailMessage mail = new MailMessage();
            mail.To.Add("anhquang.tran@vico.vn");
            mail.From = new MailAddress(Email);
            mail.Subject = "Subject: Test Mail";
            mail.Body = body;
            mail.IsBodyHtml = true;

            //Added this line here
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
            SmtpClient smtp = new SmtpClient();

            smtp.Host = Smtp;
            smtp.Credentials = new System.Net.NetworkCredential(Email, Password);
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(mail);

            return Json(new { status = true, msg = "Gửi liên hệ thành công.\nChúng tôi sẽ liên lạc với bạn sớm nhất có thể." });
        }
        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }
        //public static bool RemoteServerCertificateValidationCallback(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        //{
        //    if (sslPolicyErrors == SslPolicyErrors.None)
        //        return true;

        //    // if got an cert auth error
        //    if (sslPolicyErrors != SslPolicyErrors.RemoteCertificateNameMismatch) return false;
        //    const string sertFileName = "smpthost.cer";

        //    // check if cert file exists
        //    if (System.IO.File.Exists(sertFileName))
        //    {
        //        var actualCertificate = X509Certificate.CreateFromCertFile(sertFileName);
        //        return certificate.Equals(actualCertificate);
        //    }

        //    // export and check if cert not exists
        //    using (var file = System.IO.File.Create(sertFileName))
        //    {
        //        var cert = certificate.Export(X509ContentType.Cert);
        //        file.Write(cert, 0, cert.Length);
        //    }
        //    var createdCertificate = X509Certificate.CreateFromCertFile(sertFileName);
        //    return certificate.Equals(createdCertificate);
        //}

        [Route("landing-page/{url}")]
        public ActionResult LandingPage(string url)
        {
            var product = _unitOfWork.ProductRepository.GetQuery(a => a.Active && a.Url == url).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            var landingPage = _unitOfWork.LandingPageRepository.GetQuery(l => l.ProductId == product.Id).FirstOrDefault();
            if (landingPage == null)
            {
                return RedirectToAction("Index");
            }
            var model = new LandingPageViewModel
            {
                Policy = _unitOfWork.BannerRepository.GetQuery(l => l.Active && l.GroupId == 2, c => c.OrderBy(l => l.Sort)),
                Banners = _unitOfWork.BannerLandingPageRepository.GetQuery(l => l.ProductId == product.Id, c => c.OrderBy(l => l.Sort)),
                Feedbacks = _unitOfWork.FeedbackProductRepository.GetQuery(l => l.ProductId == product.Id, c => c.OrderBy(l => l.Sort)),
                Combos = _unitOfWork.ComboRepository.GetQuery(l => l.ProductId == product.Id, c => c.OrderBy(l => l.Sort)),
                QaProducts = _unitOfWork.QaProductRepository.GetQuery(l => l.ProductId == product.Id, c => c.OrderBy(l => l.Sort)),
                LandingPage = landingPage,
                Product = product,
            };
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult ContactProduct1(ContactProduct model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, msg = "Hãy điền đúng định dạng." });
            }
            model.IpAddress = Request.UserHostAddress;
            var currentDate = DateTime.Now;
            var contacts = _unitOfWork.ContactProductRepository.GetQuery(l => l.IpAddress == model.IpAddress && DbFunctions.TruncateTime(l.CreateDate) == DbFunctions.TruncateTime(currentDate)).Count();
            if (contacts >= 10)
            {
                return Json(new { status = false, msg = "Mỗi ngày chử được gửi tối đa 10 yêu cầu tư vấn" });
            }
            _unitOfWork.ContactProductRepository.Insert(model);
            _unitOfWork.Save();
            var product = _unitOfWork.ProductRepository.GetById(model.ProductId);
            var subject = "Email liên hệ từ website: " + Request.Url?.Host;
            var body = $"<p>Tên người liên hệ: {model.Fullname},</p>" +
                        $"<p>Số điện thoại: {model.Mobile},</p>" +
                        $"<p>Nội dung:Tư vấn sản phẩm {product.Name}</p>" +
                        $"<p>Đây là hệ thống gửi email tự động, vui lòng không phản hồi lại email này.</p>";
            Task.Run(() => HtmlHelpers.SendEmail("gmail", subject, body, ConfigSite.Email, Email, Email, Password, "HC Pharma"));
            return Json(new { status = true, msg = "Gửi liên hệ thành công.\nChúng tôi sẽ liên lạc lại với bạn sớm nhất có thể." });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult ContactProduct2(ContactProduct model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, msg = "Hãy điền đúng định dạng." });
            }
            model.IpAddress = Request.UserHostAddress;
            var currentDate = DateTime.Now;
            var contacts = _unitOfWork.ContactProductRepository.GetQuery(l => l.IpAddress == model.IpAddress && DbFunctions.TruncateTime(l.CreateDate) == DbFunctions.TruncateTime(currentDate)).Count();
            if (contacts >= 10)
            {
                return Json(new { status = false, msg = "Mỗi ngày chử được gửi tối đa 10 yêu cầu tư vấn" });
            }
            _unitOfWork.ContactProductRepository.Insert(model);
            _unitOfWork.Save();
            var product = _unitOfWork.ProductRepository.GetById(model.ProductId);
            var subject = "Email liên hệ từ website: " + Request.Url?.Host;
            var body = $"<p>Tên người liên hệ: {model.Fullname},</p>" +
                        $"<p>Email liên hệ: {model.Email},</p>" +
                        $"<p>Số điện thoại: {model.Mobile},</p>" +
                          $"<p>Địa chỉ: {model.Address},</p>" +
                           $"<p>Nội dung:Tư vấn sản phẩm {product.Name}</p>" +
                           $"<p>Nhu cầu:{model.ContactNeeds}</p>" +
                           $"<p>Nội dung:{model.Body}</p>" +
                        $"<p>Đây là hệ thống gửi email tự động, vui lòng không phản hồi lại email này.</p>";
            Task.Run(() => HtmlHelpers.SendEmail("gmail", subject, body, ConfigSite.Email, Email, Email, Password, "HC Pharma"));
            return Json(new { status = true, msg = "Gửi liên hệ thành công.\nChúng tôi sẽ liên lạc lại với bạn sớm nhất có thể." });
        }
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}