using System;
using System.Collections.Generic;

namespace WebApiCurso.ORM;

public partial class TbVenda
{
    public int Id { get; set; }

    public decimal Valor { get; set; }

    public byte[]? NotaFiscal { get; set; }

    public int FkProduto { get; set; }

    public int FkCliente { get; set; }

    public virtual TbProduto Id1 { get; set; } = null!;

    public virtual TbCliente IdNavigation { get; set; } = null!;
}
