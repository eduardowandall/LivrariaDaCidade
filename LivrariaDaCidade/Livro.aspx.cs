using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LivrariaDaCidade
{
    public partial class Livro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]))
            {
                int idLivro = Convert.ToInt32(Request.QueryString["Id"]);
                PreencherTelaComDadosDoLivro(idLivro);
            }
        }

        private int ObterIDLivroDaURL()
        {
            int idLivro = 0;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]))
                idLivro = Convert.ToInt32(Request.QueryString["Id"]);
            return idLivro;
        }

        private void PreencherTelaComDadosDoLivro(int idLivro)
        {
            var livroEdicao = new LivrariaDaCidade.Model.LivrariaDaCidadeDBEntities()
           .LIVRO.Select(livro => new
           {
               Id = livro.ID,
               Nome = livro.NOME,
               Autores = livro.LIVRO_AUTOR.Select(x => x.AUTOR),
               Editora = livro.EDITORA,
               Valor = livro.VALOR,
               Sinopse = livro.SINOPSE
           })
           .FirstOrDefault(x => x.Id == idLivro);
            txtNomeLivro.Text = livroEdicao.Nome;
            txtNomeAutores.Text = string.Join(",", livroEdicao.Autores.Select(x => x.NOME));
            hfIdAutores.Value = string.Join(",", livroEdicao.Autores.Select(x => x.ID));
            txtNomeEditora.Text = livroEdicao.Editora.NOME;
            hfIdEditora.Value = livroEdicao.Editora.ID.ToString();
            txtValor.Text = livroEdicao.Valor.ToString();
            txtSinopse.Text = livroEdicao.Sinopse;
            Page.Title = livroEdicao.Nome;
        }
        private void PopularInstanciaComDadosDaTela(Model.LIVRO livro)
        {
            livro.NOME = txtNomeLivro.Text;
            livro.SINOPSE = txtSinopse.Text;
            livro.VALOR = Convert.ToDouble(txtValor.Text);
            livro.ID_EDITORA = Convert.ToInt32(hfIdEditora.Value);
            var autores = hfIdAutores.Value.Split(',');
            var livroAutores = new List<Model.LIVRO_AUTOR>();
            foreach (var idAutor in autores)
            {
                int id = Convert.ToInt32(idAutor);
                livroAutores.Add(new Model.LIVRO_AUTOR()
                {
                    //AUTOR = model.AUTOR.FirstOrDefault(x => x.ID == id),
                    ID_AUTOR = id,
                    LIVRO = livro
                });
            }
            livro.LIVRO_AUTOR = livroAutores;
        }



        #region Modal Autores

        protected void upAutores_Load(object sender, EventArgs e)
        {
            string parameter = Request["__EVENTARGUMENT"];
            if (parameter == "OpenModal")
            {
                AtualizarListaDeAutores(ObterIDLivroDaURL());
                upAutores.Update();
            }
        }

        private void AtualizarListaDeAutores(int idLivro)
        {
            var autores = new LivrariaDaCidade.Model.LivrariaDaCidadeDBEntities()
              .AUTOR.Select(autor => new
              {
                  Id = autor.ID,
                  Nome = autor.NOME,
                  Selecionado = autor.LIVRO_AUTOR.Any(x => x.ID_LIVRO == idLivro)
              });
            rptAutores.DataSource = autores.ToList();
            rptAutores.DataBind();
        }

        protected void btnAdicionarNovoAutor_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAdicionarAutor.Text))
            {
                using (var model = new LivrariaDaCidade.Model.LivrariaDaCidadeDBEntities())
                {
                    var autor = new LivrariaDaCidade.Model.AUTOR();
                    autor.NOME = txtAdicionarAutor.Text;
                    model.AUTOR.Add(autor);
                    model.SaveChanges();
                }
                txtAdicionarAutor.Text = "";
                AtualizarListaDeAutores(ObterIDLivroDaURL());
                upAutores.Update();
            }
        }

        protected void btnSelecionarAutores_Click(object sender, EventArgs e)
        {
            var autoresSelecionados = new Dictionary<int, string>();
            foreach (RepeaterItem item in rptAutores.Items)
            {
                var chk = (CheckBox)item.FindControl("ChkAutor");
                if (chk != null && chk.Checked)
                {
                    var hf = (HiddenField)item.FindControl("hfIdAutor");
                    int idAutor = Convert.ToInt32(hf.Value);
                    string nomeAutor = chk.Text;
                    autoresSelecionados.Add(idAutor, nomeAutor);
                }
            }
            hfIdAutores.Value = string.Join(",", autoresSelecionados.Keys);
            txtNomeAutores.Text = string.Join(",", autoresSelecionados.Values);
        }

        #endregion

        #region Modal Editoras

        protected void upEditoras_Load(object sender, EventArgs e)
        {
            string parameter = Request["__EVENTARGUMENT"];
            if (parameter == "OpenModal")
            {
                AtualizarListaDeEditoras();
                upEditoras.Update();
            }
        }

        private void AtualizarListaDeEditoras()
        {
            var editoras = new LivrariaDaCidade.Model.LivrariaDaCidadeDBEntities()
              .EDITORA.Select(autor => new
              {
                  Id = autor.ID,
                  Nome = autor.NOME,
              });
            rptEditoras.DataSource = editoras.ToList();
            rptEditoras.DataBind();
        }

        protected void SelecionarEditora_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptEditoras.Items)
            {
                var rdn = (RadioButton)item.FindControl("RdnEditora");
                if (rdn != null && rdn.Checked)
                {
                    var hf = (HiddenField)item.FindControl("hfIdEditora");
                    txtNomeEditora.Text = rdn.Text;
                    hfIdEditora.Value = hf.Value;
                    break;
                }
            }
        }

        protected void btnAdicionarEditora_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAdicionarEditora.Text))
            {
                using (var model = new LivrariaDaCidade.Model.LivrariaDaCidadeDBEntities())
                {
                    var editora = new LivrariaDaCidade.Model.EDITORA();
                    editora.NOME = txtAdicionarEditora.Text;
                    model.EDITORA.Add(editora);
                    model.SaveChanges();
                }
                txtAdicionarEditora.Text = "";
                AtualizarListaDeEditoras();
                upAutores.Update();
            }
        }

        #endregion

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            int idLivro = ObterIDLivroDaURL();
            if (idLivro > 0)
            {
                using (var model = new LivrariaDaCidade.Model.LivrariaDaCidadeDBEntities())
                {
                    var livro = model.LIVRO.FirstOrDefault(x => x.ID == idLivro);
                    if (livro != null)
                    {
                        model.LIVRO_AUTOR.RemoveRange(livro.LIVRO_AUTOR);
                        model.LIVRO.Remove(livro);
                        model.SaveChanges();
                    }
                }
            }
            Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath);
        }

        protected void btnSalvarLivro_Click(object sender, EventArgs e)
        {
            try
            {
                int idLivro = ObterIDLivroDaURL();
                using (var model = new LivrariaDaCidade.Model.LivrariaDaCidadeDBEntities())
                {
                    Model.LIVRO livro;
                    if (idLivro > 0)
                    {
                        livro = model.LIVRO.FirstOrDefault(x => x.ID == idLivro);
                        if (livro != null)
                        {
                            model.LIVRO_AUTOR.RemoveRange(livro.LIVRO_AUTOR);
                            PopularInstanciaComDadosDaTela(livro);
                            model.SaveChanges();
                        }
                    }
                    else
                    {
                        livro = new Model.LIVRO();
                        PopularInstanciaComDadosDaTela(livro);
                        livro.DATA_CADASTRO = DateTime.Now;
                        model.LIVRO.Add(livro);
                        model.LIVRO_AUTOR.AddRange(livro.LIVRO_AUTOR);
                        model.SaveChanges();
                    }
                }
                MostrarMsgSucesso("Livro foi salvo com sucesso!");
                LimparTela();
            }
            catch (Exception ex)
            {
                MostrarMsgErro("Um erro impediu que o livro fosse salvo corretamente. Mais detalhes: " + ex.Message);
            }
        }
        public void LimparTela()
        {
            txtNomeLivro.Text = "";
            txtNomeAutores.Text = "";
            hfIdAutores.Value = "";
            txtNomeEditora.Text = "";
            hfIdEditora.Value = "";
            txtValor.Text = "";
            txtSinopse.Text = "";
        }
        public void MostrarMsgSucesso(string msg)
        {
            divAlert.Visible = true;
            divAlert.Attributes["class"] = "alert alert-success alert-dismissable";
            lblAlert.Text = msg;
        }
        public void MostrarMsgErro(string msg)
        {
            divAlert.Visible = true;
            divAlert.Attributes["class"] = "alert alert-danger alert-dismissable";
            lblAlert.Text = msg;
        }
    }
}