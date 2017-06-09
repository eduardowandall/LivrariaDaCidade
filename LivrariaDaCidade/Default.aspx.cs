using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LivrariaDaCidade
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var livros = new LivrariaDaCidade.Model.LivrariaDaCidadeDBEntities()
                .LIVRO.Select(livro => new
                {
                    livro.ID,
                    livro.NOME,
                    livro.VALOR,
                    AutoresNomes = livro.LIVRO_AUTOR.Select(x => x.AUTOR.NOME)
                }).OrderBy(x => x.NOME);
            rptLivros.DataSource = livros.ToList();
            rptLivros.DataBind();
        }
    }
}