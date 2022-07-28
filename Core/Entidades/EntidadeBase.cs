namespace Core.Entidades
{
    public abstract class EntidadeBase
    {
        public virtual string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
