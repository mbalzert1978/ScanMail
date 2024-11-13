namespace GatherFiles.Abstractions;

public interface IFromString<TSelf>
    where TSelf : struct {
    public static abstract TSelf FromString(string value);
    public static abstract TSelf FromString(string value, params object[] args);
}
