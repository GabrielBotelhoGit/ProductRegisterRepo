using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRegister.Models
{
    public class Product
    {
        #region Atributos privados
        private int _Codigo;
        private string _Descricao;
        private string _Marca;
        private double _Preco;
        private DateTime _DataCadastro;
        private DateTime _DataLancamento;
        private int _TipoProduto;
        #endregion
        #region Atributos Publicos
        public int Codigo { get { return this._Codigo; } set { this._Codigo = value; } }
        public string Descricao { get { return this._Descricao; } set { this._Descricao = value; } }
        public string Marca { get { return this._Marca; } set { this._Marca = value; } }
        public double Preco { get { return this._Preco; } set { this._Preco = value; } }
        public DateTime DataCadastro { get { return this._DataCadastro; } set { this._DataCadastro = value; } }
        public DateTime DataLancamento { get { return this._DataLancamento; } set { this._DataLancamento = value; } }
        public int TipoProduto { get { return this._TipoProduto; } set { this._TipoProduto = value; } }
        #endregion

        #region Metodos Públicos
        static public List<Product> ReturnProductsFromDataTable(DataTable dataTable)
        {
            List<Product> products = new List<Product>();
            foreach(DataRow dr in dataTable.Rows)
            {
                Product product = new Product();
                product.Codigo = Convert.ToInt32(dr["Codigo"]);
                product.DataCadastro = Convert.ToDateTime(dr["DataCadastro"]);
                product.DataLancamento = Convert.ToDateTime(dr["DataLancamento"]);
                product.Descricao = dr["Descricao"].ToString();
                product.Marca = dr["Marca"].ToString();
                product.Preco = Convert.ToInt32(dr["Preco"]);
                product.TipoProduto = Convert.ToInt32(dr["TipoProduto"]);
                products.Add(product);
            }
            return products;
        }
        #endregion
    }
}
