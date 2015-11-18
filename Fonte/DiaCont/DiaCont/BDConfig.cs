using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DataManager
{
    public class BDConfig : SQLiteOpenHelper
    {
        // Nome do banco
        private const string BDName = "banco";
        // Versão do banco
        private const int DatabaseVersion = 1;

        public BDConfig(Context context) : base(context, BDName, null, DatabaseVersion) { }

        public override void OnCreate(SQLiteDatabase db)
        {
            // Cria sa tabelas
            db.ExecSQL(@"
                        CREATE TABLE IF NOT EXISTS Usuario (
                            Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nome            TEXT NOT NULL,
                            Senha           TEXT NOT NULL,
                            Idade           INTEGER NOT NULL,
                            Altura          REAL NOT NULL,
                            Peso            REAL NOT NULL);
                        CREATE TABLE IF NOT EXISTS Consulta (
                            Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nome            TEXT NOT NULL,
                            Data            TEXT NOT NULL,
                            Horario         TEXT NOT NULL,
                            Endereco        TEXT NOT NULL )");
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            if (oldVersion < 2)
            {
                //perform any database upgrade tasks for versions prior to  version 2              
            }
            if (oldVersion < 3)
            {
                //perform any database upgrade tasks for versions prior to  version 3
            }
        }

    }

    public class DatabaseUpdates
    {
        private BDConfig _helper;

        public void SetContext(Context context)
        {
            if (context != null)
            {
               _helper = new BDConfig(context);
            }
        }
    }
} 

