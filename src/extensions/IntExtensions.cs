namespace GameOfLife.extensions;

public static class IntExtensions {
    public static int Loop(this int value, int start, int end) {
        var result = value;
        if (value >= end) {
            result = start;
        } else if (value < start) {
            result = end-1;
        }
        return result;
    }
}