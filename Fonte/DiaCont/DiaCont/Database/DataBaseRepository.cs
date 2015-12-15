/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiaCont.Models;
using SQLite.Net;
using SQLite.Net.Interop;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DiaCont.Database
{
    class DataBaseRepository
    {
        //Conexão a base de dados
        private SQLiteConnection dbConnection;

        //Mensagem que será retornada ao usuário
        public string Mensage { get; set; }

        //Construtor da casse para a criação da conexão
        //<param name="sqliteP">Plataforma - ios, android ou winPhone</param>
        //<param name="dbPath">Caminho onde está o arquivo db3</param>
        public DataBaseRepository(ISQLitePlatform sqliteP, string dbPath)
        {
            if (dbConnection == null)
            {
                dbConnection = new SQLiteConnection(sqliteP, dbPath);
                //Criação das tabelas do sistema
                dbConnection.CreateTable<Usuario>();
                dbConnection.CreateTable<Consulta>();
            }
        }

        public void Add_Usuario(Usuario entidadeNova)
        {
            int res = 0;

            try
            {
                res = dbConnection.Insert(entidadeNova);
                Mensage = string.Format("{0} registro(s)", res);
            }
            catch (Exception e)
            {
                Mensage = string.Format("Falha ao Inserir a entidade {0}. Erro: {1}", entidadeNova.GetType().ToString(), e.Message);
            }
        }

        public void Add_Consulta(Consulta entidadeNova)
        {
            int res = 0;

            try
            {
                res = dbConnection.Insert(entidadeNova);
                Mensage = string.Format("{0} registro(s)", res);
            }
            catch (Exception e)
            {
                Mensage = string.Format("Falha ao Inserir a entidade {0}. Erro: {1}", entidadeNova.GetType().ToString(), e.Message);
            }
        }
        
        public void Delete_Usuario(Usuario entity)
        {
            dbConnection.Delete(entity);
        }

        public void Delete_Consulta(Consulta entity)
        {
            dbConnection.Delete(entity);
        }

        public void Update_Usuario(Usuario entity)
        {
            dbConnection.Update(entity);
        }

        public void Update_Consulta(Consulta entity)
        {
            dbConnection.Update(entity);
        }

        public IEnumerable<Usuario> Usuario_List
        {
            get
            {
                return dbConnection.Table<Usuario>().ToList();
            }
        }

        public IEnumerable<Consulta> Consulta_List
        {
            get
            {
                return dbConnection.Table<Consulta>().ToList();
            }
        }

    }
}

*/