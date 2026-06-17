using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using iText.Kernel.Colors;
using iText.Layout.Properties;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Cell = iText.Layout.Element.Cell;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;

namespace AMBus.TripManage.Application.Features.DriverF.Queries
{
   
    public class GetDriverTripManifestQueryHandler
        : IRequestHandler<GetDriverTripManifestQuery, byte[]>
    {
        private readonly IUnitOfWork _uow;
        private readonly IPdfService _pdfService;

        public GetDriverTripManifestQueryHandler(IUnitOfWork uow, IPdfService pdfService)
        {
            _uow = uow;
            _pdfService = pdfService;
        }

        public async Task<byte[]> Handle(
            GetDriverTripManifestQuery request,
            CancellationToken cancellationToken)
        {
            // جيب الـ Trip مع كل التفاصيل
            var trip = await _uow.Trips.GetTripWithDetailsAsync(request.TripId)
                ?? throw new NotFoundException(nameof(Trip), request.TripId);

            //  السائق هو صاحب الرحلة
            if (trip.Driver?.UserId.ToString() != request.DriverId.ToString())
                throw new BusinessRuleException("هذه الرحلة غير مكلف بها.");

            // جيب الـ Bookings
            var bookings = await _uow.Bookings
                .GetBookingsByTripAsync(request.TripId);

            // ولّد الـ PDF
            return _pdfService.GenerateTripManifest(trip, bookings);
        }
    }
}
