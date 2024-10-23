package drawer;

import libinterfacesofcolors.ColorFactory;
import libinterfacesofcolors.Color;
import libinterfacesofshapes.ShapeFactory;
import libinterfacesofshapes.Shape;

import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.util.List;
import java.util.Optional;

public class Drawer {

    private static final List<ShapeFactory> shapeFactories = new java.util.ArrayList<>();
    private static final List<ColorFactory> colorFactories = new java.util.ArrayList<>();
    private static Shape shape;
    private static Color color;

    public static void draw(String shapeName, String colorName) {
        shape = shapeFactories.stream()
                .map(factory -> factory.create(shapeName))
                .filter(Optional::isPresent)
                .map(Optional::get)
                .findFirst()
                .orElse(null);

        if (shape == null) {
            System.out.println("No shape factory can create a shape named [" + shapeName + "].");
        }

        color = colorFactories.stream()
                .map(factory -> factory.create(colorName))
                .filter(Optional::isPresent)
                .map(Optional::get)
                .findFirst()
                .orElse(null);

        if (color == null) {
            System.out.println("No color factory can create a color named [" + colorName + "].");
        }

        System.out.println("Shape: [" + (shape != null ? shape.getName() : "No shape") + "] with color: [" +
                (color != null ? color.getName() : "No color") + "].");
    }

    public static void findFactories()
            throws ClassNotFoundException, InstantiationException, IllegalAccessException, InvocationTargetException,
            IOException {
        CbbBuilder.findFactories(shapeFactories, colorFactories);
    }
}
