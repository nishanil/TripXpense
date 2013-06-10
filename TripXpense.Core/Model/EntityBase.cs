using System;

namespace TripXpense.Core.Model
{
    public class EntityBase : IUniqueEntity<Guid>
    {
        private Guid _id;

        /// <summary>
        /// Auto Generating ID value
        /// </summary>
        public Guid Id
        {
            get
            {
                if (_id == default(Guid))
                    _id = Guid.NewGuid();
                return _id;
            }
            set { _id = value; }
        }
    }
}
