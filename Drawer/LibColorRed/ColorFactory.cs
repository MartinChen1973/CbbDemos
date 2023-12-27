using LibInterfacesOfColors;

namespace LibColorRed;

public class ColorFactory : IColorFactory
{
    public IColor? Create(string shapeName)
    {
        return shapeName == "red" ?
            new RedColor() : null;
    }
}