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
    class Consulta
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        public string Nome
        {
            get;
            set;
        }
        public string Data
        {
            get;
            set;
        }
        public string Horario
        {
            get;
            set;
        }
        public string Endereco
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("[Consulta: Nome={0}, Data={1}, Horario={2}, Endereco={3}]", Nome, Data, Horario, Endereco);
        }
    }
}