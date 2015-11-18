using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite.Net.Attributes;

namespace DiaCont
{
    class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        public string Senha
        {
            get;
            set;
        }

        public string Nome
        {
            get;
            set;
        }
        public int Idade
        {
            get;
            set;
        }
        public float Altura
        {
            get;
            set;
        }
        public float Peso
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("[Usuario: Nome={0}, Senha={1}]", Nome, Senha);
        }
    }
}