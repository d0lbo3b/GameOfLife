namespace GameOfLife.extensions;

public static class ArrayExtension {
    public static void Write<T>(this T[] arr) {
        foreach (var el in arr) {
            Console.Write($"{el} ");
        }
        Console.WriteLine();
    }
}