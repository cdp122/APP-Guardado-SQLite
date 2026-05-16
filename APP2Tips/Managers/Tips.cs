using System;
using System.Collections.Generic;
using System.Text;
using APP2Tips.Entities;
using Dapper;
using Microsoft.Data.Sqlite;

namespace APP2Tips.Managers
{
    /// <summary>
    /// Esta es la clase Manager o DAO que permite las instrucciones CRUD (Create, Read, Update, Delete)
    /// </summary>
    internal class Tips
    {
        private string connectionString;
        private SqliteConnection connection;

        public Tips()
        {
            // ↓ Garantiza que funcione en multiplatafora
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "tips.db");
            connectionString = $"Data Source={dbPath}";
            connection = new SqliteConnection(connectionString);
            connection.Open();

            // Crear la tabla Mascotas si no existe
            connection.Execute(@"
                CREATE TABLE IF NOT EXISTS Tips (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                StackTechID INTEGER,
                TituloTip TEXT, 
                DescripcionTip TEXT,
                CodigoTip TEXT)");
        }

        public Tip CreateTip(Tip tip)
        {
            var sql =
                "INSERT INTO Tips (StackTechID, TituloTip, DescripcionTip, CodigoTip) VALUES " +
                "(@StackTechID, @TituloTip, @DescripcionTip, @CodigoTip); SELECT last_insert_rowid();";
            tip.ID = connection.ExecuteScalar<int>(sql, tip);
            return tip;
        }

        public Tip CreateTip(int stackTechID, string titulo, string descripcion, string codigo)
        {
            var tip = new Tip
            {
                StackTechID = stackTechID,
                TituloTip = titulo,
                DescripcionTip = descripcion,
                CodigoTip = codigo
            };
            var sql =
                "INSERT INTO Tips (StackTechID, TituloTip, DescripcionTip, CodigoTip) VALUES " +
                "(@StackTechID, @TituloTip, @DescripcionTip, @CodigoTip); SELECT last_insert_rowid();";
            tip.ID = connection.ExecuteScalar<int>(sql, tip);
            return tip;
        }

        public List<Tip> GetAllTips()
        {
            var data = connection.Query<Tip>("SELECT * FROM Tips");
            return data.AsList();
        }

        public Tip GetTip(int ID)
        {
            var sql = "SELECT * FROM Tips WHERE ID = @ID";
            return connection.QuerySingleOrDefault<Tip>(sql, new { ID });
        }

        public void UpdateTip(Tip tip)
        {
            var sql = "UPDATE Tips SET StackTechID = @StackTechID, TituloTip = @TituloTip, " +
                      "DescripcionTip = @DescripcionTip, CodigoTip = @CodigoTip WHERE ID = @ID";
            connection.Execute(sql, tip);
        }

        public void UpdateTip(int ID, int stackTechID, string titulo, string descripcion, string codigo)
        {
            var tip = new Tip
            {
                ID = ID,
                StackTechID = stackTechID,
                TituloTip = titulo,
                DescripcionTip = descripcion,
                CodigoTip = codigo
            };
            var sql = "UPDATE Tips SET StackTechID = @StackTechID, TituloTip = @TituloTip, " +
                      "DescripcionTip = @DescripcionTip, CodigoTip = @CodigoTip WHERE ID = @ID";
            connection.Execute(sql, tip);
        }

        public void DeleteTip(Tip tip)
        {
            var sql = "DELETE FROM Tips WHERE ID = @ID";
            connection.Execute(sql, new { tip.ID });
        }

        public void DeleteTip(int ID)
        {
            var sql = "DELETE FROM Tips WHERE ID = @ID";
            connection.Execute(sql, new { ID });
        }
    }
}
