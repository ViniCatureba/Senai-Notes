using System;
using System.Collections.Generic;

namespace Senai_Notas.Models;

public partial class Nota
{
    public int IdNota { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdAnexo { get; set; }

    public int? IdHistoNota { get; set; }

    public string Titulo { get; set; } = null!;

    public string Conteudo { get; set; } = null!;

    public DateTime UltimoRefresh { get; set; }

    public DateTime DataCriacao { get; set; }

    public virtual Anexo? IdAnexoNavigation { get; set; }

    public virtual HistoricoNotum? IdHistoNotaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<NotaTag> NotaTags { get; set; } = new List<NotaTag>();
}
