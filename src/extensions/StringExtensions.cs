using System.Reflection.Metadata;

namespace GameOfLife.extensions;

public static class StringExtensions {
    public static string GetStreak(this string str, char target, int start = 0) {
        var j = start;
        var result = string.Empty;

        while(str[j] == target) {
            result += target;
            
            if (++j >= str.Length) break;
        }

        return result;
    }
    
    public static bool TryGetStreak(this string str, char target, int expectedLenght, out string result, int start = 0) {
        var j = start;
        result = string.Empty;

        while(str[j] == target) {
            result += target;
            
            if (++j >= str.Length) break;
        }

        return result.Length == expectedLenght;

    }
    
    public static bool TryGetStreak(this string str, string[] target, int expectedLenght, out string result, int start = 0) {
        var j = start;
        result = string.Empty;

        while(target.Any(x => x == str[j].ToString())) {
            result += target;
            
            if (++j >= str.Length) break;
        }

        return result.Length == expectedLenght;

    }
    
    public static bool TryGetStreak(this string str, string[] target, out string result, int start = 0) {
        result = string.Empty;
        var j = start;

        while(target.Any(x => x == str[j].ToString())) {
            result += str[j];
            
            if (++j >= str.Length) break;
        }

        return result.Length != 0;
    }
    
    public static bool TryGetFirst(this string str, string[] target, out string result, int start = 0) {
        result = string.Empty;
        var j = start;
        
        if (str.Length <= target.Length) {
            return false;
        }

        while(target.Any(x => x == str[j].ToString())) {
            result += str[j];
            
            if (result.Length > 0 || ++j >= str.Length) {
                break;
            }
        }

        return result.Length != 0;
    }

    public static bool TryFind(this string str, string target, int start = 0) {
        if (target.Length > str.Length) return false;
        
        for (var i = 0; i < str.Length; i++) {
            if (str[i] != target[0]) continue;

            var j = 1;
                
            if (j >= target.Length) return true;
            
            while (str[i+j] == target[j++]) {
                if (j == target.Length) return true;
            }
            
            i += j;
        }

        return false;
    }

    public static bool TryGetNumber(this string str, out string result, int start = 0) {
        var j = start;
        result = string.Empty;
        
        if (!int.TryParse(str[j].ToString(), out _)){
            return false;
        }
        
        while (int.TryParse(str[j].ToString(), out var parsed)) {
            result += parsed;
            
            if (++j >= str.Length) break;
        }

        return true;
    }
}