using HC_Pharma.DAL;
using HC_Pharma.Filters;
using HC_Pharma.Models;
using HC_Pharma.ViewModel;
using Helpers;
using PagedList;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace HC_Pharma.Controllers
{
    [Authorize, AdminRoleFilters]
    public class ContactController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private RoleAdmin Role => (RoleAdmin)Enum.Parse(typeof(RoleAdmin), RouteData.Values["Role"].ToString());

        #region Contact
        public ActionResult ListContact(int? page, string name)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 15;
            var contact = _unitOfWork.ContactRepository.GetQuery(orderBy: l => l.OrderByDescending(a => a.Id));

            if (!string.IsNullOrEmpty(name))
            {
                contact = contact.Where(l => l.FullName.Contains(name));
            }
            var model = new ListContactViewModel
            {
                Contacts = contact.ToPagedList(pageNumber, pageSize),
                Name = name
            };
            return View(model);
        }
        [HttpPost]
        public bool DeleteContact(int contactId = 0)
        {
            if (Role != RoleAdmin.Admin)
            {
                return false;
            }
            var contact = _unitOfWork.ContactRepository.GetById(contactId);
            if (contact == null)
            {
                return false;
            }
            _unitOfWork.ContactRepository.Delete(contact);
            _unitOfWork.Save();
            return true;
        }
        #endregion

        #region Feedback
        public ActionResult ListFeedback(int? page, string name, string result = "")
        {
            ViewBag.Result = result;
            var pageNumber = page ?? 1;
            const int pageSize = 10;
            var feedback = _unitOfWork.FeedbackRepository.GetQuery(orderBy: l => l.OrderBy(a => a.Sort));

            if (!string.IsNullOrEmpty(name))
            {
                feedback = feedback.Where(l => l.Name.Contains(name));
            }
            var model = new ListFeedbackViewModel
            {
                Feedbacks = feedback.ToPagedList(pageNumber, pageSize),
                Name = name
            };
            return View(model);
        }
        public ActionResult Feedback()
        {
            var model = new Feedback
            {
                Sort = 1,
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Feedback(Feedback model)
        {
            if (ModelState.IsValid)
            {
                var isPost = true;
                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                        isPost = false;
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                            isPost = false;
                        }
                        else
                        {
                            var imgPath = "/images/feedbacks/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            model.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            var newImage = Image.FromStream(file.InputStream);
                            var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                        }
                    }
                }
                if (isPost)
                {
                    _unitOfWork.FeedbackRepository.Insert(model);
                    _unitOfWork.Save();

                    return RedirectToAction("ListFeedback", new { result = "success" });
                }
            }
            return View(model);
        }
        public ActionResult UpdateFeedback(int feedbackId = 0)
        {
            var feedback = _unitOfWork.FeedbackRepository.GetById(feedbackId);
            if (feedback == null)
            {
                return RedirectToAction("ListFeedback");
            }
            return View(feedback);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateFeedback(Feedback model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var isPost = true;
                var feedback = _unitOfWork.FeedbackRepository.GetById(model.Id);

                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (!HtmlHelpers.CheckFileExt(file.FileName, "jpg|jpeg|png|gif|svg"))
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg, svg");
                        isPost = false;
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                            isPost = false;
                        }
                        else
                        {
                            var imgPath = "/images/feedbacks/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            feedback.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;
                            file.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }

                if (isPost)
                {
                    feedback.Name = model.Name;
                    feedback.Sort = model.Sort;
                    feedback.Content = model.Content;
                    feedback.Active = model.Active;


                    _unitOfWork.FeedbackRepository.Update(feedback);
                    _unitOfWork.Save();

                    return RedirectToAction("ListFeedback", new { result = "update" });
                }
            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteFeedback(int feedbackId = 0)
        {
            if (Role != RoleAdmin.Admin)
            {
                return false;
            }

            var feedback = _unitOfWork.FeedbackRepository.GetById(feedbackId);
            if (feedback == null)
            {
                return false;
            }
            _unitOfWork.FeedbackRepository.Delete(feedback);
            _unitOfWork.Save();
            return true;
        }
        #endregion

        #region Partner
        public ActionResult ListPartner(int? page, string name, string result = "")
        {
            ViewBag.Result = result;
            var pageNumber = page ?? 1;
            const int pageSize = 10;
            var partner = _unitOfWork.PartnerRepository.GetQuery(orderBy: l => l.OrderBy(a => a.Name));

            if (!string.IsNullOrEmpty(name))
            {
                partner = partner.Where(l => l.Name.Contains(name));
            }
            var model = new ListPartnerViewModel
            {
                Partners = partner.ToPagedList(pageNumber, pageSize),
                Name = name
            };
            return View(model);
        }
        public ActionResult Partner()
        {
            var model = new Partner
            {
                Sort = 1,
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Partner(Partner model)
        {
            if (ModelState.IsValid)
            {
                var isPost = true;
                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                        isPost = false;
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                            isPost = false;
                        }
                        else
                        {
                            var imgPath = "/images/partner/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            model.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            var newImage = Image.FromStream(file.InputStream);
                            var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                        }
                    }
                }
                if (isPost)
                {
                    _unitOfWork.PartnerRepository.Insert(model);
                    _unitOfWork.Save();

                    return RedirectToAction("ListPartner", new { result = "success" });
                }
            }
            return View(model);
        }
        public ActionResult UpdatePartner(int partnerId = 0)
        {
            var partner = _unitOfWork.PartnerRepository.GetById(partnerId);
            if (partner == null)
            {
                return RedirectToAction("ListPartner");
            }
            return View(partner);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartner(Partner model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var isPost = true;
                var partner = _unitOfWork.PartnerRepository.GetById(model.Id);

                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (!HtmlHelpers.CheckFileExt(file.FileName, "jpg|jpeg|png|gif|svg"))
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg, svg");
                        isPost = false;
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                            isPost = false;
                        }
                        else
                        {
                            var imgPath = "/images/partner/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            partner.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;
                            file.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }

                if (isPost)
                {
                    partner.Name = model.Name;
                    partner.Sort = model.Sort;
                    partner.Active = model.Active;


                    _unitOfWork.PartnerRepository.Update(partner);
                    _unitOfWork.Save();

                    return RedirectToAction("ListPartner", new { result = "update" });
                }
            }
            return View(model);
        }
        [HttpPost]
        public bool DeletePartner(int partnerId = 0)
        {
            if (Role != RoleAdmin.Admin)
            {
                return false;
            }
            var partner = _unitOfWork.PartnerRepository.GetById(partnerId);
            if (partner == null)
            {
                return false;
            }
            _unitOfWork.PartnerRepository.Delete(partner);
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