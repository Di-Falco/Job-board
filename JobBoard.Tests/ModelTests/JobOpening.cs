using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobBoard.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace JobBoard.Tests
{
  [TestClass]
  public class JobOpeningTests : IDisposable
  {
    public void Dispose()
    {
      JobOpening.ClearAll();
    }

    public JobOpeningTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=job_board_tests;";
    }

    [TestMethod]
    public void GetAll_ReturnEmptyListFromDatabase_JobOpeningList()
    {
      List<JobOpening> newList = new List<JobOpening> {};
      List<JobOpening> result = JobOpening.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfJobOpeningAreTheSame_JobOpening()
    {
      // Arrange, Act
      JobOpening newJobOpening1 = new JobOpening("Manager", "jobOpeningDescription", 8000);
      JobOpening newJobOpening2 = new JobOpening("Manager", "jobOpeningDescription", 8000);

      // Assert
      Assert.AreEqual(newJobOpening1, newJobOpening2);
    }

    [TestMethod]
    public void Save_SavesToDatabase_JobOpeningList()
    {
      JobOpening testJobOpening = new JobOpening("Manager", "jobOpeningDescription", 8000);

      testJobOpening.Save();
      List<JobOpening> result = JobOpening.GetAll();
      List<JobOpening> testlist = new List<JobOpening>{testJobOpening};
      CollectionAssert.AreEqual(testlist, result);
    }


    [TestMethod]
    public void GetAll_ReturnsJobOpeningList_JobOpeningList()
    {
      JobOpening newJobOpening1 = new JobOpening("Manager", "jobOpeningDescription", 8000);
      newJobOpening1.Save();
      JobOpening newJobOpening2 = new JobOpening("Manager", "jobOpeningDescription", 8000);
      newJobOpening2.Save();
      List<JobOpening> newList = new List<JobOpening> { newJobOpening1, newJobOpening2 };
      List<JobOpening> result = JobOpening.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectJobOpeningFromDatabase_JobOpening()
    {
      //Arrange
      JobOpening newJobOpening1 = new JobOpening("Manager", "jobOpeningDescription", 8000);
      newJobOpening1.Save();
      JobOpening newJobOpening2 = new JobOpening("Manager", "jobOpeningDescription", 8000);
      newJobOpening2.Save();

      //Act
      JobOpening foundJobOpening = JobOpening.Find(newJobOpening1.Id);
      //Assert
      Assert.AreEqual(newJobOpening1, foundJobOpening);
    }

//     [TestMethod]
//     public void GetDescription_ReturnsDescription_String()
//     {
//       string description = "Walk the dog.";
//       Item newItem = new Item(description);
//       string result = newItem.Description;
//       Assert.AreEqual(description, result);
//     }

//     [TestMethod]
// public void GetDescription_ReturnsDescription_String()
// {
//   //Arrange
//   string description = "Walk the dog.";
//   Item newItem = new Item(description);

//   //Act
//   string result = newItem.Description;

//   //Assert
//   Assert.AreEqual(description, result);
// }

  }
}