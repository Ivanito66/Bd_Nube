using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml;

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

public partial class factura2 : System.Web.UI.Page
{
    List<Factura> facturas = new List<Factura>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Cargar el archivo XML de facturas
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("/App_Data/facturas.xml"));

            // Recorrer los nodos del archivo XML y crear objetos Factura
            foreach (XmlNode node in xmlDoc.SelectNodes("/Facturas/Factura"))
            {
                Factura factura = new Factura
                {
                    ID = node.Attributes["ID"].Value,
                    FechaFactura = node.Attributes["FechaFactura"].Value,
                    CIFCliente = node.Attributes["CIFCliente"].Value,
                    NombreCliente = node.Attributes["NombreCliente"].Value,
                    Importe = Convert.ToDecimal(node.Attributes["Importe"].Value),
                    ImporteIVA = Convert.ToDecimal(node.Attributes["ImporteIVA"].Value),
                    Moneda = node.Attributes["Moneda"].Value,
                    FechaCobro = node.Attributes["FechaCobro"].Value,
                    Estado = node.Attributes["Estado"].Value
                };

                facturas.Add(factura);
            }

            // Obtener opciones únicas de estados y monedas
            var estados = facturas.Select(f => f.Estado).Distinct().ToList();
            var moneda = facturas.Select(f => f.Moneda).Distinct().ToList();

            // Llenar los DropDownList con las opciones de estados y monedas
            DropDownList1.DataSource = estados;
            DropDownList1.DataBind();
            DropDownList2.DataSource = moneda;
            DropDownList2.DataBind();

            // Establecer el origen de datos del GridView con todas las facturas y enlazar
            GridView1.DataSource = facturas;
            GridView1.DataBind();
        }
    }

    protected void FiltrarFacturas(object sender, EventArgs e)
    {
        // Obtener los valores seleccionados en los filtros de estado y moneda
        string estado = DropDownList1.SelectedValue;
        string moneda = DropDownList2.SelectedValue;

        // Cargar el archivo XML de facturas
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("/App_Data/facturas.xml"));

        // Crear una lista para almacenar facturas filtradas
        List<Factura> facturasFiltradas = new List<Factura>();

        // Recorrer los nodos del archivo XML y aplicar filtros
        foreach (XmlNode node in xmlDoc.SelectNodes("/Facturas/Factura"))
        {
            string estadoFactura = node.Attributes["Estado"].Value;
            string monedaFactura = node.Attributes["Moneda"].Value;

            // Aplicar filtros según los valores seleccionados
            if ((string.IsNullOrEmpty(estado) || estadoFactura == estado) &&
                (string.IsNullOrEmpty(moneda) || monedaFactura == moneda))
            {
                Factura factura = new Factura
                {
                    ID = node.Attributes["ID"].Value,
                    FechaFactura = node.Attributes["FechaFactura"].Value,
                    CIFCliente = node.Attributes["CIFCliente"].Value,
                    NombreCliente = node.Attributes["NombreCliente"].Value,
                    Importe = Convert.ToDecimal(node.Attributes["Importe"].Value),
                    ImporteIVA = Convert.ToDecimal(node.Attributes["ImporteIVA"].Value),
                    Moneda = monedaFactura,
                    FechaCobro = node.Attributes["FechaCobro"].Value,
                    Estado = estadoFactura
                };

                facturasFiltradas.Add(factura);
            }
        }

        // Establecer el origen de datos del GridView con las facturas filtradas y enlazar
        GridView1.DataSource = facturasFiltradas;
        GridView1.DataBind();
    }

    protected void ResetFiltros(object sender, EventArgs e)
    {
        // Restablecer los filtros cargando todas las facturas nuevamente
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("/App_Data/facturas.xml"));

        facturas.Clear();

        foreach (XmlNode node in xmlDoc.SelectNodes("/Facturas/Factura"))
        {
            Factura factura = new Factura
            {
                ID = node.Attributes["ID"].Value,
                FechaFactura = node.Attributes["FechaFactura"].Value,
                CIFCliente = node.Attributes["CIFCliente"].Value,
                NombreCliente = node.Attributes["NombreCliente"].Value,
                Importe = Convert.ToDecimal(node.Attributes["Importe"].Value),
                ImporteIVA = Convert.ToDecimal(node.Attributes["ImporteIVA"].Value),
                Moneda = node.Attributes["Moneda"].Value,
                FechaCobro = node.Attributes["FechaCobro"].Value,
                Estado = node.Attributes["Estado"].Value
            };

            facturas.Add(factura);
        }

        // Establecer el origen de datos del GridView con todas las facturas y enlazar
        GridView1.DataSource = facturas;
        GridView1.DataBind();
    }

    protected void ExportXLS(object sender, EventArgs e)
    {
        // Crear un archivo XLS en memoria y establecer las cabeceras para descargar
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=Facturas.xls");
        Response.ContentType = "application/ms-excel";

        // Escribir el contenido del archivo XLS
        Response.Write("<html xmlns:x='urn:schemas-microsoft-com:office:excel'>");
        Response.Write("<head><style>body{mso-number-format:\"\\@\";}</style></head>");
        Response.Write("<body>");
        Response.Write("<table>");

        // Agregar encabezados
        Response.Write("<tr>");
        Response.Write("<td>ID</td>");
        Response.Write("<td>Fecha de Factura</td>");
        Response.Write("<td>CIF del Cliente</td>");
        Response.Write("<td>Nombre del Cliente</td>");
        Response.Write("<td>Importe</td>");
        Response.Write("<td>Importe + IVA</td>");
        Response.Write("<td>Moneda</td>");
        Response.Write("<td>Fecha de Cobro</td>");
        Response.Write("<td>Estado</td>");
        Response.Write("</tr>");

        // Agregar datos de facturas al archivo XLS
        foreach (GridViewRow row in GridView1.Rows)
        {
            Response.Write("<tr>");
            Response.Write("<td>" + row.Cells[0].Text + "</td>");
            Response.Write("<td>" + row.Cells[1].Text + "</td>");
            Response.Write("<td>" + row.Cells[2].Text + "</td>");
            Response.Write("<td>" + row.Cells[3].Text + "</td>");
            Response.Write("<td>" + row.Cells[4].Text + "</td>");
            Response.Write("<td>" + row.Cells[5].Text + "</td>");
            Response.Write("<td>" + row.Cells[6].Text + "</td>");
            Response.Write("<td>" + row.Cells[7].Text + "</td>");
            Response.Write("<td>" + row.Cells[8].Text + "</td>");
            Response.Write("</tr>");
        }

        Response.Write("</table>");
        Response.Write("</body>");
        Response.Write("</html>");

        // Finalizar la respuesta para descargar el archivo
        Response.End();
    }
}
