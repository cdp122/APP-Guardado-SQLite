using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using APP2Tips.Entities;
using Dapper;
using Microsoft.Data.Sqlite;

namespace APP2Tips.Managers
{
    /// <summary>
    /// Esta es la clase DAO o Manager que permite las instrucciones CRUD (Create, Read, Update, Delete) 
    /// para la entidad StackTech en la BDD SQLite con Dapper.
    /// </summary>
    internal class StackTechs
    {
        private string connectionString;
        private SqliteConnection connection;

        public StackTechs()
        {
            // Añadir using System.IO arriba
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "tips.db");

            // Solo mientras estés en desarrollo: elimina la DB para forzar recreación
            bool forceReset = false; // cambia a false para no borrar
            if (forceReset && File.Exists(dbPath)) {
                File.Delete(dbPath);
                forceReset = false; // Solo queremos borrar una vez, no cada vez que se inicie la clase 
            }

            connectionString = $"Data Source={dbPath}";
            connection = new SqliteConnection(connectionString);
            connection.Open();

            // Crear la tabla StackTech si no existe. Usar nombres de columna que coincidan con la entidad
            connection.Execute(@"
                CREATE TABLE IF NOT EXISTS StackTech (
                ID INTEGER PRIMARY KEY AUTOINCREMENT, 
                NombreStack TEXT, 
                DescripcionStack TEXT)");
        }

        public StackTech CreateStack(StackTech stackTech)
        {
            var sql =
                "INSERT INTO StackTech (NombreStack, DescripcionStack) VALUES " +
                "(@NombreStack, @DescripcionStack); SELECT last_insert_rowid();";
            stackTech.ID = connection.ExecuteScalar<int>(sql, stackTech);
            return stackTech;
        }

        public StackTech CreateStack(string nombre, string descripcion)
        {
            var stackTech = new StackTech
            {
                NombreStack = nombre,
                DescripcionStack = descripcion
            };
            var sql =
                "INSERT INTO StackTech (NombreStack, DescripcionStack) VALUES " +
                "(@NombreStack, @DescripcionStack); SELECT last_insert_rowid();";
            stackTech.ID = connection.ExecuteScalar<int>(sql, stackTech);
            return stackTech;
        }

        public List<StackTech> GetAllStack()
        {
            var data = connection.Query<StackTech>("SELECT * FROM StackTech");
            return data.ToList();
        }

        public StackTech GetStack(int id)
        {
            var data = connection.QueryFirstOrDefault<StackTech>(
                "SELECT * FROM StackTech WHERE ID = @ID", new { ID = id });

            return data;
        }

        public void UpdateStack(StackTech stackTech)
        {
            var sql = "UPDATE StackTech SET NombreStack = @NombreStack, DescripcionStack = @DescripcionStack WHERE ID = @ID";
            connection.Execute(sql, stackTech);
        }

        public void UpdateStack(int id, string nombre, string descripcion)
        {
            var sql = "UPDATE StackTech SET NombreStack = @NombreStack, DescripcionStack = @DescripcionStack WHERE ID = @ID";
            connection.Execute(sql, new { ID = id, NombreStack = nombre, DescripcionStack = descripcion });
        }

        public void DeleteStack(StackTech stackTech)
        {
            var sql = "DELETE FROM StackTech WHERE ID = @ID";
            connection.Execute(sql, new { ID = stackTech.ID });
        }

        public void DeleteStack(int id)
        {
            var sql = "DELETE FROM StackTech WHERE ID = @ID";
            connection.Execute(sql, new { ID = id });
        }
    }
}
