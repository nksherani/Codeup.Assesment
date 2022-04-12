using Codeup.Assesment.Data.Repository;
using Codeup.Assesment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeup.Assesment.Services.Interfaces
{
    public interface ISeedService
    {
        Task SeedAsync();
    }
}
