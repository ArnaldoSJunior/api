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
    public string? Nome { get; set; }
    public string? Descricao { get; set; }

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






