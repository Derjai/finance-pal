using System;
using System.Data.SQLite;
using System.IO;

namespace finance_pal
{
    public static class DatabaseInitializer
    {
        public static void Initialize() { 
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Finance-Pal.db");
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                using (var connection= new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();
                    string createTables = @"
                        CREATE TABLE IF NOT EXISTS Transacciones (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Tipo TEXT NOT NULL,
                            Categoría TEXT NOT NULL,
                            Concepto TEXT NOT NULL,
                            Monto REAL NOT NULL,
                            Presupuesto REAL NOT NULL,
                            Fecha TEXT NOT NULL
                        );
                        CREATE TABLE IF NOT EXISTS Configuraciones (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Fecha TEXT NOT NULL,
                            PorcentajeGastos REAL NOT NULL,
                            PorcentajeAhorros REAL NOT NULL,
                            PorcentajeOcio REAL NOT NULL
                        );
                        CREATE TABLE IF NOT EXISTS Deudas (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nombre TEXT NOT NULL,
                            Monto REAL NOT NULL,
                            FechaPago TEXT NOT NULL,
                            Estado BOOLEAN NOT NULL
                        );
                        CREATE TABLE IF NOT EXISTS Objetivos (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nombre TEXT NOT NULL,
                            Monto REAL NOT NULL,
                            FechaMeta TEXT NOT NULL,
                            Estado BOOLEAN NOT NULL
                        );
                        CREATE TABLE IF NOT EXISTS AbonosDeudas (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            DeudaId INTEGER NOT NULL,
                            Monto REAL NOT NULL,
                            Fecha TEXT NOT NULL,
                            FOREIGN KEY (DeudaId) REFERENCES Deudas(Id) ON DELETE CASCADE
                        );
                        CREATE TABLE IF NOT EXISTS AbonosObjetivos (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            ObjetivoId INTEGER NOT NULL,
                            Monto REAL NOT NULL,
                            Fecha TEXT NOT NULL,
                            FOREIGN KEY (ObjetivoId) REFERENCES Objetivos(Id) ON DELETE CASCADE
                        );
                    ";
                    using (var command = new SQLiteCommand(createTables, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
