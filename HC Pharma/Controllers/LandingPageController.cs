using HC_Pharma.DAL;
using HC_Pharma.Filters;
using HC_Pharma.Models;
using HC_Pharma.ViewModel;
using Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HC_Pharma.Controllers
{
    [Authorize, AdminRoleFilters]
    public class LandingPageController : Controller
    {
        private RoleAdmin Role => (RoleAdmin)Enum.Parse(typeof(RoleAdmin), RouteData.Values["Role"].ToString());

        public PartialViewResult Menu(int productId)
        {
            ViewBag.ProductId = productId;
            return PartialView();
        }

        // GET: LandingPage
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        #region Feedback

        public ActionResult ListFeedback(int? page, string name, int productId = 0, string result = "")
        {
            ViewBag.Result = result;
            var pageNumber = page ?? 1;
            const int pageSize = 10;
            var product = _unitOfWork.ProductRepository.GetById(productId);
            if (product == null)
            {
                return RedirectToAction("Listproduct", "ProductVcms");
            }
            var feedback = _unitOfWork.FeedbackProductRepository.GetQuery(a => a.ProductId == product.Id, l => l.OrderBy(a => a.Sort));
            if (!string.IsNullOrEmpty(name))
            {
                feedback = feedback.Where(l => l.Name.Contains(name));
            }
            var model = new ListFeedbackProductViewModel
            {
                Feedbacks = feedback.ToPagedList(pageNumber, pageSize),
                Name = name,
                Product = product,
            };
            return View(model);
        }


        public ActionResult Feedback(int productId = 0)
        {
            var product = _unitOfWork.ProductRepository.GetById(productId);
            if (product == null)
            {
                return RedirectToAction("Listproduct", "ProductVcms");
            }
            var model = new InserFeedbackProductViewModel
            {
                Product = product,
                Feedback = new FeedbackProduct { ProductId = productId,Active = true}
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Feedback(InserFeedbackProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _unitOfWork.ProductRepository.GetById(model.Feedback.ProductId);
                if (product == null)
                {
                    return RedirectToAction("Listproduct", "ProductVcms");
                }
                var file = Request.Files["Feedback.Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                        }
                        else
                        {
                            var imgPath = "/images/feedbacks/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            model.Feedback.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            var newImage = System.Drawing.Image.FromStream(file.InputStream);
                            var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                        }
                    }
                }

                var file1 = Request.Files["Feedback.FileVideo"];
                if (file1 != null && file1.ContentLength > 0)
                {
                    if (!HtmlHelpers.CheckFileExt(file1.FileName, "mp4"))
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng mp4");
                        return View(model);
                    }
                    else
                    {
                        if (file1.ContentLength > 40000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 40MB. Hãy thử lại");
                            return View(model);
                        }
                        else
                        {
                            var imgPath = "/images/feedbacks/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file1.FileName);

                            model.Feedback.FileVideo = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;
                            file1.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }

                _unitOfWork.FeedbackProductRepository.Insert(model.Feedback);
                _unitOfWork.Save();
                return RedirectToAction("ListFeedback", new { result = "success", productId = product.Id });
            }
            return View(model);
        }
        public ActionResult UpdateFeedback(int feedbackId = 0)
        {
            var feedback = _unitOfWork.FeedbackProductRepository.GetById(feedbackId);
            if (feedback == null)
            {
                return RedirectToAction("ListFeeback");
            }
            var model = new InserFeedbackProductViewModel
            {
                Feedback = feedback
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateFeedback(InserFeedbackProductViewModel model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var product = _unitOfWork.ProductRepository.GetById(model.Feedback.ProductId);
                if (product == null)
                {
                    return RedirectToAction("Listproduct", "ProductVcms");
                }
                var file = Request.Files["Feedback.Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                        }
                        else
                        {
                            var imgPath = "/images/feedbacks/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            if (System.IO.File.Exists(Server.MapPath("/images/feedbacks/" + model.Feedback.Image)))
                            {
                                System.IO.File.Delete(Server.MapPath("/images/feedbacks/" + model.Feedback.Image));
                            }
                            model.Feedback.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            var newImage = System.Drawing.Image.FromStream(file.InputStream);
                            var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                        }
                    }
                }
                else
                {
                    model.Feedback.Image = fc["CurrentFile"] == "" ? null : fc["CurrentFile"];
                }

                var file1 = Request.Files["Feedback.FileVideo"];
                if (file1 != null && file1.ContentLength > 0)
                {
                    if (!HtmlHelpers.CheckFileExt(file1.FileName, "mp4"))
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng mp4");
                        return View(model);
                    }
                    else
                    {
                        if (file1.ContentLength > 40000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 40MB. Hãy thử lại");
                            return View(model);
                        }
                        else
                        {
                            var imgPath = "/images/feedbacks/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file1.FileName);

                            model.Feedback.FileVideo = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;
                            file1.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }
                else
                {
                    model.Feedback.FileVideo = fc["CurrentFileVideo"] == "" ? null : fc["CurrentFileVideo"];
                }

                _unitOfWork.FeedbackProductRepository.Update(model.Feedback);
                _unitOfWork.Save();
                return RedirectToAction("ListFeedback", new { result = "success", productId = product.Id });
            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteFeedback(int feedbackId = 0)
        {
            var feedback = _unitOfWork.FeedbackProductRepository.GetById(feedbackId);
            if (feedback == null)
            {
                return false;
            }
            _unitOfWork.FeedbackProductRepository.Delete(feedback);
            _unitOfWork.Save();
            return true;
        }


        #endregion

        #region LandingPage 
        public ActionResult LandingPage(int productId, string result = "")
        {
            ViewBag.Result = result;

            var config = _unitOfWork.LandingPageRepository.GetQuery(l => l.ProductId == productId).FirstOrDefault();
            if (config == null)
            {
                config = new LandingPage
                {
                    ProductId = productId,
                };
                _unitOfWork.LandingPageRepository.Insert(config);
                _unitOfWork.Save();
            }
            return View(config);
            //else
            //{

            //}
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult LandingPage(LandingPage model, FormCollection fc)
        {
            var config = _unitOfWork.LandingPageRepository.GetQuery(l => l.ProductId == model.ProductId).FirstOrDefault();

            for (var i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files[i] == null || Request.Files[i].ContentLength <= 0) continue;
                if (!HtmlHelpers.CheckFileExt(Request.Files[i].FileName, "jpg|jpeg|png|gif")) continue;
                if (Request.Files[i].ContentLength > 1024 * 1024 * 4) continue;

                var imgFileName = HtmlHelpers.ConvertToUnSign(null, Path.GetFileNameWithoutExtension(Request.Files[i].FileName)) +
                    "-" + DateTime.Now.Millisecond + Path.GetExtension(Request.Files[i].FileName);
                var imgPath = "/images/landingPage/" + DateTime.Now.ToString("yyyy/MM/dd");
                HtmlHelpers.CreateFolder(Server.MapPath(imgPath));

                var imgFile = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                var newImage = Image.FromStream(Request.Files[i].InputStream);
                var fixSizeImage = HtmlHelpers.FixedSize(newImage, 1000, 1000, false);
                HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);

                if (Request.Files.Keys[i] == "ImageAnalysis")
                {
                    config.ImageAnalysis = imgFile;
                }
                else if (Request.Files.Keys[i] == "ImageIntro")
                {
                    config.ImageIntro = imgFile;
                }
                else if (Request.Files.Keys[i] == "Imagesolution")
                {
                    config.Imagesolution = imgFile;
                }
                else if (Request.Files.Keys[i] == "Imageregistration")
                {
                    config.Imageregistration = imgFile;
                }
            }
            config.Title = model.Title;
            config.SubTitle2 = model.SubTitle2;
            config.SubTitle = model.SubTitle;
            config.Intro = model.Intro;
            config.Solution = model.Solution;
            config.ProductAnalysis = model.ProductAnalysis;
            config.CommonDiseases = model.CommonDiseases;
            config.CauseDisease = model.CauseDisease;
            config.QA = model.QA;
            _unitOfWork.Save();
            return RedirectToAction("LandingPage", new { result = "success", productId = config.ProductId });
        }
        #endregion

        #region QaProduct 

        public ActionResult QaProduct(int productId, string result = "")
        {
            ViewBag.Result = result;
            var model = new QaProduct
            {
                ProductId = productId,
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult QaProduct(QaProduct qa)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.QaProductRepository.Insert(qa);
                _unitOfWork.Save();
                return RedirectToAction("QaProduct", new { result = "success", productId = qa.ProductId });
            }
            return View(qa);
        }

        public ActionResult UpdateQaProduct(int id)
        {
            var qa = _unitOfWork.QaProductRepository.GetById(id);
            if (qa == null)
            {
                return RedirectToAction("Listproduct", "ProductVcms");
            }
            return View(qa);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateQaProduct(QaProduct qa)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.QaProductRepository.Update(qa);
                _unitOfWork.Save();
                return RedirectToAction("QaProduct", new { result = "success", productId = qa.ProductId });
            }
            return View(qa);
        }
        [ChildActionOnly]
        public ActionResult ListQaProduct(int productId)
        {
            ViewBag.ProductId = productId;
            var allcats = _unitOfWork.QaProductRepository.GetQuery(l => l.ProductId == productId, c => c.OrderBy(l => l.Sort));
            return PartialView(allcats);
        }
        [HttpPost]
        public bool DeleteQaProduct(int id)
        {
            var qa = _unitOfWork.QaProductRepository.GetById(id);
            if (qa == null)
            {
                return false;
            }
            _unitOfWork.QaProductRepository.Delete(qa);
            _unitOfWork.Save();
            return true;
        }
        #endregion

        #region Combo
        [ChildActionOnly]
        public ActionResult ListCombo(string name, int productId)
        {
          
            var combos = _unitOfWork.ComboRepository.GetQuery(l => l.ProductId == productId, c => c.OrderBy(l => l.Sort));
            if (!string.IsNullOrEmpty(name))
            {
                combos = combos.Where(l => l.Name.ToLower().Contains(name.ToLower()));
            }
            var model = new ListComboViewModel
            {
                Combos = combos,
                Name = name,
                ProductId = productId,
            };
            return PartialView(model);
        }

        public ActionResult Combo(int productId, string result = "")
        {
            ViewBag.Result = result;
            var model = new InsertComboViewModel
            {
                Combo = new Combo { ProductId = productId, Active = true },
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Combo(InsertComboViewModel model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                if (model.Price != null)
                {
                    model.Combo.Price = Convert.ToDecimal(model.Price.Replace(",", ""));
                }
                if (model.PriceSale != null)
                {
                    model.Combo.PriceSale = Convert.ToDecimal(model.PriceSale.Replace(",", ""));
                }
                if (model.Combo.Price > 0 && model.Combo.PriceSale > 0 && model.Combo.PriceSale >= model.Combo.Price)
                {
                    ModelState.AddModelError("", @"Giá khuyến mãi không hợp lệ, vui lòng kiểm tra lại.");
                    return View(model);
                }
                var file = Request.Files["Combo.Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                        return View(model);
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                            return View(model);
                        }
                        else
                        {
                            var imgPath = "/images/combos/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            //var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);
                            var imgFileName = HtmlHelpers.ConvertToUnSign(null, Path.GetFileNameWithoutExtension(file.FileName)) + "-" + DateTime.Now.Millisecond + Path.GetExtension(file.FileName);

                            model.Combo.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            var newImage = Image.FromStream(file.InputStream);
                            //var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            //HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                            file.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }

                _unitOfWork.ComboRepository.Insert(model.Combo);
                _unitOfWork.Save();
                return RedirectToAction("Combo", new { result = "update", productId = model.Combo.ProductId });
            }
            return View(model);
        }

        public ActionResult UpdateCombo(int productId, int proId = 0)
        {
            var combo = _unitOfWork.ComboRepository.GetById(proId);
            if (combo == null)
            {
                return RedirectToAction("Combo", new { productId = productId });
            }
            var model = new InsertComboViewModel
            {
                Combo = combo,
                Price = combo.Price?.ToString("N0"),
                PriceSale = combo.PriceSale?.ToString("N0"),
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateCombo(InsertComboViewModel model, FormCollection fc)
        {
            var combo = _unitOfWork.ComboRepository.GetById(model.Combo.Id);
            if (ModelState.IsValid)
            {
                if (model.Price != null)
                {
                    combo.Price = Convert.ToDecimal(model.Price.Replace(",", ""));
                }
                else
                {
                    combo.Price = null;
                }
                if (model.PriceSale != null)
                {
                    combo.PriceSale = Convert.ToDecimal(model.PriceSale.Replace(",", ""));
                }
                else
                {
                    combo.PriceSale = null;
                }
                if (combo.Price > 0 && combo.PriceSale > 0 && combo.PriceSale >= combo.Price)
                {
                    ModelState.AddModelError("", @"Giá khuyến mãi không hợp lệ, vui lòng kiểm tra lại.");
                    return View(model);
                }

                var file = Request.Files["Combo.Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (!HtmlHelpers.CheckFileExt(file.FileName, "jpg|jpeg|png|gif"))
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                        return View(model);
                    }
                    else
                    {
                        var imgFileName = HtmlHelpers.ConvertToUnSign(null, Path.GetFileNameWithoutExtension(file.FileName)) + "-" + DateTime.Now.Millisecond + Path.GetExtension(file.FileName);
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                            return View(model);
                        }
                        else
                        {
                            var imgPath = "/images/combos/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            combo.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;
                            var newImage = Image.FromStream(file.InputStream);
                            var fixSizeImage = HtmlHelpers.FixedSize(newImage, 800, 600, false);
                            HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                        }
                    }
                }
                else
                {
                    combo.Image = fc["CurrentFile"] == "" ? null : fc["CurrentFile"];
                }
                combo.Name = model.Combo.Name;
                combo.Sort = model.Combo.Sort;
                combo.Active = model.Combo.Active;
                _unitOfWork.Save();
                return RedirectToAction("Combo", new { result = "update", productId = model.Combo.ProductId });
            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteCombo(int id)
        {
            var combo = _unitOfWork.ComboRepository.GetById(id);
            if (combo == null)
            {
                return false;
            }
            _unitOfWork.ComboRepository.Delete(combo);
            _unitOfWork.Save();
            return true;
        }
        #endregion

        #region BannerLandingPage

        public ActionResult ListBanner(int? page, PostionAbout? groupId, int productId,string result ="")
        {
            var banners = _unitOfWork.BannerLandingPageRepository.GetQuery(l => l.ProductId == productId, c => c.OrderBy(l => l.PostionAbout));
            if(groupId > 0)
            {
                banners = banners.Where(a => a.PostionAbout == groupId);
            }
            var model = new ListBannerLandingPageViewModel
            {
                Banners = banners,
                GroupId = groupId,
                ProductId = productId
            };
            return View(model);
        }

        public ActionResult BannerLandingPage(int productId)
        {
            var banner = new BannerLandingPage
            {
                Active = true,
                ProductId = productId
            };
            return View(banner);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult BannerLandingPage(BannerLandingPage banner)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                        }
                        else
                        {
                            var imgPath = "/images/banners/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            banner.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            //var newImage = Image.FromStream(file.InputStream);
                            //var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            //HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                            file.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }
                _unitOfWork.BannerLandingPageRepository.Insert(banner);
                _unitOfWork.Save();
                return RedirectToAction("ListBanner", new { productId = banner.ProductId, result = "success" });
            }
            return View(banner);
        }
        public ActionResult UpdateLandingPage(int bannerId, int productId)
        {
         
            var banner = _unitOfWork.BannerLandingPageRepository.GetById(bannerId);
            if (banner == null)
            {
                return RedirectToAction("ListBanner", new { productId = productId });
            }
            return View(banner);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateLandingPage(BannerLandingPage model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var banner = _unitOfWork.BannerLandingPageRepository.GetById(model.Id);
                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                        return View(model);
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                            return View(model);
                        }
                        else
                        {
                            var imgPath = "/images/banners/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            if (System.IO.File.Exists(Server.MapPath("/images/banners/" + banner.Image)))
                            {
                                System.IO.File.Delete(Server.MapPath("/images/banners/" + banner.Image));
                            }
                            banner.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            //var newImage = Image.FromStream(file.InputStream);
                            //var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            //HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                            file.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }
                banner.Name = model.Name;
                banner.Sort = model.Sort;
                banner.Active = model.Active;
                banner.Content = model.Content;
                banner.PostionAbout = model.PostionAbout;
                //_unitOfWork.BannerLandingPageRepository.Update(banner);
                _unitOfWork.Save();
                return RedirectToAction("ListBanner", new { productId = model.ProductId, result = "update" });

            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteBanner(int bannerId = 0)
        {
            if (Role != RoleAdmin.Admin)
            {
                return false;
            }
            var banner = _unitOfWork.BannerLandingPageRepository.GetById(bannerId);
            if (banner == null)
            {
                return false;
            }
            HtmlHelpers.DeleteFile(Server.MapPath("/images/banners/" + banner.Image));
            _unitOfWork.BannerLandingPageRepository.Delete(banner);
            _unitOfWork.Save();
            return true;
        }

        public bool UpdateBannerQuick(int sort = 1, bool active = false, int bannerId = 0)
        {
            var banner = _unitOfWork.BannerLandingPageRepository.GetById(bannerId);
            if (banner == null)
            {
                return false;
            }
            banner.Sort = sort;
            banner.Active = active;

            _unitOfWork.Save();
            return true;
        }
        #endregion
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}