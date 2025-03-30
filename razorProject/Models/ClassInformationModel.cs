
using System.ComponentModel.DataAnnotations;

public class ClassInformationModel // class structure created with ChatGPT
{

    public int Id { get; set; } 

    
    [Required(ErrorMessage = "Class name is required")] 
    public string ClassName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Student count is required")]
    [Range(1, 1000, ErrorMessage = "Student count must be between 1-1000")]
    public int StudentCount { get; set; }
    public string Description { get; set; } = string.Empty;

    public ClassInformationModel() { 

    }

    public ClassInformationModel(int id, string className, int studentCount, string description) 
    {
        Id = id;
        ClassName = className;
        StudentCount = studentCount;
        Description = description;
    }
}
