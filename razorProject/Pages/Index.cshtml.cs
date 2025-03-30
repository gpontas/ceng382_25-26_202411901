using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razorProject.Pages
{
    public class IndexModel : PageModel
    {
        private static List<ClassInformationModel> _classList = new();
        private static int _nextId = 1;

        [BindProperty]
        public ClassInformationModel NewClass { get; set; } = new();

        public List<ClassInformationModel> ClassList { get; private set; } = new();

        public void OnGet(int? editId)
        {
            ClassList = _classList;
            if (editId.HasValue)
            {
                var existingItem = _classList.FirstOrDefault(c => c.Id == editId.Value);
                if (existingItem != null)
                {
                    NewClass = new ClassInformationModel {
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
                    NewClass.Description));
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
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var item = _classList.FirstOrDefault(c => c.Id == id);
            if (item != null) _classList.Remove(item);
            return RedirectToPage();
        }
    }
}