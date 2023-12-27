if (args.Length == 0)
    args = new[] { "circle", "red" };
Console.WriteLine($"Trying to drawing a [{args[0]}] with a [{args[1]}] color.");

Drawer.Drawer.FindFactories();
Drawer.Drawer.Draw(args[0], args[1]);
//
// args = new[] { "circle", "blue" };
// Drawer.Drawer.Draw(args[0], args[1]);
// args = new[] { "rectangle", "red" };
// Drawer.Drawer.Draw(args[0], args[1]);
// args = new[] { "rectangle", "blue" };
// Drawer.Drawer.Draw(args[0], args[1]);
