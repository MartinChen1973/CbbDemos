// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.Text.Json;
using LibCbbFactoryBase;
using RndApp;

Console.WriteLine("Hello, World!");

//Load steps from AppSettings.json
var appSettings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText("AppSettings.json"));
if (appSettings == null)
{
    Console.WriteLine("Cannot load AppSettings.json");
    return;
}
Console.WriteLine(JsonSerializer.Serialize(appSettings));

//加载所有当前目录中的DLL
var dlls = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll");
//加载所有DLL中的Assembly
var assemblies = dlls.Select(Assembly.LoadFrom).ToList();

//查询assemblies中所有实现了ICbbFactory的类
var cbbFactories = assemblies.SelectMany(assembly => assembly.GetTypes())
    .Where(type => type.IsClass && !type.IsAbstract && typeof(ICbbFactory).IsAssignableFrom(type))
    .Select(type => Activator.CreateInstance(type) as ICbbFactory)
    .Where(factory => factory != null).ToList();
// var cbbFactories = new List<ICbbFactory> { new LibBuA.BuAFactory() };
Console.WriteLine($"Found {cbbFactories.Count} ICbbFactory");

var steps = new List<ICbb>();
//执行AppSettings里边的所有Steps
foreach (var step in appSettings.Steps)
{
    //让所有的cbbFactories执行CreateCbb方法，看是否能得到非空的ICbb
    var cbb = cbbFactories.Select(factory => factory?.CreateCbb(step)).
        FirstOrDefault(cbb => cbb != null);
    // var cbb = new LibBuA.BuAFactory().CreateCbb(step);
    if (cbb != null)
    {
        Console.WriteLine($"Created {cbb.GetType().Name}");
        steps.Add(cbb);
    }
    else
    {
        Console.WriteLine($"Cannot find ICbbFactory {step}");
    }
}

//执行所有的ICbb
foreach (var step in steps)
{
    Console.WriteLine(step.Run());
}
