namespace PdfService.Models
{
    public class GenerateInvoiceRequest
    {
        public string StorageContainerId { get; set; }
        public InvoiceModel Model { get; set; }
    }
}