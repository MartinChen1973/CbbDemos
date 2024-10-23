package libcolorblue;

import libinterfacesofcolors.Color;
import libinterfacesofcolors.ColorFactory;
import java.util.Optional;

public class ColorFactoryImpl implements ColorFactory {

    @Override
    public Optional<Color> create(String shapeName) {
        if ("blue".equals(shapeName)) {
            return Optional.of(new BlueColor());
        }
        return Optional.empty();
    }
}
