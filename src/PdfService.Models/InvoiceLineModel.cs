namespace PdfService.Models
{
    public class InvoiceLineModel
    {
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}