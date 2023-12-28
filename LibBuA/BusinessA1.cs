using LibCbbFactoryBase;

namespace LibBuA;

public class BusinessA1 : ICbb
{
    public string Name { get; set; } = string.Empty;
    public string Run()
    {
        return $"Cbb {Name} is running.";
    }
}

public class BusinessA2 : ICbb
{
    public string Name { get; set; } = string.Empty;
    public string Run()
    {
        return $"Cbb {Name} is running.";
    }
}