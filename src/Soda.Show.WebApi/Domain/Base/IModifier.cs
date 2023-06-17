using Soda.Show.WebApi.Domain;

namespace Soda.Show.WebApi.Base;

public interface IModifier
{
    public Guid? ModifierId { get; set; }
    public User? Modifier { get; set; }
    public DateTime? UpdateTime { get; set; }
}