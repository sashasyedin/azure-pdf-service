using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Mvc;
using PdfService.Api.Helpers;
using PdfService.Models;
using RazorLight;
using System.Text;
using System.Threading.Tasks;

namespace PdfService.Api.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IStorageManager _storageManager;
        private readonly IJsReportMVCService _jsReportMvcService;

        public InvoiceController(IStorageManager storageManager, IJsReportMVCService jsReportMvcService)
        {
            _storageManager = storageManager;
            _jsReportMvcService = jsReportMvcService;

            RazorEngine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(Startup))
                .UseMemoryCachingProvider()
                .Build();
        }

        public RazorLightEngine RazorEngine { get; }

        [HttpPost("api/invoices")]
        public async Task<ApiResponse<GenerateInvoiceResponse>> GenerateInvoiceAndStoreResultIntoStorage([FromBody] GenerateInvoiceRequest request)
        {
            // Get template file
            var ms = await _storageManager.DownloadFileAsync("invoices", "templates/sample.cshtml");

            if (ms == null)
            {
                return new ApiResponse<GenerateInvoiceResponse>(ErrorCode.TemplateNotFound);
            }

            ms.Position = 0;

            // Convert Razor based code to HTML in accordance with data model
            var template = Encoding.UTF8.GetString(ms.ToArray());
            var html = await RazorEngine.CompileRenderStringAsync("some-key", template, request.Model);

            // Render PDF file
            var report = await _jsReportMvcService.RenderAsync(new RenderRequest
            {
                Template = new Template
                {
                    Content = html,
                    Engine = Engine.None,
                    Recipe = Recipe.ChromePdf,
                    Chrome = new Chrome
                    {
                        MarginTop = "1cm",
                        MarginLeft = "1cm",
                        MarginBottom = "1cm",
                        MarginRight = "1cm"
                    }
                }
            });

            if (report.Content == null || report.Content.Length == 0)
            {
                return new ApiResponse<GenerateInvoiceResponse>(ErrorCode.PdfGenerationFailed);
            }

            // Upload result to Storage
            var fileName = $"{request.Model.InvoiceNumber}.pdf";
            await _storageManager.UploadFileAsync("invoices", $"pdfs/{fileName}", report.Content);
            return new ApiResponse<GenerateInvoiceResponse> {Payload = new GenerateInvoiceResponse {FileSize = report.Content.Length}};
        }

        [HttpGet("api/invoices")]
        [MiddlewareFilter(typeof(JsReportPipeline))]
        public IActionResult GenerateInvoiceAndGetResultOnTheSpot(GenerateInvoiceRequest request)
        {
            HttpContext.JsReportFeature()
                .Configure(req =>
                    req.Options.Base =
                        "http://localhost") // the normal jsreport base url injection into the html doesn't work properly with docker and asp.net because of port mapping
                .Recipe(Recipe.ChromePdf);

            return View(request.Model);
        }
    }
}