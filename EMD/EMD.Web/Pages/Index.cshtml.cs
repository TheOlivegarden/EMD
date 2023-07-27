using EMD.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMD.Web.Pages
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class IndexModel : PageModel
    {
        private readonly EMDDbContext _eMDDbContext;

        [TempData]
        public string UpdatedUsername { get; set; }

        public IndexModel(EMDDbContext context)
        {
            _eMDDbContext = context;
        }

        public JsonResult OnGetEmployeeData()
        {
            var startDate = DateTime.Now.Date.AddDays(-14);
            var endDate = DateTime.Now.Date;

            var employeeCountData = new List<int>();
            var dates = new List<string>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var count = _eMDDbContext.Emds.Count(e => e.DateCreated.Date == date);
                employeeCountData.Add(count);
                dates.Add(date.ToString("yyyy-MM-dd"));
            }

            var departments = _eMDDbContext.Emds
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count()
                })
                .ToList();

            var departmentNames = departments.Select(d => d.Department).ToList();
            var departmentCounts = departments.Select(d => d.Count).ToList();

            return new JsonResult(new { employeeCount = employeeCountData, dates, departmentNames, departmentCounts });
        }
    }
}