using System.ComponentModel.DataAnnotations;

namespace YoussofPortfolio.Client.Models;

public class Project
{
    [Key]
    public string? Title { get; set; }
    public string? Details { get; set; }
    public string? Image { get; set; }
    public string? Link { get; set; }
    public string? Linkr { get; set; }
    public string? Alt { get; set; }
    public List<string>? Tags { get; set; }
}