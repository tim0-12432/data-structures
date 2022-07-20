namespace DatastructuresTests;

/// <summary>
/// Interface ensuring a type can be decreased.
/// </summary>
public interface IDecreasable
{
    /// <summary>
    /// Decrease.
    /// </summary>
    /// <returns>A instance of IDecreaseable</returns>
    IDecreasable Decrease();
    
    public static IDecreasable operator --(IDecreasable decreasable)
    {
        return decreasable.Decrease();
    }
}