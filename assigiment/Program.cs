using assigiment.Data;
using assigiment.Entities;
using assigiment.Enums;
using Microsoft.EntityFrameworkCore;
PrintSubCategories(1);
PrintTopCategories();
CourseRoster(1);
UpdateProgress(1, 2, 100);
DeleteCategory(5);
DeleteCourse(2);

using AppDbContext context = new AppDbContext();

if (!context.Students.Any())
{
    context.Students.AddRange(

        new Student
        {
            FullName = "Ali Ahmed",
            Email = "ali@gmail.com",
            DateOfBirth = new DateTime(2000, 1, 1)
        },

        new Student
        {
            FullName = "Omar Mohamed",
            Email = "omar@gmail.com",
            DateOfBirth = new DateTime(2001, 2, 2)
        },

        new Student
        {
            FullName = "Sara Ali",
            Email = "sara@gmail.com",
            DateOfBirth = new DateTime(1999, 3, 3)
        },

        new Student
        {
            FullName = "Nada Hassan",
            Email = "nada@gmail.com",
            DateOfBirth = new DateTime(2002, 4, 4)
        },

        new Student
        {
            FullName = "Mona Ahmed",
            Email = "mona@gmail.com",
            DateOfBirth = new DateTime(2001, 5, 5)
        }
    );

    context.SaveChanges();
}

if (!context.Enrollments.Any())
{
    context.Enrollments.AddRange(

        new Enrollment
        {
            StudentId = 1,
            CourseId = 1,
            EnrollmentDate = DateTime.Now,
            Progress = 30,
            Status = EnrollmentStatus.Active
        },

        new Enrollment
        {
            StudentId = 1,
            CourseId = 2,
            EnrollmentDate = DateTime.Now,
            Progress = 100,
            Status = EnrollmentStatus.Completed,
            FinalGrade = 95
        },

        new Enrollment
        {
            StudentId = 2,
            CourseId = 1,
            EnrollmentDate = DateTime.Now,
            Progress = 50,
            Status = EnrollmentStatus.Active
        },

        new Enrollment
        {
            StudentId = 2,
            CourseId = 2,
            EnrollmentDate = DateTime.Now,
            Progress = 0,
            Status = EnrollmentStatus.Dropped
        },

        new Enrollment
        {
            StudentId = 3,
            CourseId = 1,
            EnrollmentDate = DateTime.Now,
            Progress = 100,
            Status = EnrollmentStatus.Completed,
            FinalGrade = 90
        },

        new Enrollment
        {
            StudentId = 4,
            CourseId = 2,
            EnrollmentDate = DateTime.Now,
            Progress = 70,
            Status = EnrollmentStatus.Active
        },

        new Enrollment
        {
            StudentId = 5,
            CourseId = 1,
            EnrollmentDate = DateTime.Now,
            Progress = 100,
            Status = EnrollmentStatus.Completed,
            FinalGrade = 88
        },

        new Enrollment
        {
            StudentId = 5,
            CourseId = 2,
            EnrollmentDate = DateTime.Now,
            Progress = 20,
            Status = EnrollmentStatus.Active
        }

    );

    context.SaveChanges();
}

Console.WriteLine("Students and Enrollments Seeded Successfully");

AddInstructor();

static void AddInstructor()
{
    using AppDbContext context = new AppDbContext();

    var instructor = new Instructor
    {
        FullName = "Mahmoud Ezz",
        Email = "mahmoud@gmail.com",
        JoinDate = DateTime.Now
    };

    context.Instructors.Add(instructor);

    context.SaveChanges();

    Console.WriteLine($"Instructor Id = {instructor.Id}");
}

AddCourse();

static void AddCourse()
{
    using AppDbContext context = new AppDbContext();

    int instructorId = 1;
    int categoryId = 3;

    bool instructorExists =
        context.Instructors.Any(i => i.Id == instructorId);

    bool categoryExists =
        context.Categories.Any(c => c.Id == categoryId);

    if (!instructorExists || !categoryExists)
    {
        Console.WriteLine("Instructor Or Category Not Found");
        return;
    }

    var course = new Course
    {
        Title = "SQL Server",
        Price = 1500,
        Level = CourseLevel.Beginner,
        IsPublished = true,
        PublishedDate = DateTime.Now,
        InstructorId = instructorId,
        CategoryId = categoryId
    };

    context.Courses.Add(course);

    context.SaveChanges();

    Console.WriteLine("Course Added");
}


FindCourse(1);

static void FindCourse(int id)
{
    using AppDbContext context = new AppDbContext();

    var course = context.Courses.Find(id);

    if (course == null)
    {
        Console.WriteLine("Course Not Found");
        return;
    }

    Console.WriteLine(
        $"{course.Title} - {course.Price}");
}



UpdatePrice(1);

