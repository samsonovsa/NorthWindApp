using System;
using System.Runtime.Serialization;

namespace NorthWindApp.BLL.Services
{
    class CachedEntity<TEntity>
    {
        public TEntity Entity { get; set; }

        public DateTime TimeSetCache { get; set; }

        public string Path { get; set; }

        public CachedEntity()
        {
            if (!typeof(TEntity).IsSerializable && !(typeof(ISerializable).IsAssignableFrom(typeof(TEntity))))
                throw new InvalidOperationException("A serializable Type is required");
        }
    }
}
