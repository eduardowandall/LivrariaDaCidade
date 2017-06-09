<%@ Page Title="Livro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Livro.aspx.cs" Inherits="LivrariaDaCidade.Livro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function updateModalAutores() {
            var updatePanel = '<%=upAutores.ClientID%>';
            updateModal(updatePanel);
        }
        function updateModalEditoras() {
            var updatePanel = '<%=upEditoras.ClientID%>';
            updateModal(updatePanel);
        }
        function updateModal(updatePanel) {
            if (updatePanel != null) {
                __doPostBack(updatePanel, 'OpenModal');
            }
        }
    </script>
    <div class="row">
        <div class="col-md-8 col-md-offset-2">
            <div id="divAlert" visible="false" class="alert alert-success alert-dismissable" runat="server">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <asp:Label ID="lblAlert" runat="server" />
            </div>
            <div class="panel panel-default panel-body">
                <div class="pull-right">
                    <asp:LinkButton ID="btnAdicionar" PostBackUrl="~/Livro.aspx" ToolTip="Adicionar um novo livro" runat="server" CssClass="btn btn-primary">
                        <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnExcluir" OnClick="btnExcluir_Click"
                        ToolTip="Apaga o registro desse livro" runat="server" CssClass="btn btn-primary">
                        <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                    </asp:LinkButton>
                </div>

                <div class="form-group">
                    <label for="txtNomeLivro">Nome</label>
                    <asp:TextBox ID="txtNomeLivro" Width="80%" MaxLength="150"
                        ValidationGroup="CadastroLivro" CssClass="form-control " runat="server" />
                    <asp:RequiredFieldValidator ControlToValidate="txtNomeLivro" Display="Dynamic"
                        Text="O nome do livro é obrigatório." ValidationGroup="CadastroLivro" runat="server" />
                </div>
                <div class="form-group">
                    <label for="txtNomeAutores">Autor(es)</label>
                    <div class="row">
                        <div class="col-sm-2">
                            <asp:Button ID="btnPesquisarAutores" Text="Selecionar" runat="server" OnClientClick='updateModalAutores()'
                                CssClass="btn btn-primary" data-toggle="modal" data-target="#modalAutores" />
                        </div>
                        <div class="col-sm-10">
                            <asp:HiddenField ID="hfIdAutores" runat="server" />
                            <asp:TextBox ID="txtNomeAutores" Width="100%" ValidationGroup="CadastroLivro" CssClass="form-control" ReadOnly="true"
                                ForeColor="GrayText" MaxLength="150" runat="server" />
                            <asp:RequiredFieldValidator ControlToValidate="txtNomeAutores" Display="Dynamic" runat="server"
                                Text="Você precisa selecionar o(s) autor(es) do livro." ValidationGroup="CadastroLivro" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtNomeEditora">Editora</label>
                    <div class="row">
                        <div class="col-sm-2">
                            <asp:Button ID="btnPesquisarEditoras" Text="Selecionar" runat="server" OnClientClick='updateModalEditoras()'
                                CssClass="btn btn-primary" data-toggle="modal" data-target="#modalEditoras" />
                        </div>
                        <div class="col-sm-10">
                            <asp:HiddenField ID="hfIdEditora" runat="server" />
                            <asp:TextBox ID="txtNomeEditora" Width="100%" CssClass="form-control" ValidationGroup="CadastroLivro"
                                ReadOnly="true" MaxLength="150" ForeColor="GrayText" runat="server" />
                            <asp:RequiredFieldValidator ControlToValidate="txtNomeEditora" runat="server" Display="Dynamic"
                                Text="Você precisa selecionar a editora do livro." ValidationGroup="CadastroLivro" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtValor">Valor</label>
                    <asp:TextBox ID="txtValor" TextMode="Number" step="any" Width="100px" ValidationGroup="CadastroLivro" CssClass="form-control" runat="server" />
                    <asp:RequiredFieldValidator ControlToValidate="txtValor" runat="server" Display="Dynamic"
                        Text="Digite um valor para o livro." ValidationGroup="CadastroLivro" />
                </div>
                <div class="form-group">
                    <label for="txtSinopse">Sinopse</label>
                    <asp:TextBox ID="txtSinopse" CssClass="form-control" Width="100%" Height="150px" MaxLength="2000" TextMode="MultiLine" runat="server" />
                </div>
                <div>
                    <asp:Button ID="btnSalvarLivro" Text="Salvar" OnClick="btnSalvarLivro_Click" ValidationGroup="CadastroLivro" CssClass="btn btn-success" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div id="modalAutores" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Autores</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="upAutores" UpdateMode="Conditional" ChildrenAsTriggers="false" OnLoad="upAutores_Load">
                        <ContentTemplate>
                            <div class="input-group">
                                <asp:TextBox ID="txtAdicionarAutor" placeholder="Nome do Autor" CssClass="form-control" runat="server" />
                                <div class="input-group-addon">
                                    <asp:LinkButton ID="btnAdicionarNovoAutor" Text="Adicionar" OnClick="btnAdicionarNovoAutor_Click" runat="server" />
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <asp:Repeater ID="rptAutores" runat="server">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfIdAutor" Value='<%# Eval("Id") %>' runat="server" />
                                        <div style="padding: 7px;">
                                            <asp:CheckBox ID="ChkAutor" Checked='<%# Eval("Selecionado") %>' Text='<%# Eval("Nome") %>' runat="server" />
                                        </div>
                                    </ItemTemplate>
                                    <SeparatorTemplate>
                                        <div class="separator"></div>
                                    </SeparatorTemplate>
                                </asp:Repeater>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnSelecionarAutores" CssClass="btn btn-primary" Text="Selecionar Autores" OnClick="btnSelecionarAutores_Click" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div id="modalEditoras" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Editoras</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="upEditoras" UpdateMode="Conditional" OnLoad="upEditoras_Load">
                        <ContentTemplate>
                            <div class="input-group">
                                <asp:TextBox ID="txtAdicionarEditora" Width="100%" placeholder="Nome da Editora" CssClass="form-control" runat="server" />
                                <div class="input-group-addon">
                                    <asp:LinkButton ID="btnAdicionarEditora" Text="Adicionar" OnClick="btnAdicionarEditora_Click" runat="server" />
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <asp:Repeater ID="rptEditoras" runat="server">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfIdEditora" Value='<%# Eval("Id") %>' runat="server" />
                                        <div style="padding: 7px;">
                                            <asp:RadioButton ID="RdnEditora" GroupName="RadiosEditora" Text='<%# Eval("Nome") %>' runat="server" />
                                        </div>
                                    </ItemTemplate>
                                    <SeparatorTemplate>
                                        <div class="separator"></div>
                                    </SeparatorTemplate>
                                </asp:Repeater>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:Button CssClass="btn btn-primary" Text="Selecionar Editora" OnClick="SelecionarEditora_Click" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
