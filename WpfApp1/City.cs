using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class City
{
    public int Id { get; set; }

    public int Idcountry { get; set; }

    public string NameCity { get; set; } = null!;

    public int CityPop { get; set; }

    public virtual Country IdcountryNavigation { get; set; } = null!;
}
