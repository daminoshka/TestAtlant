using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.DAL.DAL_Core.EF;

namespace Infrastructure.DAL.DAL_Core.Interfaces
{
    public interface IDbFactory
    {
        EntitiesContext Init();
    }
}
