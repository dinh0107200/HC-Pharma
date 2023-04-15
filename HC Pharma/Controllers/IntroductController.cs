using HC_Pharma.DAL;
using HC_Pharma.Filters;
using System.Web.Mvc;
namespace HC_Pharma.Controllers
{
    [Authorize, AdminRoleFilters]

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