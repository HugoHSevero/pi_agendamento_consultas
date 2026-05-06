using System.ComponentModel.DataAnnotations;

public enum CategoriaChamado
{
    [Display(Name = "Banco de Dados")]
    BancoDeDados = 1,
    
    Infraestrutura = 2,
    Sistema = 3,
    
    [Display(Name = "Manutenção")]
    Manutencao = 4
}