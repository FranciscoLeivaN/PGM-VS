using System;
using System.Collections.Generic;

namespace PGM_ORM.Models;

public partial class Solicitude
{
    public int IdSolicitud { get; set; }

    public string NombreSolicitud { get; set; } = null!;

    public string ApellidosSolicitud { get; set; } = null!;

    public string RunSolicitud { get; set; } = null!;

    public string? TelefonoSolicitud { get; set; }

    public string CorreoSolicitud { get; set; } = null!;

    public DateTime FechaSolicitud { get; set; }

    public string? DetalleSolicitud { get; set; }

    public string Servicio { get; set; } = null!;

    public int SolicitudUsuario { get; set; }

    public virtual Usuario SolicitudUsuarioNavigation { get; set; } = null!;
}
