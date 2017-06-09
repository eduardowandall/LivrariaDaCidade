<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LivrariaDaCidade._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Lista de Livros</h2>
    <asp:LinkButton ID="btnAdicionar" PostBackUrl="~/Livro.aspx" ToolTip="Adicionar um novo livro" runat="server" CssClass="btn btn-primary">
                        <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
    </asp:LinkButton>
    <asp:Repeater ID="rptLivros" runat="server">
        <HeaderTemplate>
            <div class="row lista-livros-header">
                <div class="col-sm-4">
                    Nome
                </div>
                <div class="col-sm-4">
                    Autor(es)
                </div>
                <div class="col-sm-2">
                    Valor
                </div>
                <div class="col-sm-2">
                    Opções
                </div>
            </div>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="row lista-livros-row">
                <div class="col-sm-4">
                    <%# Eval("NOME") %>
                </div>
                <div class="col-sm-4">
                    <%# string.Join(", ",((List<string>)Eval("AutoresNomes"))) %>
                </div>
                <div class="col-sm-2">
                    <%# Eval("VALOR") %>
                </div>
                <div class="col-sm-2">
                    <a href="Livro.aspx?Id=<%# Eval("ID") %>">Detalhes</a>
                </div>
            </div>
        </ItemTemplate>
        <SeparatorTemplate>
            <div class="separator"></div>
        </SeparatorTemplate>
    </asp:Repeater>
</asp:Content>
