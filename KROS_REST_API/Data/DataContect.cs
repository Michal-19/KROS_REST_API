using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.Data
{
    public class DataContect : DbContext
    {
        public DataContect(DbContextOptions<DataContect> options) : base(options) { }

    }
}
