using LibInterfaceCbbBase;

namespace LibInterfacesOfShapes;

public interface IShapeFactory : ICbbFactory
{
    public IShape? Create(string shapeName);
}