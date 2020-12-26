namespace PdfService.Models
{
    public class InvoiceLineModel
    {
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string TotalAmount { get; set; }
    }
}