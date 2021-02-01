namespace EFDemo
{
    public enum STEState
    {
        Added,
        Deleted,
        Modified,
        UnChanged
    }

    public interface IEntity
    {
        STEState State { get; set; }
    }
}