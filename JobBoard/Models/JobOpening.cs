using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace JobBoard.Models
{
  public class JobOpening
  {
    public string Title { get; set; }
    public string Description { get; set; }
    //public Dictionary <string, string> {{"Name", name}, {"PhoneNumber", phoneNumber},{"Email", email}}
    public int Salary { get; set; }
    public int Id { get; set; }


    public JobOpening(string _title, string _description, int _salary)
    {
      Title = _title;
      Description = _description;
      Salary = _salary;
    }

    public JobOpening(int id, string _title, string _description, int _salary)
    {
      Title = _title;
      Description = _description;
      Salary = _salary;
      Id = id;
    }

    public override bool Equals(System.Object otherJobOpening)
    {
      if(!(otherJobOpening is JobOpening))
      {
        return false;
      }
      else
      {
        JobOpening newJobOpening = (JobOpening) otherJobOpening;
        bool idEquality = (this.Id == newJobOpening.Id);
        bool titleEquality = (this.Title == newJobOpening.Title);
        bool descriptionEquality = (this.Description == newJobOpening.Description);
        bool salaryEquality = (this.Salary == newJobOpening.Salary);
        return (titleEquality && descriptionEquality && salaryEquality);
      }
    }

    public static List<JobOpening> GetAll()
    {
      List<JobOpening> allJobOpenings = new List<JobOpening> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM job_board;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int jobOpeningId = rdr.GetInt32(0);
        string jobOpeningTitle = rdr.GetString(1);
        string jobOpeningDescription = rdr.GetString(2);
        int jobOpeningSalary = rdr.GetInt32(3);
        JobOpening newJobOpening = new JobOpening(jobOpeningId, jobOpeningTitle, jobOpeningDescription, jobOpeningSalary);
        allJobOpenings.Add(newJobOpening);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allJobOpenings;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "DELETE FROM job_board;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static JobOpening Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM job_board WHERE id = @ThisId;";

      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ThisId";
      param.Value = id;
      cmd.Parameters.Add(param);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int jobOpeningId = 0;
      string jobOpeningTitle = "";
      string jobOpeningDescription = "";
      int jobOpeningSalary = 0;
      while (rdr.Read())
      {
        jobOpeningId = rdr.GetInt32(0);
        jobOpeningTitle = rdr.GetString(1);
        jobOpeningDescription = rdr.GetString(2);
        jobOpeningSalary = rdr.GetInt32(3);
      }
      JobOpening foundJobOpening = new JobOpening(jobOpeningId, jobOpeningTitle, jobOpeningDescription, jobOpeningSalary);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundJobOpening;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = "INSERT INTO job_board (title, description, salary) VALUES (@JobOpeningTitle, @JobOpeningDescription, @JobOpeningSalary)";
      cmd.Parameters.AddWithValue("@JobOpeningTitle", this.Title);
      cmd.Parameters.AddWithValue("@JobOpeningDescription", this.Description);
      cmd.Parameters.AddWithValue("@JobOpeningSalary", this.Salary);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    // public static void Delete(int id)
    // {
    //   _instances.RemoveAt(id-1);
    //   for (int i=id-1; i<_instances.Count; i++) {
    //     _instances[i].Id -= 1;
    //   }
    // }
  }
}