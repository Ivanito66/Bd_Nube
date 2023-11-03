using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

/**
 * Pre: Este programa es un formulario web que muestra una tabla con datos de facturas almacenadas en un XML.
 * 
 * Pro: Utiliza unos filtros para que el usuario pueda filtar las facturas por dos campos.
 * También contiene dos botones, uno resetea los filtros para que vuelvan a aparecer todas las facturas
 * y el otro permite exportar las facturas que aparecen en pantalla a formato XLS.
 * 
 * 16/10/2023
 */
public class Factura
{
    public string ID { get; set; }
    public string FechaFactura { get; set; }
    public string CIFCliente { get; set; }
    public string NombreCliente { get; set; }
    public decimal Importe { get; set; }
    public decimal ImporteIVA { get; set; }
    public string Moneda { get; set; }
    public string FechaCobro { get; set; }
    public string Estado { get; set; }
}

public partial class Main : System.Web.UI.Page
{
    List<Factura> facturas = new List<Factura>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["facturasConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID, FechaFactura, CIFCliente, NombreCliente, Importe, ImporteIVA, Moneda, FechaCobro, Estado FROM Facturas";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Factura factura = new Factura
                        {
                            ID = reader["ID"].ToString(),
                            FechaFactura = reader["FechaFactura"].ToString(),
                            CIFCliente = reader["CIFCliente"].ToString(),
                            NombreCliente = reader["NombreCliente"].ToString(),
                            Importe = Convert.ToDecimal(reader["Importe"]),
                            ImporteIVA = Convert.ToDecimal(reader["ImporteIVA"]),
                            Moneda = reader["Moneda"].ToString(),
                            FechaCobro = reader["FechaCobro"].ToString(),
                            Estado = reader["Estado"].ToString()
                        };

                        facturas.Add(factura);
                    }
                }
            }

            // Obtener opciones únicas de estados y monedas
            var estados = facturas.Select(f => f.Estado).Distinct().ToList();
            var moneda = facturas.Select(f => f.Moneda).Distinct().ToList();

            // Llenar los DropDownList con las opciones de estados y monedas
            

            // Establecer el origen de datos del GridView con todas las facturas y enlazar
            GridView1.DataSource = facturas;
            GridView1.DataBind();
        }
    }

}
