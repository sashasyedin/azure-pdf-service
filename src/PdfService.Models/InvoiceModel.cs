using System;
using System.Collections.Generic;

namespace PdfService.Models
{
    public class InvoiceModel
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public string OrderNumber { get; set; }
        public string TotalAmount { get; set; }
        public IEnumerable<InvoiceLineModel> Lines { get; set; }
    }
}