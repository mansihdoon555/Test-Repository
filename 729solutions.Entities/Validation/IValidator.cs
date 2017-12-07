using _729solutions.Entities.Base;

namespace _729solutions.Entities.Validation
{
    public interface IValidator
    {
        ValidationSummary Validate(Entity entity);
    }
}
