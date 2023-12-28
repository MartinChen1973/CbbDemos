namespace LibCbbFactoryBase;

public interface ICbbFactory
{
    ICbb? CreateCbb(string name);
}