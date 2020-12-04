using ProductRegister.Contexts;
using ProductRegister.DbAccess;
using ProductRegister.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRegister.Services
{
    public class ProductService
    {
        private readonly ProductContext _productContext;
        private readonly EntityAccess _entityAccess;

        public ProductService(ProductContext productContext)
        {
            this._productContext = productContext;
        }

        public ProductService(EntityAccess entityAccess)
        {
            this._entityAccess = entityAccess;
        }

        public List<Product> Find()
        {
            List<Product> products = new List<Product>();
            products = _productContext.Product.ToList<Product>();
            return products;
        }

        public List<Product> FindOdbc()
        {
            List<Product> products = new List<Product>();
            string query = string.Empty;
            query = "Select * from Product";
            List<QueryParameter> queryParameters = new List<QueryParameter>();            
            DataTable dataTable = _entityAccess.ExecNativeQuery(query, queryParameters);
            products = Product.ReturnProductsFromDataTable(dataTable);
            return products;
        }        

        public Product FindById(int codigo)
        {
            Product product = new Product();
            product = _productContext.Product.Single(x => x.Codigo == codigo);
            return product;
        }

        public Product FindByIdOdbc(int codigo)
        {
            Product product = new Product();
            List<Product> products = new List<Product>();
            string query = string.Empty;
            query = "Select * from Product where Product.Codigo = @Codigo;";

            List<QueryParameter> queryParameters = new List<QueryParameter>();
            queryParameters.Add(new QueryParameter("@Codigo", codigo));

            DataTable dataTable = _entityAccess.ExecNativeQuery(query, queryParameters);
            products = Product.ReturnProductsFromDataTable(dataTable);
            if (products.Count > 0)
            {
                product = products[0];
            }
            return product;
        }

        public void AddProduct(Product product)
        {            
            _productContext.Add<Product>(product);
            _productContext.SaveChanges();
        }

        public void AddProductOdbc(Product product)
        {
            string query = string.Empty;
            query = " Insert into Product " +
                    " (Descricao, Marca, Preco, DataCadastro, DataLancamento, TipoProduto) " +
                    " Values(@Descricao, @Marca, @Preco, @DataCadastro, @DataLancamento, @TipoProduto); ";

            List<QueryParameter> queryParameters = new List<QueryParameter>();
            queryParameters.Add(new QueryParameter("@Descricao", product.Descricao));
            queryParameters.Add(new QueryParameter("@Marca", product.Marca));
            queryParameters.Add(new QueryParameter("@Preco", product.Preco));
            queryParameters.Add(new QueryParameter("@DataCadastro", product.DataCadastro));
            queryParameters.Add(new QueryParameter("@DataLancamento", product.DataLancamento));
            queryParameters.Add(new QueryParameter("@TipoProduto", product.TipoProduto));

            _entityAccess.ExecNativeQuery(query, queryParameters);            
        }

        public void UpdateProduct(Product product)
        {            
            _productContext.Update<Product>(product);
            _productContext.SaveChanges();
        }

        public void UpdateProductOdbc(Product product)
        {
            string query = string.Empty;
            query = " Update Product set " +
                    " Descricao = @Descricao, " +
                    " Marca = @Marca, " +
                    " Preco = @Preco, " +
                    " DataCadastro = @DataCadastro, " +
                    " DataLancamento = @DataLancamento, " +
                    " TipoProduto = @TipoProduto " +
                    " Where Product.Codigo = @Codigo; ";                    

            List<QueryParameter> queryParameters = new List<QueryParameter>();
            queryParameters.Add(new QueryParameter("@Codigo", product.Codigo));
            queryParameters.Add(new QueryParameter("@Descricao", product.Descricao));
            queryParameters.Add(new QueryParameter("@Marca", product.Marca));
            queryParameters.Add(new QueryParameter("@Preco", product.Preco));
            queryParameters.Add(new QueryParameter("@DataCadastro", product.DataCadastro));
            queryParameters.Add(new QueryParameter("@DataLancamento", product.DataLancamento));
            queryParameters.Add(new QueryParameter("@TipoProduto", product.TipoProduto));

            _entityAccess.ExecNativeQuery(query, queryParameters);
        }

        public void DeleteProduct(int codigo)
        {
            Product produto = new Product();
            produto = _productContext.Product.Single(x => x.Codigo == codigo);            
            _productContext.Remove(produto);
            _productContext.SaveChanges();
        }

        public void DeleteProductOdbc(int codigo)
        {
            string query = string.Empty;
            query = "Delete from Product where Product.Codigo = @Codigo;";

            List<QueryParameter> queryParameters = new List<QueryParameter>();
            queryParameters.Add(new QueryParameter("@Codigo", codigo));

            _entityAccess.ExecNativeQuery(query, queryParameters);            
        }
    }
}
