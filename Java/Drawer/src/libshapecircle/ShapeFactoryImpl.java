package libshapecircle;

import libinterfacesofshapes.Shape;
import libinterfacesofshapes.ShapeFactory;
import java.util.Optional;

public class ShapeFactoryImpl implements ShapeFactory {

    @Override
    public Optional<Shape> create(String shapeName) {
        if ("circle".equals(shapeName)) {
            return Optional.of(new Circle());
        }
        return Optional.empty();
    }
}
