using System;
using System.Collections.Generic;

namespace ProjectPRN212.Models;

public partial class AdminAction
{
    public int ActionId { get; set; }

    public int? AdminId { get; set; }

    public string ActionType { get; set; } = null!;

    public int? QuestionId { get; set; }

    public DateTime ActionDate { get; set; }
}
