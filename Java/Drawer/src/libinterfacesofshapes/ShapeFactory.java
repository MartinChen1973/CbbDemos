package libinterfacesofshapes;

import libinterfacecbbbase.CbbFactory;
import java.util.Optional;

public interface ShapeFactory extends CbbFactory {
    Optional<Shape> create(String shapeName);
}
