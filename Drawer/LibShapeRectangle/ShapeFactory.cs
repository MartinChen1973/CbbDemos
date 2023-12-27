using LibInterfacesOfShapes;

namespace LibShapeRectangle;

public class ShapeFactory : IShapeFactory
{
    public IShape? Create(string shapeName)
    {
        return shapeName == "rectangle" ?
            new Rectangle() : null;
    }
}