using AutoMapper;
using Bus.Core.Models;
using Bus.Core.Repositories.Contract;
using Bus.Infrastructure.Data;
using BusTraveller.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace BusTraveller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravellerController : ControllerBase
    {
      
        private readonly IDataRepository<Request> _reqRepo;
        private readonly IDataRepository<Destination> _destRepo;
        private readonly IDataRepository<Appointment> _appointRepo;
        private readonly IDataRepository<TravellerHistorySearch> _tHSRepo;
        private readonly IMapper _mapper;
        private readonly IDataRepository<User> _userRepo;
        private readonly ApplicationDbContext _dbContext;

        public TravellerController( IDataRepository<Request> reqRepo, IDataRepository<Destination> destRepo, IDataRepository<Appointment> appointRepo, IDataRepository<TravellerHistorySearch> THSRepo, IDataRepository<User> userRepo, ApplicationDbContext dbContext, IMapper mapper)
        {
         
            _mapper = mapper;
            _reqRepo = reqRepo;
            _destRepo = destRepo;
            _appointRepo = appointRepo;
            _tHSRepo = THSRepo;
            _userRepo = userRepo;
            _dbContext = dbContext;


        }

        [Authorize(Roles = "Traveller")]
        [HttpPost("SendRequestToAdmin")]
        public async Task<ActionResult> SendRequestToAdmin(requestDtotraveller Dto)
        {
            if (Dto == null)
            {
                return BadRequest("Invalid appointment data");
            }

            // Retrieve the appointment corresponding to the provided AppointmentId
            var appointment = await _appointRepo.Get(Dto.AppointmentId);
            if (appointment == null)
            {
                return BadRequest("Invalid appointment ID");
            }

            // Retrieve the destination corresponding to the provided DestinationId
            var destination = await _destRepo.Get(Dto.DestinationId);
            if (destination == null)
            {
                return BadRequest("Invalid destination ID");
            }

            // Check if the Location of the destination matches the EndPoint of the appointment
            if (destination.Location != appointment.EndPoint)
            {
                return BadRequest("Invalid destination location");
            }

            // Retrieve the count of requests for the appointment
            var requestCount = await _reqRepo.GetRequestCountForAppointment(Dto.AppointmentId);

            // Check if the request count exceeds the capacity of the appointment
            if (requestCount >= appointment.Capacity)
            {
                return BadRequest("You can't send a request. Appointment capacity reached.");
            }

            var request = new Request
            {
                Status = false,
                AppointmentId = Dto.AppointmentId,
                TravellerId = Dto.TravellerId,
                DestinationId = Dto.DestinationId
            };

            // Add the request to the repository
            await _reqRepo.Add(request);

            return Ok("Appointment request sent to admin successfully");
        }




        [Authorize(Roles = "Traveller")]
        [HttpGet("FilterByPriceAndStartPoint")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> FilterByPriceAndStartPoint(decimal minPrice, decimal maxPrice, string startPoint)
        {
            // Get appointments within the specified price range and with the specified start point
            var appointments = await _reqRepo.FilterByPriceAndStartPoint(minPrice, maxPrice, startPoint);

            // Save the search criteria to TravellerHistorySearch
            var travellerIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(travellerIdString, out int travellerId))
            {
                // Handle the case where the travellerIdString cannot be parsed to an integer
                // For example, log an error, return a bad request response, etc.
                return BadRequest("Invalid traveller ID");
            }
            var travellerHistorySearch = new TravellerHistorySearch
            {
               
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                startPoint = startPoint,
                TravellerId = travellerId,
                
            };
            _tHSRepo.Add(travellerHistorySearch);

            var appointmentDtos = _mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentDto>>(appointments);
            return Ok(appointmentDtos);
        }


        [Authorize(Roles = "Traveller")]
        [HttpGet("SearchHistory")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetSearchHistory()
        {
            var travellerIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(travellerIdString) || !int.TryParse(travellerIdString, out int travellerId))
            {
                // Log an error or return a BadRequest if parsing fails
                return BadRequest("Invalid traveller ID");
            }

            var searchHistory = await _tHSRepo.GetSearchHistoryByTravelerId(travellerId);
            if (searchHistory == null || !searchHistory.Any())
            {
                return NotFound("No search history found for the traveller");
            }

            var appointments = new List<Appointment>();
            foreach (var history in searchHistory)
            {
                var filteredAppointments = await _reqRepo.FilterByPriceAndStartPoint(history.MinPrice, history.MaxPrice, history.startPoint);
                appointments.AddRange(filteredAppointments);
            }

            var appointmentDtos = _mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentDto>>(appointments);
            return Ok(appointmentDtos);
        }
        /////////////////////////////////////////////////////////////////////
        [Authorize(Roles = "Traveller")] // Assuming only travellers can access this endpoint
        [HttpGet("AllDestinations")]
        public async Task<ActionResult<IEnumerable<DestinationDto>>> GetAllDestinations()
        {
            var destinations = await _destRepo.GetAll();
            var destinationDtos = _mapper.Map<IEnumerable<Destination>, IEnumerable<DestinationDto>>(destinations);
            return Ok(destinationDtos);
        }

        [Authorize(Roles = "Traveller")] // Assuming only travellers can access this endpoint
        [HttpGet("AllAppointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAllAppointments()
        {
            var appointments = await _appointRepo.GetAll();
            var appointmentDtos = _mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentDto>>(appointments);
            return Ok(appointmentDtos);
        }
    }
}
