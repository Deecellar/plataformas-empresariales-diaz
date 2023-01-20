using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public abstract class BaseEntity<TId> : IEntity<TId> where TId : struct
    {
        public virtual TId Id { get; set; }
    }


    public interface IEntity<TId>
    {
         TId Id { get; set; }
    }

}
