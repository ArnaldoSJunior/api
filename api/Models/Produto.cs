using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Produto
{  //construtor 
    public Produto()
    {
        Id = Guid.NewGuid().ToString();
        Criadoem = DateTime.Now;
    }

    public Produto(string nome, string descricao, double valor)
    {
        Id = Guid.NewGuid().ToString();
        Nome = nome;
        Descricao = descricao;
        Valor = valor;
        Criadoem = DateTime.Now;
    }

    //Atributos ou propriedades = caracteristicas de um objeto
    public string Id { get; set; }

    //anotação (Data Annotetions)
    [Required(ErrorMessage = "Campo obrigatório!")]
    public string? Nome { get; set; }


    [MinLength(3, ErrorMessage = "Mínimo de caracteres 3!")]
    [MaxLength(10, ErrorMessage = "Máximo de caracteres 10!")]
    public string? Descricao { get; set; }

    [Range(1, 1000, ErrorMessage = "Valor entre R$1 e R$1000!")]
    public double Valor { get; set; }

    public DateTime Criadoem { get; set; }

    public int Quantidade { get; set; }

}
/*  private string nome;


  public void setNome(string nome)
  {
      this.nome = nome;
  }
  public string getNome()
  {
      return this.nome;
  }*/






