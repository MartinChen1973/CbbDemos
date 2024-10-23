using LibInterfacesOfColors;

namespace LibColorBlue;

public class ColorFactory : IColorFactory
{
    public IColor? Create(string shapeName)
    {
        return shapeName == "blue" ? 
            new BlueColor() : null;
    }
}