using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Digital.Lib.Net.Entities.Models;
using Digital.Lib.Net.Entities.Models.Documents;
using Microsoft.EntityFrameworkCore;

namespace Digital.Pages.Api.Data.FramesConfig;

[Table("FrameConfig"), Index(nameof(Version), IsUnique = true)]
public class FrameConfig : EntityId
{
    [Column("DocumentId"), Required]
    public Guid DocumentId { get; set; }

    [Column("Version"), Required, MaxLength(24)]
    public string Version { get; set; }

    [Column("IsPublished"), Required]
    public bool IsPublished { get; set; }
}