using System;
using System.Collections.Generic;

namespace ProjectArinaka.Models;

public partial class Ww2table
{
    public string? Фио { get; set; }
    public string? Отдел { get; set; }
    public string? Оборудование { get; set; }
    public long? ИнвентарныйНомер { get; set; }
    public byte? Списано { get; set; }

    public string СписаноТекст
    {
        get { return Списано.HasValue && Списано.Value == 1 ? "да" : "нет"; }
    }
}

