using LibInterfacesOfShapes;

namespace LibShapeCircle;

public class ShapeFactory : IShapeFactory
{
    public IShape? Create(string shapeName)
    {
        return shapeName == "circle" ? 
            new Circle() : null;
    }
}