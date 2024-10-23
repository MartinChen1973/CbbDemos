package drawer;

public class Main {

    public static void main(String[] args) {
        // Check if arguments length is not 2 (shape and color)
        if (args.length != 2) {
            System.out.println("Incorrect usage. The correct format is: java Main <shape> <color>");
            System.out.println("Example: java Main circle red");
            System.out.println("Defaulting to: 'circle' and 'red'.");
            args = new String[] { "circle", "red" };
        }

        // Output message showing the shape and color being drawn
        System.out.println("Trying to draw a [" + args[0] + "] with a [" + args[1] + "] color.");

        // Find the factories and draw the shape with the given color
        try {
            Drawer.findFactories(); // Calls Drawer.findFactories() to discover shape/color factories
            Drawer.draw(args[0], args[1]); // Draws the specified shape and color
        } catch (Exception e) {
            e.printStackTrace(); // Handles any exceptions thrown during the process
        }
    }
}
