using System;
using System.ComponentModel.DataAnnotations;

namespace Agendamentos.Models;

public class Medico
{
    public int Id { get; set; }
    
    [Required]
    [MinLength(3)]
    public string Nome { get; set; }
    
    [Required]
    public string Especialidade { get; set; }
    
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }
    
    [MaxLength(15)]
    public string Telefone { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
}