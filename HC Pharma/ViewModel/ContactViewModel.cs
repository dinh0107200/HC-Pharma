using System.Collections.Generic;
using HC_Pharma.Models;

namespace HC_Pharma.ViewModel
{
    public class ListContactViewModel
    {
        public PagedList.IPagedList<Contact> Contacts { get; set; }
        public string Name { get; set; }
    }

   
    public class ListFeedbackViewModel
    {
        public PagedList.IPagedList<Feedback> Feedbacks { get; set; }
        public string Name { get; set; }
    }


    public class ListPartnerViewModel
    {
        public PagedList.IPagedList<Partner> Partners { get; set; }
        public string Name { get; set; }
    }
}