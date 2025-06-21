using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Sistema.API.Consume;
using SistemaGFYMP.Modelos;
using SistemaLogs.Modelos;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Presentacion_MVC_.Controllers
{
    public class ReportesController : Controller
    {
        [Authorize]
        public IActionResult ReporteRiesgoFallo()
        {
            var camionesConRiesgo = GetCamionesConRiesgo();  // Obtener camiones con riesgo de fallo
            return View(camionesConRiesgo);  // Pasar los datos al reporte
        }

        public IActionResult GenerarReporteExcel()
        {
            var camionesConRiesgo = GetCamionesConRiesgo();  // Obtener camiones con riesgo

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Camiones con Riesgo");

                // Títulos de las columnas
                worksheet.Cells[1, 1].Value = "Código";
                worksheet.Cells[1, 2].Value = "Marca";
                worksheet.Cells[1, 3].Value = "Modelo";
                worksheet.Cells[1, 4].Value = "Riesgo";

                // Agregar las filas con los datos
                var row = 2;
                foreach (var camion in camionesConRiesgo)
                {
                    worksheet.Cells[row, 1].Value = camion.Codigo;
                    worksheet.Cells[row, 2].Value = camion.Marca;
                    worksheet.Cells[row, 3].Value = camion.Modelo;
                    worksheet.Cells[row, 4].Value = "Riesgo de fallo";
                    row++;
                }

                var fileBytes = package.GetAsByteArray();

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteCamionesRiesgo.xlsx");
            }
        }

        public IActionResult GenerarReportePDF()
        {
            var camionesConRiesgo = GetCamionesConRiesgo();  // Obtener camiones con riesgo

            var memoryStream = new MemoryStream();
            var writer = new PdfWriter(memoryStream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            // Título del reporte
            document.Add(new Paragraph("Reporte de Camiones con Riesgo de Fallo en los Próximos 30 Días"));

            // Crear tabla con los datos
            var table = new Table(4, true);  // 4 columnas: Código, Marca, Modelo, Riesgo

            table.AddHeaderCell("Código");
            table.AddHeaderCell("Marca");
            table.AddHeaderCell("Modelo");
            table.AddHeaderCell("Riesgo");

            // Agregar las filas de los camiones con riesgo
            foreach (var camion in camionesConRiesgo)
            {
                table.AddCell(camion.Codigo.ToString());
                table.AddCell(camion.Marca);
                table.AddCell(camion.Modelo);
                table.AddCell("Riesgo de fallo");
            }

            document.Add(table);
            document.Close();

            // Convertir el MemoryStream a un arreglo de bytes
            var byteArray = memoryStream.ToArray();

            return File(byteArray, "application/pdf", "ReporteCamionesRiesgo.pdf");
        }

        private List<Camion> GetCamionesConRiesgo()
        {
            var camiones = CRUD<Camion>.GetAll();  // Obtener todos los camiones

            // Filtrar camiones con riesgo de fallo
            return camiones.Where(camion =>
                camion.Kilometraje >= 100000 ||  // Kilometraje alto
                camion.MantenimientosProgramados.Any(mantenimiento =>
                    mantenimiento.Fecha <= DateTime.UtcNow.AddDays(30)) ||  // Mantenimiento en 30 días
                camion.Estado == "Alerta"  // Motor en alerta
            ).ToList();
        }
    }
}
