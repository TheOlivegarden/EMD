using EMD.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMD.Web.Pages
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class IndexModel : PageModel
    {
        private readonly EMDDbContext _eMDDbContext;

        public IndexModel(EMDDbContext context)
        {
            _eMDDbContext = context;
        }

        public JsonResult OnGetEmployeeData()
        {
            var startDate = DateTime.Now.Date.AddDays(-7);
            var endDate = DateTime.Now.Date;

            var employeeCountData = new List<int>();
            var dates = new List<string>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var count = _eMDDbContext.Emds.Count(e => e.DateCreated.Date == date);
                employeeCountData.Add(count);
                dates.Add(date.ToString("yyyy-MM-dd"));
            }

            return new JsonResult(new { employeeCount = employeeCountData, dates });
        }
    }
}