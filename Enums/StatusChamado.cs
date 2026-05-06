using System.ComponentModel.DataAnnotations;

public enum StatusChamado
{
    [Display(Name = "Aberto")]
    Aberto = 1,
    
    [Display(Name = "Em andamento")]
    EmAndamento = 2,
    
    [Display(Name = "Concluído")]
    Concluido = 3
}