using System;
using System.Collections.Generic;

namespace Senai_Notas.Models;

public partial class HistoricoNotum
{
    public int IdHistoNota { get; set; }

    public DateTime UltimoReflesh { get; set; }

    public string ConteudoAnterior { get; set; } = null!;

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
