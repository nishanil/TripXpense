
namespace TripXpense.Core.Model
{
    public interface IUniqueEntity<TEntityType>
    {

        /// <summary>
        /// Unique Entity ID
        /// </summary>
        TEntityType Id { get; set; }
    }
}
