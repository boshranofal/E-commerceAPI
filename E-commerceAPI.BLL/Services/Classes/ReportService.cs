using E_commerceAPI.DAL.Reposetories.Intefaces;
using Microsoft.Identity.Client;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Classes
{
    public class ReportService
    {
        private readonly IProductRepository _productRepository;

        public ReportService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
           
        }
        public void GenerateProductReport()
        {
          

            // code in your main method
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("E-commerce-Products")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);
                            foreach(var products in _productRepository.GetAllproductWithImage())
                            {
                                x.Item().Text($"Name: {products.Name }.. Id: {products.Id}");
                            }
                        
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf("E_commerceAPI-Products.pdf!");
        }
    }
}
