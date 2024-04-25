using System.ComponentModel.DataAnnotations;
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

    List<ValidationResult> erros = new List<ValidationResult>();
    if (!Validator.TryValidateObject(
        produto, new ValidationContext(produto), erros, true))
    {
        return Results.BadRequest(erros);
    }

    Produto? produtoBuscado = ctx.Produtos.FirstOrDefault
    (x => x.Nome == produto.Nome);
    if (produtoBuscado is null)
    {
        //Adcionar obj dentro da tabela no banco de dados
        ctx.Produtos.Add(produto);
        ctx.SaveChanges();
        return Results.Created("", produto);

    }

    return Results.BadRequest("Já existe produto com o emso nome!");
});

// DELETE http://localhost:5169/produtos/deletar/id
app.MapDelete("/produtos/deletar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Produto? produto = ctx.Produtos.FirstOrDefault(x => x.Id == id);

    if (produto is null)
    {

        return Results.NotFound("Produto não encontrado");
    }
    ctx.Produtos.Remove(produto);
    ctx.SaveChanges();
    return Results.Ok("Produto removido com sucesso!!");
});

// PATCH http://localhost:5169/produtos/alterar/id
app.MapPatch("/produtos/alterar/{id}", ([FromRoute] string id, [FromBody] Produto novoProduto,
[FromServices] AppDataContext ctx) =>
{
    var produto = ctx.Produtos.Find(id);
    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado");

    }
    produto.Nome = novoProduto.Nome;
    produto.Descricao = novoProduto.Descricao;
    produto.Valor = novoProduto.Valor;

    ctx.SaveChanges();
    return Results.Ok("Produto atualizado com suscesso!!");
});


app.Run();



