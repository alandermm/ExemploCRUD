using System;

namespace ExemploCRUD
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}