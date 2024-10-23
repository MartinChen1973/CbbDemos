package libcolorred;

import libinterfacesofcolors.Color;
import libinterfacesofcolors.ColorFactory;
import java.util.Optional;

public class ColorFactoryImpl implements ColorFactory {

    @Override
    public Optional<Color> create(String shapeName) {
        if ("red".equals(shapeName)) {
            return Optional.of(new RedColor());
        }
        return Optional.empty();
    }
}
