using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razorProject.Pages // scructure for add / delete created using ChatGPT assistance
{
    public class IndexModel : PageModel
    {
        private static List<ClassInformationModel> _classList = new(); // In-memory storage

        private static int _nextId = 1;

        [BindProperty]
        public ClassInformationModel NewClass { get; set; } = new();

        public List<ClassInformationModel> ClassList { get; private set; } = new();

        public void OnGet()
        {
            ClassList = _classList;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                NewClass.Id = _nextId++;
                _classList.Add(new ClassInformationModel(
                    NewClass.Id,
                    NewClass.ClassName, 
                    NewClass.StudentCount, 
                    NewClass.Description));
            }
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var item = _classList.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                _classList.Remove(item);
            }
            return RedirectToPage();
        }
    }
}