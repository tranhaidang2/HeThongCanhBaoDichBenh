using System;
using System.Collections.Generic;

namespace HeThongCanhBaoDichBenh.Models;

public partial class Prevention
{
    public int PreventionId { get; set; }

    public string? Method { get; set; }

    public int? DiseaseId { get; set; }

    public virtual Disease? Disease { get; set; }
}
