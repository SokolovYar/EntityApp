using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Country
{
    public int Id { get; set; }

    public int Idregion { get; set; }

    public string NameCountry { get; set; } = null!;

    public double Square { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Region IdregionNavigation { get; set; } = null!;
}
