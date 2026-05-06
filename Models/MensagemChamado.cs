using System;
using System.ComponentModel.DataAnnotations;

public class MensagemChamado
{
    public int Id { get; set; }

    [Required]
    public string Conteudo { get; set; }

    public DateTime DataEnvio { get; set; }

    public int ChamadoId { get; set; }
    public Chamado Chamado { get; set; }

    public string UsuarioId { get; set; }
    public ApplicationUser Usuario { get; set; }
}