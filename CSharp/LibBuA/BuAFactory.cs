using LibCbbFactoryBase;

namespace LibBuA;

public class BuAFactory : ICbbFactory
{
    public ICbb? CreateCbb(string name)
    {
        // foreach (var factory in BuAFactories)
        // {
        //     var cbb = factory.CreateCbb(name);
        //     if (cbb != null)
        //     {
        //         return cbb;
        //     }
        // }
        // return null;
        return name == "BusinessA1" ? new BusinessA1 { Name = "BusinessA1" }
            : name == "BusinessA2" ? new BusinessA2 { Name = "BusinessA2" }
            : null;
    }
}