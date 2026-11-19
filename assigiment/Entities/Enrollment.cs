using assigiment.Enums;

namespace assigiment.Entities;

public class Enrollment
{
    public int StudentId { get; set; }

    public Student Student { get; set; }

    public int CourseId { get; set; }

    public Course Course { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public int Progress { get; set; }

    public EnrollmentStatus Status { get; set; }

    public decimal? FinalGrade { get; set; }
}