using Microsoft.EntityFrameworkCore;

namespace Avans.Demo.DataAccess.SqlLite
{
    /// <summary>
    /// Implementation for <see cref="DataContext"/>
    /// This version implements SQLite. The base file is expected
    /// to be found in the local application data folder with 
    /// filename avansDemo.db
    /// </summary>
    public class SqlLiteDataContext : DataContext
    {
        private string _dbPath { get; }

        public SqlLiteDataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            _dbPath = Path.Join(path, "avansDemo.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
