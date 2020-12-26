using PdfService.Models;
using System.Threading.Tasks;

namespace PdfService.Client
{
    public interface IPdfServiceClient
    {
        Task<ApiResponse<GenerateInvoiceResponse>> GenerateInvoice(GenerateInvoiceRequest request);
    }
}