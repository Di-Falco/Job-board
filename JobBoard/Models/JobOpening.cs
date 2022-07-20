using System.Collections.Generic;

namespace JobBoard.Models
{
  public class JobOpening
  {
    public string Title { get; set; }
    public string Description { get; set; }
    //public Dictionary <string, string> {{"Name", name}, {"PhoneNumber", phoneNumber},{"Email", email}}
    public string Salary { get; set; }
    public int Id { get; set; }
    private static List<JobOpening> _instances = new List<JobOpening> { };

    public JobOpening(string _title, string _description, string _salary)
    {
      Title = _title;
      Description = _description;
      Salary = _salary;
      _instances.Add(this);
      Id = _instances.Count;
    }

    public static List<JobOpening> GetAll()
    {
      return _instances;
    }

    public static void ClearAll()
    {
      _instances.Clear();
    }

    public static JobOpening Find(int searchId)
    {
      return _instances[searchId-1];
    }

    public static void Delete(int id)
    {
      int deleteId = id-1;
      _instances.RemoveAt(deleteId);
      for (int i=deleteId; i<_instances.Count; i++) {
        _instances[i].Id -= 1;
      }
    }
  }
}