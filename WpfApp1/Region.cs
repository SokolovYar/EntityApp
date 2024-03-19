using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Region
{
    public int Id { get; set; }

    public string NameRegion { get; set; } = null!;

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
