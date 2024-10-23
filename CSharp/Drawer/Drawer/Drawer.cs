using System.Reflection;
using LibInterfaceCbbBase;
using LibInterfacesOfColors;
using LibInterfacesOfShapes;

namespace Drawer;

internal static class Drawer
{
    private static readonly List<IShapeFactory> ShapeFactories = new();
    private static readonly List<IColorFactory> ColorFactories = new();
    private static IShape? _shape;
    private static IColor? _color;

    public static void Draw(string shapeName, string colorName)
    {
        _shape = ShapeFactories.Select(shapeFactory => shapeFactory.Create(shapeName))
            .FirstOrDefault(shape => shape != null);
        if (_shape == null) Console.WriteLine($"No shape factory can create a shape named [{shapeName}].");
        _color = ColorFactories.Select(colorFactory => colorFactory.Create(colorName))
            .FirstOrDefault(color => color != null);
        if (_color == null) Console.WriteLine($"No color factory can create a color named [{colorName}].");

        Console.WriteLine($"Shape: [{_shape?.Name ?? "No shape"}] with color: [{_color?.Name ?? "No color"}].");
    }

    public static void FindFactories()
    {
        //Find all factories.
        var assemblies = LoadDllsInFolder();
        foreach (var assembly in assemblies)
        {
            // var allFactories = assembly.GetTypes().Where(i => i.Name.EndsWith("Factory") && !i.IsInterface).ToList(); //Find by name.
            var allFactories = assembly.GetTypes().Where(i => i.IsAssignableTo(typeof(ICbbFactory)) && !i.IsInterface).ToList(); //Find by type.
            if (allFactories.Count == 0)
                continue;

            Console.WriteLine($"Found [{allFactories.Count}] factories in assembly {assembly.FullName}.");
            foreach (var factory in allFactories) 
                Console.WriteLine($"Factory: {factory.Name}");

            ShapeFactories.AddRange(LoadFactories<IShapeFactory>(allFactories, ShapeFactoryName));
            ColorFactories.AddRange(LoadFactories<IColorFactory>(allFactories, ColorFactoryName));
        }
        if (ShapeFactories.Count == 0)
            Console.WriteLine($"No shape factories found from any dll. Shape factory name: [{ShapeFactoryName}]");
        if (ColorFactories.Count == 0)
            Console.WriteLine($"No color factories found from any dll. Color factory name: [{ColorFactoryName}]");
    }

    private static List<T> LoadFactories<T>(List<Type> allFactories, string factoryName) where T : class
    {
        var factories = new List<T>();

        foreach (var factory in allFactories)
        {
            if (!factory.Name.Equals(factoryName))
                continue;
            if (!factory.IsAssignableTo(typeof(T)))
            {
                Console.WriteLine($"The factory's name is right, but it's type [{factory.Name}] is not assignable to [{typeof(T).Name}].");
                continue;
            }
            factories.Add((Activator.CreateInstance(factory) as T)!);
        }

        return factories;
    }

    // private const string BusinessBlockDllName = "BusinessBlocks\\*.dll";
    private const string ShapeFactoryName = "ShapeFactory";
    private const string ColorFactoryName = "ColorFactory";

    private static List<Assembly> LoadDllsInFolder(string? folderName = null)
    {
        folderName = Directory.GetCurrentDirectory() + (folderName == null ? null : $@"\{folderName}");
        Console.WriteLine($"Loading dlls from folder {folderName}.");
        var dllFiles = Directory.GetFiles(folderName).Where(fn => fn.EndsWith(".dll")).ToList();

        return dllFiles.Select(fileName =>
            AppDomain.CurrentDomain.Load(Assembly.LoadFrom(fileName).GetName())).ToList();
    }
}