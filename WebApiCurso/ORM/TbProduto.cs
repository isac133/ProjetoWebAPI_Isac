using System;
using System.Collections.Generic;

namespace WebApiCurso.ORM;

public partial class TbProduto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public int Quanto { get; set; }

    public byte[]? NotaFiscal { get; set; }

    public virtual TbVenda? TbVenda { get; set; }
}
