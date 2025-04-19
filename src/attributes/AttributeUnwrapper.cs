namespace GameOfLife.attributes;

public static class AttributeUnwrapper {
    public static TA? Unwrap<TA, TO>(TO obj) {
        var type = typeof(TO);
        
        object?[] attributes = type.GetCustomAttributes(false);

        foreach (var attr in attributes) {
            if (attr is TA targetAttr) {
                return targetAttr;
            }
        }

        return default;
    }
}