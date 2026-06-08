using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.AuthDto;
using AMBus.TripManage.Application.Dtos.BookingDto;
using AMBus.TripManage.Application.Dtos.BusDto;
using AMBus.TripManage.Application.Dtos.Chat;
using AMBus.TripManage.Application.Dtos.DriverDto;
using AMBus.TripManage.Application.Dtos.RouteDto;
using AMBus.TripManage.Application.Dtos.SeatDto;
using AMBus.TripManage.Application.Dtos.TicketDto;
using AMBus.TripManage.Application.Dtos.TripDto;
using AMBus.TripManage.Application.Features.AuthF.Commands.Login;
using AMBus.TripManage.Application.Features.AuthF.Commands.RegisterCommands;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            
            //login
            CreateMap<LoginCommand, LoginRequestDto>().ReverseMap();
            
            // RegisterCommand → User
            CreateMap<RegisterRequestDto, RegisterCommand>().ReverseMap();

       CreateMap<RegisterRequestDto, User>()
     .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
     .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
     .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
     .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
     .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
     .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
     .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => false))
     .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
     // تجاهل الخصائص اللي مش موجودة في DTO
     .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
     .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
     .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
     .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
     .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
     .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
     .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
     .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
     .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore());


            // User → UserDto
            CreateMap<User, UserDto>()
                .ForMember(d => d.Role, o => o.Ignore()); // تتملى من UserManager

            // ══════════════════════════════════════════════
            //  DRIVER
            // ══════════════════════════════════════════════

            CreateMap<Driver, DriverDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ReverseMap(); 
            // ══════════════════════════════════════════════
            //  BUS
            // ══════════════════════════════════════════════

            CreateMap<Bus, BusDto>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.ToString()));

            // ══════════════════════════════════════════════
            //  SEAT
            // ══════════════════════════════════════════════

            CreateMap<Seat, SeatDto>();

            CreateMap<Seat, SeatAvailabilityDto>()
                .ForMember(d => d.SeatId, o => o.MapFrom(s => s.Id));

            // ══════════════════════════════════════════════
            //  ROUTE + STOP
            // ══════════════════════════════════════════════

            CreateMap<Route, RouteDto>()
                .ForMember(d => d.Stops, o => o.MapFrom(s =>
                    s.Stops.OrderBy(st => st.StopOrder).ToList()))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedDate));

            CreateMap<Stop, StopDto>();

            // ══════════════════════════════════════════════
            //  TRIP
            // ══════════════════════════════════════════════
            CreateMap<Trip, TripDto>()
                .ForMember(d => d.FromCity, o => o.MapFrom(s => s.From.Name))
                .ForMember(d => d.ToCity, o => o.MapFrom(s => s.To.Name))
                .ForMember(d => d.FromId, o => o.MapFrom(s => s.FromId))
                .ForMember(d => d.ToId, o => o.MapFrom(s => s.ToId))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.BusType, o => o.MapFrom(s => s.Bus.Type.ToString()))
                .ForMember(d => d.BusPlate, o => o.MapFrom(s => s.Bus.PlateNumber))
                .ForMember(d => d.DriverName, o => o.MapFrom(s => s.Driver.User.FullName))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedDate));


            // ══════════════════════════════════════════════
            //  BOOKING
            // ══════════════════════════════════════════════

            CreateMap<Booking, BookingDto>()
    .ForMember(d => d.UserId, o => o.MapFrom(s => s.User))
    .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.FullName))
    .ForMember(d => d.TripId, o => o.MapFrom(s => s.Trip))
    .ForMember(d => d.TripSummary, o => o.MapFrom(s =>
        $"{s.Trip.From.Name} → {s.Trip.To.Name}" +
        $" | {s.Trip.DepartureTime:dd MMM yyyy HH:mm}"))
    .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
    .ForMember(d => d.BookedAt, o => o.MapFrom(s => s.BookedAt))
    .ForMember(d => d.Seats, o => o.MapFrom(s => s.BookingSeats));


            // ══════════════════════════════════════════════
            //  BOOKING SEAT
            // ══════════════════════════════════════════════

            CreateMap<BookingSeat, BookingSeatDto>()
                .ForMember(d => d.SeatNumber, o => o.MapFrom(s => s.Seat.SeatNumber));

            // ══════════════════════════════════════════════
            //  TICKET
            // ══════════════════════════════════════════════

            CreateMap<Booking, TicketDto>()
     .ForMember(d => d.BookingId, o => o.MapFrom(s => s.Id))
     .ForMember(d => d.FromCity, o => o.MapFrom(s => s.Trip.From.Name))
     .ForMember(d => d.ToCity, o => o.MapFrom(s => s.Trip.To.Name))
     .ForMember(d => d.DepartureTime, o => o.MapFrom(s => s.Trip.DepartureTime))
     .ForMember(d => d.BusPlate, o => o.MapFrom(s => s.Trip.Bus.PlateNumber))
     .ForMember(d => d.BusType, o => o.MapFrom(s => s.Trip.Bus.Type.ToString()))
     .ForMember(d => d.Passengers, o => o.MapFrom(s => s.BookingSeats));


            // ══════════════════════════════════════════════
            //  PAYMENT
            // ══════════════════════════════════════════════

            CreateMap<Payment, PaymentDto>().ReverseMap();



            // ══════════════════════════════════════════════
            //  REVIEW
            // ══════════════════════════════════════════════

            CreateMap<Review, ReviewDto>()
     .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.FullName))
     .ForMember(d => d.TripSummary, o => o.MapFrom(s =>
         $"{s.Trip.From.Name} → {s.Trip.To.Name}"));

            // ══════════════════════════════════════════════
            //  NOTIFICATION
            // ══════════════════════════════════════════════

            CreateMap<Notification, NotificationDto>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.ToString()));

            CreateMap<ChatConversation, ConversationDto>()
        .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.FullName))
        .ForMember(d => d.AdminName, o => o.MapFrom(s => s.Admin != null
            ? s.Admin.FullName : null))
        .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
        .ForMember(d => d.UnreadCount, o => o.MapFrom(s =>
            s.Messages.Count(m => !m.IsRead)))
        .ForMember(d => d.LastMessage, o => o.MapFrom(s =>
            s.Messages.OrderByDescending(m => m.CreatedDate)
                      .FirstOrDefault()))
        .ForMember(d => d.CreatedDate, o => o.MapFrom(s => s.CreatedDate));

            CreateMap<ChatMessage, ChatMessageDto>()
                .ForMember(d => d.SenderName, o => o.MapFrom(s => s.Sender.FullName))
                .ForMember(d => d.SenderIsAdmin, o => o.Ignore()) // بيتملى في Controller
                .ForMember(d => d.CreatedDate, o => o.MapFrom(s => s.CreatedDate));
        }
    }
}

