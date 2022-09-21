using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AgroFamily.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _databasePath;
        public RepositoryBase()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _databasePath = System.IO.Path.Combine(folderPath, "AgroFamily.db");
        }

        protected SQLiteConnection GetConnection()
        {
            SQLite.SQLiteConnection database = new SQLite.SQLiteConnection(_databasePath);
            return database;
        }
    }
}
