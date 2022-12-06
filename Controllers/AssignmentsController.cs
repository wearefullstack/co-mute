using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Co_Mute.Data;
using Co_Mute.Models;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using Microsoft.Net.Http.Headers;

namespace Co_Mute.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AssignmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult MyAssignments()
        {
            return View();
        }
        public IActionResult GetAllMyAssignmentMarks()
        {
            return View();
        }

       /* public async Task<IActionResult> UpcomingAssignments()
        {
            var parent = await _userManager.GetUserAsync(User);
            var assignments = await (
                from i in _context.Invites
                join u in _context.Users
                    on i.InviteFrom equals u.Id
                join mc in _context.MyCourses
                on i.InviteFrom equals mc.StudentId
                join a in _context.Assignments
                    on mc.CourseId equals a.CourseId
                join c in _context.Course
                on mc.CourseId equals c.Id
                where a.ExpireDate >= DateTime.Now && i.Status == true && i.InviteTo == parent.Id
                select new
                {
                    Id = a.Id,
                    Name = a.Name,
                    Date = a.ExpireDate.ToString("d"),
                    ChildName = u.FirstName + " " + u.LastName,
                    Subject = c.CourseName

                }).ToListAsync();

            return Json(assignments);
        }
        public async Task<IActionResult> Export()
        {

            var parent = await _userManager.GetUserAsync(User);

            var assignments = await (
                from i in _context.Invites
                join u in _context.Users
                    on i.InviteFrom equals u.Id
                join mc in _context.MyCourses
                    on i.InviteFrom equals mc.StudentId
                join a in _context.Assignments
                    on mc.CourseId equals a.CourseId
                join c in _context.Course
                    on mc.CourseId equals c.Id
                join ar in _context.AssignmentResults
                    on a.Id equals ar.AssignmentId
                where a.ExpireDate >= DateTime.Now && i.Status == true && i.InviteTo == parent.Id && ar.StudentId == i.InviteFrom
                select new
                {
                    Id = a.Id,
                    Name = a.Name,
                    Date = a.ExpireDate.ToString("d"),
                    ChildName = u.FirstName + " " + u.LastName,
                    Subject = c.CourseName,
                    MarkObtained = ar.NewMark,
                    AssignmentMark = a.Mark,
                    Outcome = ((ar.NewMark / a.Mark) * 100) <= 49 ? "FAIL" : "PASS",
                    Percentage = ((ar.NewMark / a.Mark) * 100) + " %",


                }).OrderBy(x => x.ChildName).ToListAsync();
            try
            {
                var stream = new MemoryStream();

                using (var package = new ExcelPackage())
                {
                    var sheet = package.Workbook.Worksheets.Add("All Results");

                    sheet.Cells["A1"].Value = "Child Name";
                    sheet.Cells["B1"].Value = "Date";
                    sheet.Cells["C1"].Value = "Subject";
                    sheet.Cells["D1"].Value = "Assignment";
                    sheet.Cells["E1"].Value = "Mark Obtained";
                    sheet.Cells["F1"].Value = "Assignment Mark";
                    sheet.Cells["G1"].Value = "Percentage";
                    sheet.Cells["H1"].Value = "Outcome";


                    for (var i = 0; i < assignments.Count; i++)
                    {
                        sheet.Cells[$"A{i + 2}"].Value = assignments[i].ChildName;
                        sheet.Cells[$"B{i + 2}"].Value = assignments[i].Date;
                        sheet.Cells[$"C{i + 2}"].Value = assignments[i].Subject;
                        sheet.Cells[$"D{i + 2}"].Value = assignments[i].Name;
                        sheet.Cells[$"E{i + 2}"].Value = assignments[i].MarkObtained;
                        sheet.Cells[$"F{i + 2}"].Value = assignments[i].AssignmentMark;
                        sheet.Cells[$"G{i + 2}"].Value = assignments[i].Percentage;
                        sheet.Cells[$"H{i + 2}"].Value = assignments[i].Outcome;

                    }

                    var range = sheet.Cells[1, 1, assignments.Count + 1, 6];

                    var table = sheet.Tables.Add(range, "Children");

                    table.TableStyle = TableStyles.Light2;

                    sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                    await package.SaveAsAsync(stream);
                }

                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"All Results - {DateTime.Now.ToShortDateString()}.xlsx");

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }
        public async Task<IActionResult> ResultChildrenAssignments()
        {
            var parent = await _userManager.GetUserAsync(User);
            var assignments = await (
                from i in _context.Invites
                join u in _context.Users
                    on i.InviteFrom equals u.Id
                join mc in _context.MyCourses
                    on i.InviteFrom equals mc.StudentId
                join a in _context.Assignments
                    on mc.CourseId equals a.CourseId
                join c in _context.Course
                    on mc.CourseId equals c.Id
                join ar in _context.AssignmentResults
                on a.Id equals ar.AssignmentId
                where i.Status == true && i.InviteTo == parent.Id && ar.StudentId == i.InviteFrom
                select new
                {
                    Id = a.Id,
                    Name = a.Name,
                    Date = a.ExpireDate.ToString("d"),
                    ChildName = u.FirstName + " " + u.LastName,
                    Subject = c.CourseName,
                    MarkObtained = ar.NewMark,
                    AssignmentMark = a.Mark,
                    Outcome = ((ar.NewMark / a.Mark) * 100) <= 49 ? "<label style=\"font-size: 14px; \" class=\"label label-danger\">FAIL</label>" : "<label style=\"font-size: 14px; \" class=\"label label-success\">PASS</label>",
                    Percentage = ((ar.NewMark / a.Mark) * 100) + " %",


                }).ToListAsync();

            return Json(assignments);
        }

        public async Task<IActionResult> UpdateOutstandingAssignment([FromQuery] Guid id, [FromQuery] bool outstanding)
        {
            var assignmentResult = await _context.AssignmentResults.SingleOrDefaultAsync(x => x.Id == id);

            if (assignmentResult != null)
            {
                var assignment =
                    await _context.Assignments.SingleOrDefaultAsync(x => x.Id == assignmentResult.AssignmentId);

                if (assignment != null)
                {
                    if (assignment.ExpireDate <= DateTime.Now)
                    {
                        assignmentResult.Outstanding = outstanding;

                        if (assignmentResult.Outstanding)
                        {
                            assignmentResult.NewMark = 0;
                        }


                        _context.AssignmentResults.Update(assignmentResult);

                        await _context.SaveChangesAsync();

                        return Json(assignmentResult);
                    }
                    return BadRequest("You can not chose outstanding if the submission date is not over, yet");
                }

                return BadRequest("Assignment not found");
            }

            return BadRequest("Assignment on this student not found");

        }
        public async Task<IActionResult> GetMyPersonalAssignmentMarks()
        {
            var user = await _userManager.GetUserAsync(User);

            var myAssignments = await (

                from ar in _context.AssignmentResults
                where ar.StudentId == user.Id
                join a in _context.Assignments
                    on ar.AssignmentId equals a.Id
                join c in _context.Course
                on a.CourseId equals c.Id
                select new
                {
                    Id = a.Id,
                    Mark = a.Mark,
                    Name = a.Name,
                    NewMark = ar.NewMark,
                    Weight = a.Weight,
                    CourseId = c.Id,
                    Grade = c.Grade,
                    CourseName = c.CourseName + " - " + c.Grade,
                }).ToListAsync();

            return Json(myAssignments);

        }
        public async Task<IActionResult> DeleteAssignment([FromQuery] Guid id)
        {
            var assignment = await _context.Assignments.SingleOrDefaultAsync(x => x.Id == id);

            if (assignment != null)
            {
                var assignmentResults = await _context.AssignmentResults.Where(x => x.AssignmentId == assignment.Id)
                    .ToListAsync();

                foreach (var aResults in assignmentResults)
                {
                    _context.AssignmentResults.RemoveRange(aResults);
                    await _context.SaveChangesAsync();

                }

                _context.Assignments.Remove(assignment);
                await _context.SaveChangesAsync();

                return Json(assignment);

            }

            return BadRequest("Assignment not found");
        }


        public async Task<IActionResult> GetMyAssignment([FromQuery] Guid id)
        {
            var assignment = await _context.Assignments.SingleOrDefaultAsync(x => x.Id == id);

            return Json(assignment);
        }
        public async Task<IActionResult> GetAllMyChildsAssignment([FromQuery] string studentId, [FromQuery] Guid courseId)
        {
            var getAllMyStudentAssignments = await (
                from ar in _context.AssignmentResults
                join a in _context.Assignments
                    on ar.AssignmentId equals a.Id
                join c in _context.Course
                on a.CourseId equals c.Id
                where a.CourseId == courseId && ar.StudentId == studentId
                select new
                {
                    Id = ar.Id,
                    AssignmentId = a.Id,
                    AssignmnetResult = ar.Id,
                    AssignmentName = a.Name,
                    AssignmentMark = a.Mark,
                    Weight = a.Weight,
                    NewMark = ar.NewMark,
                    Percentage = ((ar.NewMark / a.Mark) * 100) + " %",
                    Outcome = ((ar.NewMark / a.Mark) * 100) <= 49 ? "<label style=\"font-size: 14px; \" class=\"label label-danger\">FAIL</label>" : "<label style=\"font-size: 14px; \" class=\"label label-success\">PASS</label>",
                    StudentId = ar.StudentId,
                    WeightMark = (ar.NewMark / a.Mark) * a.Weight,
                    CourseId = a.CourseId,
                    Outstanding = ar.Outstanding,
                    Date = a.ExpireDate.ToString("dd MM yyyy")
                }).ToListAsync();


            return Json(getAllMyStudentAssignments);

        }
        public async Task<IActionResult> GetAllMyChildsOutstandingAssignment([FromQuery] string studentId, [FromQuery] Guid courseId)
        {
            var getAllMyStudentAssignments = await (
                from ar in _context.AssignmentResults
                where ar.StudentId == studentId
                join a in _context.Assignments
                    on ar.AssignmentId equals a.Id
                where ar.Outstanding == true
                join c in _context.Course
                    on a.CourseId equals c.Id
                where a.CourseId == courseId
                select new
                {
                    Id = ar.Id,
                    AssignmentId = a.Id,
                    AssignmnetResult = ar.Id,
                    AssignmentName = a.Name,
                    AssignmentMark = a.Mark,
                    Date = a.ExpireDate.ToString("yyyy MMMM dd"),
                    Weight = a.Weight,
                    NewMark = 0,
                    Percentage = ((ar.NewMark / a.Mark) * 100) + " %",
                    Outcome = ((ar.NewMark / a.Mark) * 100) == 0 ? "<label style=\"font-size: 14px; \" class=\"label label-danger\">OUTSTANDING</label>" : "<label style=\"font-size: 14px; \" class=\"label label-success\">PASS</label>",
                    StudentId = ar.StudentId,
                    WeightMark = (a.Weight / 100) * ar.NewMark,
                    CourseId = a.CourseId,
                    Outstanding = ar.Outstanding
                }).ToListAsync();


            return Json(getAllMyStudentAssignments);

        }

        public async Task<IActionResult> GetAllMyStudentsAssignment([FromQuery] Guid courseId, [FromQuery] string studentId)
        {
            var getAllMyStudentAssignments = await (
                from a in _context.Assignments
                join ar in _context.AssignmentResults
                    on a.Id equals ar.AssignmentId
                join c in _context.Course
                    on a.CourseId equals c.Id
                where a.CourseId == courseId && ar.StudentId == studentId
                select new
                {
                    Id = ar.Id,
                    AssignmentId = a.Id,
                    AssignmnetResult = ar.Id,
                    AssignmentName = a.Name,
                    AssignmentMark = a.Mark,
                    Weight = a.Weight,
                    NewMark = ar.NewMark,
                    Percentage = ((ar.NewMark / a.Mark) * 100) + " %",
                    StudentId = ar.StudentId,
                    WeightMark = (a.Weight / 100) * ar.NewMark,
                    CourseId = a.CourseId,
                    Outstanding = ar.Outstanding,
                    Date = a.ExpireDate.ToString("f")
                }).ToListAsync();


            return Json(getAllMyStudentAssignments);
        }
        public async Task<IActionResult> GetMyStudentsAssignmentMark([FromQuery] Guid assignmentResultId)
        {
            var getMyStudentResultForThisAssignment = await (
                from ar in _context.AssignmentResults
                where ar.Id == assignmentResultId
                join a in _context.Assignments
                    on ar.AssignmentId equals a.Id
                select new
                {
                    Id = ar.Id,
                    AssignmnetId = a.Id,
                    CourseId = a.CourseId,
                    AssignmentName = a.Name,
                    Date = a.ExpireDate.ToString("f"),
                    AssignmentMark = a.Mark,
                    NewMark = ar.NewMark,
                    Outstanding = ar.Outstanding,
                    Weight = a.Weight

                }).SingleOrDefaultAsync();
            return Json(getMyStudentResultForThisAssignment);
        }

        public async Task<IActionResult> GetMyAssignments()
        {
            var user = await _userManager.GetUserAsync(User);

            var myAssignments = await (
                from c in _context.Course
                join a in _context.Assignments
                    on c.Id equals a.CourseId
                join u in _context.Users
                on c.TeacherId equals u.Id
                where c.TeacherId == user.Id
                select new
                {
                    Id = a.Id,
                    Mark = a.Mark,
                    Date = a.ExpireDate.ToString("yyyy MMMM dd"),
                    Name = a.Name,
                    Weight = a.Weight,
                    CourseId = c.Id,
                    Grade = c.Grade,
                    CourseName = c.CourseName + " - " + c.Grade,
                    Teacher = c.TeacherId

                }).ToListAsync();

            return Json(myAssignments);
        }

        public async Task<IActionResult> UpdateMyAssignment([FromBody] UpdateMyAssignment modal)
        {
            if (ModelState.IsValid)
            {
                var assignmnet = await _context.Assignments.SingleOrDefaultAsync(x => x.Id == modal.Id);
                if (assignmnet != null)
                {
                    assignmnet.CourseId = modal.CourseId;
                    assignmnet.Mark = modal.Mark;
                    assignmnet.Weight = modal.Weight;

                    _context.Assignments.Update(assignmnet);

                    await _context.SaveChangesAsync();

                    return Json(assignmnet);

                }
                return BadRequest("Assignment not found");

            }

            return BadRequest("Modal is not valid");
        }

        public async Task<IActionResult> UpdateCurrentStudentsAssignmentMark([FromBody] UpdateAssignmentPostModal modal)
        {
            if (ModelState.IsValid)
            {
                var assignmentResult =
                    await _context.AssignmentResults.SingleOrDefaultAsync(x =>
                        x.Id == modal
                            .Id);

                if (assignmentResult != null)
                {
                    var assignment =
                            await _context.Assignments.SingleOrDefaultAsync(x => x.Id == assignmentResult.AssignmentId);

                    if (assignment != null)
                    {
                        if (assignment.Mark < modal.NewMark)
                        {
                            return BadRequest("This mark cannot be higher than the grade mark");
                        }
                        else
                        {
                            assignmentResult.NewMark = modal.NewMark;

                            _context.AssignmentResults.Update(assignmentResult);

                            await _context.SaveChangesAsync();

                            return Json(assignmentResult);
                        }
                    }
                    return BadRequest("Assignment  not found");


                }
                return BadRequest("Assignment result not found");

            }

            return BadRequest("Modal not valid");

        }
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentPostModal modal)
        {
            if (ModelState.IsValid)
            {
                var course = await _context.Course.SingleOrDefaultAsync(x => x.Id == modal.Course);

                if (course != null)
                {
                    var existingAssignment = await _context.Assignments.SingleOrDefaultAsync(x => x.Name == modal.Name && x.Mark == modal.Mark &&
                        x.CourseId == course.Id && x.Weight == modal.Weight && x.ExpireDate == modal.ExpireDate);

                    if (modal.ExpireDate > DateTime.Now)
                    {
                        if (existingAssignment == null)
                        {

                            var newAssignment = new Assignments()
                            {
                                Id = Guid.NewGuid(),
                                Name = modal.Name,
                                Mark = modal.Mark,
                                CourseId = course.Id,
                                Weight = modal.Weight,
                                ExpireDate = modal.ExpireDate

                            };

                            var myStudents = await _context.MyCourses.Where(x => x.CourseId == course.Id && x.Status == true).ToListAsync();
                            if (myStudents.Count > 0)
                            {
                                foreach (var allMyStudents in myStudents)
                                {
                                    var assignmentResults = new AssignmentResults()
                                    {
                                        Id = Guid.NewGuid(),
                                        StudentId = allMyStudents.StudentId,
                                        NewMark = 0,
                                        AssignmentId = newAssignment.Id

                                    };
                                    await _context.AddRangeAsync(assignmentResults);

                                }
                                await _context.Assignments.AddAsync(newAssignment);
                                await _context.SaveChangesAsync();
                                return Json(newAssignment);
                            }

                            return BadRequest("You have no students to give an assignment to");


                        }
                        return BadRequest("There is already an Assignment with the same information");
                    }

                    return BadRequest("Please choose a valid date and time to create an assignment");

                }
                return BadRequest("Course not found");
            }

            return BadRequest("Modal not found");
        }

        public async Task<IActionResult> GetMyCourses()
        {
            var user = await _userManager.GetUserAsync(User);
            var myCourses = await (
                from c in _context.Course
                join u in _context.Users
                    on c.TeacherId equals u.Id
                where c.TeacherId == user.Id
                select new
                {
                    Id = c.Id,
                    Name = c.CourseName,
                    Grade = c.Grade,
                    TeacherId = c.TeacherId
                }).ToListAsync();

            return Json(myCourses);
        }
        [HttpGet]
        public FileStreamResult GetFileStreamResultDemo(string fileName) //download file
        {
            string path = "/wwwroot/Submitedassgnments/" + fileName;
            var stream = new MemoryStream(System.IO.File.ReadAllBytes(path));
            string contentType = SpClass.GetContenttype(fileName);
            return new FileStreamResult(stream, new MediaTypeHeaderValue(contentType))
            {
                FileDownloadName = fileName
            };
        }

        [HttpGet]
        public FileContentResult GetFileContentResultDemo(string fileName)
        {
            string path = "/wwwroot/Submitedassgnments/" + fileName;
            byte[] fileContent = System.IO.File.ReadAllBytes(path);
            string contentType = SpClass.GetContenttype(fileName);
            return new FileContentResult(fileContent, contentType);
        }
        [HttpGet]
        public VirtualFileResult GetVirtualFileResultDemo(string filename)
        {
            string path = "Submitedassgnments/" + filename;
            string contentType = SpClass.GetContenttype(filename);
            return new VirtualFileResult(path, contentType);
        }
        [HttpGet]
        public PhysicalFileResult GetPhysicalFileResultDemo(string fileName)
        {
            string path = "/wwwroot/Submitedassgnments/" + fileName;
            string contentType = SpClass.GetContenttype(fileName);
            return new PhysicalFileResult(_hostingEnvironment.ContentRootPath
                                          + path, contentType);
        }
        [HttpGet]
        public FileResult GetFileResultDemo(string fileName)
        {
            string path = "/Submitedassgnments/" + fileName;
            string contentType = SpClass.GetContenttype(fileName);
            return File(path, contentType);
        }

        [HttpGet]
        public IActionResult SubmitAssignment()
        {
            ViewBag.Action = "Upload";
            ViewBag.Assignment = _context.UpdateMyAssignments.Select(t => t).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAssignmentAsync(UpdateMyAssignment myFile)
        {
            if (Request.Form.Files.Count > 0)
            {
                UpdateMyAssignment assignment = new UpdateMyAssignment();
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    assignment.attachment = dataStream.ToArray();
                }

            }

            return RedirectToAction();

        }

        public static byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }*/
    }
}
