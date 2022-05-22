using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest1.Builders
{
    public class DatabasedBuilder
    {
        internal  DataBaseContext GetDbContext()
        {
            var option = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new DataBaseContext(option);
        }
    }
}
