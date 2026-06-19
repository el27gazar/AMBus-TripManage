using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Domain.Entites;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cell = iText.Layout.Element.Cell;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;

namespace AMBus.TripManage.Persistance.Service
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateTripManifest(Trip trip, IEnumerable<Booking> bookings)
        {
            using var ms = new MemoryStream();
            var writerProperties = new WriterProperties().UseSmartMode();
            var writer = new PdfWriter(ms, new WriterProperties());
            var pdf = new PdfDocument(writer);
            var doc = new Document(pdf, PageSize.A4);

            // Header
            doc.Add(new Paragraph("كشف ركاب الرحلة")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)));

            doc.Add(new Paragraph($"من: {trip.From.Name}  →  إلى: {trip.To.Name}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(14));

            doc.Add(new Paragraph($"تاريخ الرحلة: {trip.DepartureTime:dd/MM/yyyy HH:mm}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(12));

            doc.Add(new Paragraph($"الباص: {trip.Bus.PlateNumber} | المقاعد المتاحة: {trip.AvailableSeats}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(11));

            doc.Add(new Paragraph("\n"));

            // Table
            var table = new Table(new float[] { 1, 3, 3, 2, 2 })
                .UseAllAvailableWidth();

            // Table Headers
            foreach (var h in new[] { "#", "اسم الراكب", "رقم الهوية", "رقم المقعد", "حالة الحجز" })
            {
                table.AddHeaderCell(
                    new Cell().Add(new Paragraph(h))
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetTextAlignment(TextAlignment.CENTER));
            }

            // Table Rows
            int counter = 1;
            foreach (var booking in bookings)
            {
                foreach (var seat in booking.BookingSeats)
                {
                    table.AddCell(
                        new Cell().Add(new Paragraph(counter++.ToString()))
                            .SetTextAlignment(TextAlignment.CENTER));

                    table.AddCell(
                        new Cell().Add(new Paragraph(seat.PassengerName ?? "-")));

                    table.AddCell(
                        new Cell().Add(new Paragraph(seat.PassengerIdNumber ?? "-")));

                    table.AddCell(
                        new Cell().Add(new Paragraph(seat.Seat?.SeatNumber ?? "-"))
                            .SetTextAlignment(TextAlignment.CENTER));

                    table.AddCell(
                        new Cell().Add(new Paragraph(booking.Status.ToString()))
                            .SetTextAlignment(TextAlignment.CENTER));
                }
            }

            doc.Add(table);

            // Footer
            doc.Add(new Paragraph($"\nإجمالي الركاب: {counter - 1}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20))
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            doc.Add(new Paragraph($"تم الطباعة: {DateTime.Now:dd/MM/yyyy HH:mm}")
                .SetFontSize(10)
                .SetFontColor(ColorConstants.GRAY));

            doc.Close();
            return ms.ToArray();
        }
    }
}
