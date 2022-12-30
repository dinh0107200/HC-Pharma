using HC_Pharma.DAL;
using HC_Pharma.Models;
using HC_Pharma.ViewModel;
using Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace HC_Pharma.Controllers
{
    [Authorize]

    public class IntroductController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}