using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
//registrar o serviço de banco de dados 
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

Produto produto = new Produto();

//List<Produto> produtos = new List<Produto>();
var produtos = new List<Produto>();
produtos.Add(new Produto("Celular", "IOS", 20));
produtos.Add(new Produto("Celular", "Android", 50));
produtos.Add(new Produto("Tv", "LG", 60));
produtos.Add(new Produto("Tv", "Samsung", 90));

//funcionalidade da aplicação -EndPoints
//GET: http://localhost:5054/produtos/listar
app.MapGet("/produtos/listar", ([FromServices] AppDataContext ctx)
 =>
 {

     if (ctx.Produtos.Any())
     {
         return Results.Ok(ctx.Produtos.ToList());

     }
     return Results.NotFound("não existem produtos na tabela");
 }
 );

//GET:http://localhost:5054/buscar/iddoproduto
app.MapGet("/produtos/buscar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Produto? produto = ctx.Produtos.Find(id);
    if (produto == null)
    {
        return Results.NotFound("Não encontrado!");
    }
    return Results.Ok(produto);
});

//GET:http://localhost:5054
app.MapGet("/", () => "API de produtos");

//POST:http://localhost:5054/produtos/cadastrar
//cadastrar produtos dentro da lista
/*app.MapPost("/produtos/cadastrar/{nome}/{descricao}/{valor}", ([FromRoute] string nome, [FromRoute] string descricao,
 [FromRoute] double valor) => {

//preencher o objeto pelo contrutor
 Produto produto = new Produto(nome, descricao, valor);

//preencher o objeto pelos atributos
produto.Nome = nome;
produto.Descricao = descricao;
produto.Valor = valor;

//Adicionar o objeto dentro da lista 
produtos.Add(produto);
return Results.Created("", produto);


});*/

app.MapPost("/produtos/cadastrar", ([FromBody] Produto produto,
[FromServices] AppDataContext ctx) =>
{
    ctx.Produtos.Add(produto);
    ctx.SaveChanges();
    return Results.Created("", produto);
});

app.MapDelete("/produtos/excluir/{nome}", ([FromRoute] string nome) =>
{

    int index = produtos.FindIndex(p => p.Nome == nome);


    if (index >= 0)
    {
        produtos.RemoveAt(index);
        return Results.Ok($"Produto '{nome}' excluído com sucesso.");
    }
    else
    {
        return Results.NotFound($"Produto '{nome}' não encontrado.");
    }
});
app.MapPatch("/produtos/alterar/{nome}/{preco}", ([FromRoute] string nome, [FromRoute] double preco) =>
{
    for (int i = 0; i < produtos.Count; i++)
    {
        if (produtos[i].Nome == nome)
        {
            produtos[i].Valor = preco;
            return Results.Ok("Produto alterado");
        }


    }
    return Results.NotFound("produto não encontrado");
});




//Exercicios
//1. cadastrar um produto
//2. pela url
//3. pelo corpo
//4. remoção do produto
//5.alteração de produto

app.Run();



