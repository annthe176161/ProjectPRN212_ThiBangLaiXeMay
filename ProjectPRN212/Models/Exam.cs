using System;
using System.Collections.Generic;

namespace ProjectPRN212.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public int? UserId { get; set; }

    public DateTime ExamDate { get; set; }

    public int? Score { get; set; }
}
