using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyNewMicroService.Order.Domain.Entities
{
    public class BaseEntity<TEntityId>
    {
        public TEntityId Id { get; set; } = default!;
    }
}
