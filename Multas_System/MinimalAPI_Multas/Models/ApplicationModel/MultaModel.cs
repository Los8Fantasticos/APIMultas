﻿namespace MinimalAPI_Multas.Models.ApplicationModel
{
    public class MultaModel
    {
        public int IdMulta { get; set; }
        public string Patente { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha { get; set; }
        public bool Active { get; set; }
    }
}
