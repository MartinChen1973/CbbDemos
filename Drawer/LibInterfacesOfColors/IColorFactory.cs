using LibInterfaceCbbBase;

namespace LibInterfacesOfColors;

public interface IColorFactory : ICbbFactory
{
    public IColor? Create(string shapeName);
}