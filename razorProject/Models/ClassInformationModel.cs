
public class ClassInformationModel // class structure created with ChatGPT
{

    public int Id { get; set; }  
    public string ClassName { get; set; } = string.Empty;
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
