package drawer;

import libinterfacesofcolors.ColorFactory;
import libinterfacesofshapes.ShapeFactory;

import java.io.File;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.net.URL;
import java.net.URLClassLoader;
import java.util.*;
import java.util.jar.JarEntry;
import java.util.jar.JarFile;

public class CbbBuilder {

    private static final String SHAPE_FACTORY_NAME = "ShapeFactoryImpl";
    private static final String COLOR_FACTORY_NAME = "ColorFactoryImpl";

    /**
     * Find and instantiate shape and color factories by loading the JAR files and
     * checking for classes named ShapeFactoryImpl and ColorFactoryImpl.
     */
    public static void findFactories(List<ShapeFactory> shapeFactories, List<ColorFactory> colorFactories)
            throws ClassNotFoundException, InstantiationException, IllegalAccessException, InvocationTargetException,
            IOException {

        List<Class<?>> assemblies = loadJarsInFolder();
        System.out.println("Classes found:");
        assemblies.forEach(clazz -> System.out.println("Class: " + clazz.getName()));
        System.out.println("--------------");

        for (Class<?> assembly : assemblies) {
            if (assembly.getName().endsWith("." + SHAPE_FACTORY_NAME)) {
                try {
                    shapeFactories.add((ShapeFactory) assembly.getDeclaredConstructor().newInstance());
                } catch (InstantiationException | IllegalAccessException | IllegalArgumentException
                        | InvocationTargetException | NoSuchMethodException | SecurityException e) {
                    e.printStackTrace();
                }
            }
            if (assembly.getName().endsWith("." + COLOR_FACTORY_NAME)) {
                try {
                    colorFactories.add((ColorFactory) assembly.getDeclaredConstructor().newInstance());
                } catch (InstantiationException | IllegalAccessException | IllegalArgumentException
                        | InvocationTargetException | NoSuchMethodException | SecurityException e) {
                    e.printStackTrace();
                }
            }
        }

        // Print the factories found.
        System.out.println("Factories found:");
        shapeFactories.forEach(factory -> System.out.println("Shape factory: " + factory.getClass().getName()));
        colorFactories.forEach(factory -> System.out.println("Color factory: " + factory.getClass().getName()));
        System.out.println("--------------");
    }

    /**
     * Load JAR files in the current folder and list all the classes inside them.
     */
    private static List<Class<?>> loadJarsInFolder() throws ClassNotFoundException, IOException {
        String folderName = System.getProperty("user.dir") + File.separator + "libs"; // Path to the JARs
        System.out.println("Loading JARs from folder " + folderName + ".");
        File folder = new File(folderName);
        File[] jarFiles = folder.listFiles((dir, name) -> name.endsWith(".jar"));

        if (jarFiles == null)
            return Collections.emptyList();

        List<Class<?>> loadedClasses = new ArrayList<>();
        for (File jarFile : jarFiles) {
            System.out.println("Loading JAR: " + jarFile.getName());
            URL jarUrl = jarFile.toURI().toURL();
            URLClassLoader classLoader = new URLClassLoader(new URL[] { jarUrl }, CbbBuilder.class.getClassLoader());

            // Open the JAR file and list all classes
            JarFile jar = new JarFile(jarFile);
            Enumeration<JarEntry> entries = jar.entries();
            while (entries.hasMoreElements()) {
                JarEntry entry = entries.nextElement();
                if (entry.getName().endsWith(".class")) {
                    // Convert the entry name into a class name
                    String className = entry.getName().replace("/", ".").replace(".class", "");
                    System.out.println("Found class: " + className);
                    loadedClasses.add(classLoader.loadClass(className));
                }
            }
            jar.close();
        }

        return loadedClasses;
    }
}
