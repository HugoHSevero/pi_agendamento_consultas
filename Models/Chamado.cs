using System;
using System.ComponentModel.DataAnnotations;

public class Chamado
{
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; }

    [Required]
    public string Descricao { get; set; }

    [Required]
    public CategoriaChamado Categoria { get; set; }

    public StatusChamado Status { get; set; }
    
    public PrioridadeChamado Prioridade { get; set; }

    public DateTime DataCriacao { get; set; }

    public string UsuarioId { get; set; }
    public ApplicationUser Usuario { get; set; }
    
    public List<MensagemChamado> Mensagens { get; set; }
}