using System;
using System.Collections.Generic;

namespace ProjectPRN212.Models;

public partial class UserHistory
{
    public int HistoryId { get; set; }

    public int? UserId { get; set; }

    public int? ExamId { get; set; }

    public DateTime ExamDate { get; set; }

    public int? Score { get; set; }
}
