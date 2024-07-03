using System;
using System.Collections.Generic;

namespace ProjectPRN212.Models;

public partial class ExamResult
{
    public int ResultId { get; set; }

    public int? ExamId { get; set; }

    public int TotalQuestions { get; set; }

    public int CorrectAnswers { get; set; }

    public int Score { get; set; }
}
