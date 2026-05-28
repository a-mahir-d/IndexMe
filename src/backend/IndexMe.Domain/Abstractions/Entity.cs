namespace IndexMe.Domain.Abstractions;

public abstract class Entity<TKey> : IEquatable<Entity<TKey>>
{
    public TKey Id { get; protected set; } = default!;

    protected Entity() { }

    public bool Equals(Entity<TKey>? other)
    {
        if (other is null) return false;
        if (other.GetType() != GetType()) return false;
        return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
    }
}
