package libshaperectangle;

import libinterfacesofshapes.Shape;
import libinterfacesofshapes.ShapeFactory;
import java.util.Optional;

public class ShapeFactoryImpl implements ShapeFactory {

    @Override
    public Optional<Shape> create(String shapeName) {
        if ("rectangle".equals(shapeName)) {
            return Optional.of(new Rectangle());
        }
        return Optional.empty();
    }
}
