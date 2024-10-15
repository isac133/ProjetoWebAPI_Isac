using System;
using System.Collections.Generic;

namespace WebApiCurso.ORM;

public partial class TbCliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public byte[]? DocDocumentacao { get; set; }

    public virtual TbEndereco? TbEndereco { get; set; }

    public virtual TbVenda? TbVenda { get; set; }
}
