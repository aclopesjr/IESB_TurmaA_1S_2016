﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Model
{
    [SQLite.Net.Attributes.Table(nameof(Contato))]
    public class Contato
    {
        [SQLite.Net.Attributes.Column(nameof(ID)),
        SQLite.Net.Attributes.PrimaryKey,
        SQLite.Net.Attributes.AutoIncrement]
        public int? ID { get; set; }

        [SQLite.Net.Attributes.MaxLength(100),
        SQLite.Net.Attributes.NotNull]
        public string Nome { get; set; }

        [SQLite.Net.Attributes.MaxLength(100)]
        public string Email { get; set; }

        [SQLite.Net.Attributes.MaxLength(40)]
        public string Telefone { get; set; }

        public bool IsFavorito { get; set; }
    }
}