using Microsoft.AspNetCore.Mvc;
using JobBoard.Models;
using System.Collections.Generic;

namespace JobBoard.Controllers
{
  public class JobOpeningsController : Controller
  {
    [HttpGet("/jobopenings")]
    public ActionResult Index()
    {
      List<JobOpening> allJobs = JobOpening.GetAll();
      return View(allJobs);
    }

    [HttpGet("jobopenings/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/jobopenings")]
    public ActionResult Create(string title, string description, int salary)
    {
      JobOpening newJob = new JobOpening(title, description, salary);
      newJob.Save();
      return RedirectToAction("Index");
    }

    [HttpPost("/jobopenings/deleteall")]
    public ActionResult DeleteAll()
    {
      JobOpening.ClearAll();
      return View();
    }

    [HttpGet("/jobopenings/{id}")]
    public ActionResult Show(int id)
    {
      JobOpening foundJob = JobOpening.Find(id);
      return View(foundJob);
    }

    // [HttpPost("/jobopenings/delete/{id}")]
    // public ActionResult Delete(int id)
    // {
    //   JobOpening.Delete(id);
    //   return View("Delete");
    // }
  }
}