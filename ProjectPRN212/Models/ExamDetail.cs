using System;
using System.Collections.Generic;

namespace ProjectPRN212.Models;

public partial class ExamDetail
{
    public int ExamDetailId { get; set; }

    public int? ExamId { get; set; }

    public int? QuestionId { get; set; }

    public string? UserAnswer { get; set; }

    public bool IsCorrect { get; set; }
}
