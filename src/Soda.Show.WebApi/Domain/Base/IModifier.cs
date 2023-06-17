namespace Soda.Show.WebApi.Base;

public interface IModifier
{
    public Guid? ModifierId { get; set; }
    public DateTime? UpdateTime { get; set; }
}