static void UpdatePrice(int id)
{
    using AppDbContext context = new AppDbContext();

    var course = context.Courses.Find(id);

    if (course == null)
        return;

    course.Price += 500;

    context.SaveChanges();

    Console.WriteLine("Price Updated");
}



DeleteEnrollment(1, 1);

static void DeleteEnrollment(
    int studentId,
    int courseId)
{
    using AppDbContext context = new AppDbContext();

    var enrollment =
        context.Enrollments.Find(
            studentId,
            courseId);

    if (enrollment == null)
    {
        Console.WriteLine("Enrollment Not Found");
        return;
    }

    context.Enrollments.Remove(enrollment);

    context.SaveChanges();

    Console.WriteLine("Enrollment Deleted");
}

FindByTitle("ASP.NET Core");

static void FindByTitle(string title)
{
    using AppDbContext context = new();

    var course = context.Courses
                        .FirstOrDefault(c => c.Title == title);

    if (course == null)
    {
        Console.WriteLine("Course Not Found");
        return;
    }

    Console.WriteLine(course.Title);
}



FilterCourses(1000, 3000);

static void FilterCourses(decimal min, decimal max)
{
    using AppDbContext context = new();

    var courses = context.Courses
                         .Where(c => c.Price >= min &&
                                     c.Price <= max)
                         .OrderByDescending(c => c.Price);

    foreach (var course in courses)
    {
        Console.WriteLine(
            $"{course.Title} {course.Level} {course.Price}");
    }
}



PublishCourse(4);

static void PublishCourse(int id)
{
    using AppDbContext context = new();

    var course = context.Courses.Find(id);

    if (course == null)
        return;

    course.IsPublished = true;
    course.PublishedDate = DateTime.Now;

    context.SaveChanges();

    Console.WriteLine("Course Published");
}


BulkRaise();

static void BulkRaise()
{
    using AppDbContext context = new();

    var rows = context.Courses
                      .Where(c => c.IsPublished)
                      .ExecuteUpdate(x =>

                          x.SetProperty(
                              p => p.Price,
                              p => p.Price * 1.1m
                          ));

    Console.WriteLine(rows);
}

DeleteDropped();

static void DeleteDropped()
{
    using AppDbContext context = new();

    var rows = context.Enrollments
                      .Where(e => e.Status ==
                                  EnrollmentStatus.Dropped)
                      .ExecuteDelete();

    Console.WriteLine(rows);
}


PrintSubCategories(1);

static void PrintSubCategories(int categoryId)
{
    using AppDbContext context = new();

    var subs = context.Categories
        .Where(c => c.ParentCategoryId == categoryId)
        .ToList();

    foreach (var item in subs)
    {
        Console.WriteLine(item.Name);
    }
}



PrintTopCategories();

static void PrintTopCategories()
{
    using AppDbContext context = new();

    var categories = context.Categories
        .Where(c => c.ParentCategoryId == null)
        .ToList();

    foreach (var item in categories)
    {
        Console.WriteLine(item.Name);
    }
}


CourseRoster(1);

static void CourseRoster(int courseId)
{
    using AppDbContext context = new();

    var ids = context.Enrollments
        .Where(e => e.CourseId == courseId)
        .Select(e => e.StudentId)
        .ToList();

    Console.WriteLine($"Count = {ids.Count}");

    var students = context.Students
        .Where(s => ids.Contains(s.Id))
        .ToList();

    foreach (var student in students)
    {
        Console.WriteLine(student.FullName);
    }
}



UpdateProgress(1, 2, 100);

static void UpdateProgress(
    int studentId,
    int courseId,
    int progress)
{
    using AppDbContext context = new();

    if (progress < 0 || progress > 100)
    {
        Console.WriteLine("Invalid Progress");
        return;
    }

    var enrollment =
        context.Enrollments.Find(
            studentId,
            courseId);

    if (enrollment == null)
        return;

    enrollment.Progress = progress;

    if (progress == 100)
    {
        enrollment.Status =
            EnrollmentStatus.Completed;
    }

    context.SaveChanges();
}



DeleteCategory(5);

static void DeleteCategory(int id)
{
    using AppDbContext context = new();

    bool hasChildren =
        context.Categories
        .Any(c => c.ParentCategoryId == id);

    if (hasChildren)
    {
        Console.WriteLine(
            "Category Has Sub Categories");
        return;
    }

    var category =
        context.Categories.Find(id);

    if (category == null)
        return;

    context.Categories.Remove(category);

    context.SaveChanges();
}


DeleteCourse(2);

static void DeleteCourse(int id)
{
    using AppDbContext context = new();

    var course =
        context.Courses.Find(id);

    if (course == null)
        return;

    context.Courses.Remove(course);

    context.SaveChanges();

    Console.WriteLine("Course Deleted");
}





