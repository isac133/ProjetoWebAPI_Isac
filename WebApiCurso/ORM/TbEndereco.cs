using System;
using System.Collections.Generic;

namespace WebApiCurso.ORM;

public partial class TbEndereco
{
    public int Id { get; set; }

    public string Logradouro { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Cep { get; set; } = null!;

    public string Pontodereferencia { get; set; } = null!;

    public int Nº { get; set; }

    public int FkCliente { get; set; }

    public virtual TbCliente IdNavigation { get; set; } = null!;
}
