using System;
using System.Collections.Generic;

namespace ProjectPRN212.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string? OptionA { get; set; }

    public string? OptionB { get; set; }

    public string? OptionC { get; set; }

    public string? OptionD { get; set; }

    public string? CorrectOption { get; set; }

    public string? ImagePath { get; set; }
}
