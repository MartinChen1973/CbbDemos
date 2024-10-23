package libinterfacesofcolors;

import libinterfacecbbbase.CbbFactory;
import java.util.Optional;

public interface ColorFactory extends CbbFactory {
    Optional<Color> create(String shapeName);
}
