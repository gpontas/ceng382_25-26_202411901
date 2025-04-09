using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace razorProject.Pages
{
    public class IndexModel : PageModel
    {
        private static List<ClassInformationModel> _classList = new();
        private static int _nextId = 1;

        [BindProperty]
        public ClassInformationModel NewClass { get; set; } = new();

        [BindProperty]
        public ClassInformationTable ClassTable { get; set; } = new();

        // generate 100 sample records (DeepSeekLLM)
        static IndexModel()
        {
            if (_classList.Count == 0)
            {
                var rand = new Random();
                for (int i = 0; i < 100; i++)
                {
                    _classList.Add(new ClassInformationModel(
                        _nextId++,
                        $"Class {i + 1}",
                        rand.Next(1, 1000),
                        $"Description {i + 1}"
                    ));
                }
            }
        }

        public void OnGet(
            int? editId,
            string? classNameFilter,
            int? studentCountFilter,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _classList.AsQueryable();
            
            if (!string.IsNullOrEmpty(classNameFilter))
                query = query.Where(c => c.ClassName.Contains(classNameFilter));
            
            if (studentCountFilter.HasValue)
                query = query.Where(c => c.StudentCount == studentCountFilter.Value);

            ClassTable.TotalItems = query.Count();
            ClassTable.Classes = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ClassTable.CurrentPage = pageNumber;
            ClassTable.PageSize = pageSize;
            ClassTable.ClassNameFilter = classNameFilter;
            ClassTable.StudentCountFilter = studentCountFilter;

            // Edit logic
            if (editId.HasValue)
            {
                var existingItem = _classList.FirstOrDefault(c => c.Id == editId.Value);
                if (existingItem != null)
                {
                    NewClass = new ClassInformationModel
                    {
                        Id = existingItem.Id,
                        ClassName = existingItem.ClassName,
                        StudentCount = existingItem.StudentCount,
                        Description = existingItem.Description
                    };
                }
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            if (NewClass.Id == 0)
            {
                NewClass.Id = _nextId++;
                _classList.Add(new ClassInformationModel(
                    NewClass.Id,
                    NewClass.ClassName,
                    NewClass.StudentCount,
                    NewClass.Description
                ));
            }
            else
            {
                var existingItem = _classList.FirstOrDefault(c => c.Id == NewClass.Id);
                if (existingItem != null)
                {
                    existingItem.ClassName = NewClass.ClassName;
                    existingItem.StudentCount = NewClass.StudentCount;
                    existingItem.Description = NewClass.Description;
                }
            }

            return RedirectToPage(new 
            {
                pageNumber = ClassTable.CurrentPage,
                pageSize = ClassTable.PageSize,
                classNameFilter = ClassTable.ClassNameFilter,
                studentCountFilter = ClassTable.StudentCountFilter
            });
        }

        public IActionResult OnPostDelete(int id)
        {
            var item = _classList.FirstOrDefault(c => c.Id == id);
            if (item != null) _classList.Remove(item);
            
            return RedirectToPage(new 
            {
                pageNumber = ClassTable.CurrentPage,
                pageSize = ClassTable.PageSize,
                classNameFilter = ClassTable.ClassNameFilter,
                studentCountFilter = ClassTable.StudentCountFilter
            });
        }
    }
}