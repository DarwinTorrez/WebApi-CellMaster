using System;
using System.Collections.Generic;

namespace CellMasterAPI.Models;

public partial class Vfactura
{
    public int IdFactura { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public string? Estado { get; set; }

    public int? IdUsuarios { get; set; }

    public int IdClientes { get; set; }

    public int IdPersona { get; set; }

    public string Cedula { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public int Idclient { get; set; }

    public string? NombreTrabajador { get; set; }

    public string? Apellidotrabjador { get; set; }

    public string Cargo { get; set; } = null!;

    public int IdDetalleFactura { get; set; }

    public int? IdtipoCambio { get; set; }

    public decimal? PrecioCambio { get; set; }
}
