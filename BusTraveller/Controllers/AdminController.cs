using AutoMapper;
using Bus.Core.Models;
using Bus.Core.Repositories.Contract;
using BusTraveller.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusTraveller.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IDataRepository<Destination> _destRepo;
        private readonly IDataRepository<Appointment> _appointRepo;
        private readonly IDataRepository<Request> _reqRepo;
        private readonly IDataRepository<User> _userRepo;

        private readonly IMapper _mapper;

		public AdminController(IDataRepository<Destination> destRepo, IDataRepository<Appointment> appointRepo, IDataRepository<Request> reqRepo, IDataRepository<User> userRepo, IMapper mapper)
        {
			_destRepo = destRepo;
            _appointRepo = appointRepo;
            _mapper = mapper;
            _reqRepo= reqRepo;
            _userRepo = userRepo;

        }

        [HttpPut("ApproveUser")]
        public async Task<ActionResult> ApproveUsers()
        {
            var users = await _userRepo.GetAll();
            foreach (var user in users)
            {
                if (user.UserRoleId != 1)
                    user.Status = true;
                await _userRepo.Update(user);
            }
            return Ok();
        }

/////////////////////////////////////////////////////////////////////////////////////////////////

        [Authorize(Roles ="Admin")]
        [HttpPost("CreateDestination")]
		public async Task<ActionResult<DestinationDto>> CreateDestination(DestinationDto dto)
		{
			if(dto == null)
			{
				return BadRequest("Please Enter Valid Data");
			}
			var destinationToCreate=_mapper.Map<Destination>(dto);

			await _destRepo.Add(destinationToCreate);

			return Ok();
		}

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateDestination/{id}")]
        public async Task<ActionResult<DestinationDto>> UpdateDestination(int id, DestinationDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data");
            }

            var destinationToUpdate = await _destRepo.Get(id);

            if (destinationToUpdate == null)
            {
                return NotFound();
            }

            // Store the original location and update the location
            var originalLocation = destinationToUpdate.Location;
            destinationToUpdate.Location = dto.Location;

            // Update the destination
            _mapper.Map(dto, destinationToUpdate);
            await _destRepo.Update(destinationToUpdate);

            // Update the end-point in associated appointments
            var appointmentsToUpdate = await _appointRepo.GetAll();
            foreach (var appointment in appointmentsToUpdate.Where(app => app.DestinationId == id))
            {
                appointment.EndPoint = dto.Location;
                await _appointRepo.Update(appointment);
            }

            return Ok(destinationToUpdate);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteDestination/{id}")]
        public async Task<ActionResult<DestinationDto>> deleteDestination(int id)
        {

            var existingDestination = await _destRepo.Get(id);
            if (existingDestination == null)
            {
                return NotFound("No destination found");
            }

            await _destRepo.Delete(existingDestination);
     
            return Ok();
        }
        //////////////////////////////////////////////////////////////////////////////

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateAppointment")]
        public async Task<ActionResult<AppointmentDto>> CreateAppointment(AppointmentDto appDto)
        {
            if (appDto == null)
            {
                return BadRequest("Please Enter Valid Data");
            }

            // Retrieve the destination corresponding to the EndPoint
            var destination = await _destRepo.GetByLocationAsync(appDto.EndPoint);
            if (destination == null)
            {
                return BadRequest("Invalid destination location");
            }

            // Check if the Location matches the destination's Location
            if (destination.Location != appDto.EndPoint)
            {
                return BadRequest("Location does not match the destination for the provided EndPoint");
            }

            // Update the Location in Destination with the EndPoint in AppointmentDto
            destination.Location = appDto.EndPoint;

            var appointmentToCreate = _mapper.Map<Appointment>(appDto);

            appointmentToCreate.DestinationId = destination.Id;

            await _appointRepo.Add(appointmentToCreate);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateAppointment/{id}")]
        public async Task<ActionResult<AppointmentDto>> UpdateAppointment(int id, AppointmentDto appDto)
        {
            if (appDto == null)
            {
                return BadRequest("Invalid data ");
            }

            var AppointmentToUpdate = await _appointRepo.Get(id);

            if (AppointmentToUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(appDto, AppointmentToUpdate);

            await _appointRepo.Update(AppointmentToUpdate);
            _ = _mapper.Map<Appointment>(appDto);

            return Ok(AppointmentToUpdate);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("AppointmentDestination/{id}")]
        public async Task<ActionResult<DestinationDto>> AppointmentDestination(int id)
        {

            var existingAppointment = await _appointRepo.Get(id);
            if (existingAppointment == null)
            {
                return NotFound("No destination found");
            }

            await _appointRepo.Delete(existingAppointment);

            return Ok();
        }
        ///////////////////////////////////////////////////////////////////////
       
        /*[Authorize(Roles = "Admin")]
        [HttpPost("CreateRequestState")]
        public async Task<ActionResult<RequestDto>> CreateRequestState(RequestDto reqdto)
        {
            if (reqdto == null)
            {
                return BadRequest("Please Enter Valid Data");
            }
            var requestToCreate = _mapper.Map<Request>(reqdto);

            await _reqRepo.Add(requestToCreate);

            return Ok();
        }*/
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateRequestState/{id}")]
        public async Task<ActionResult<RequestDto>> UpdateRequestState(int id, RequestDto reqDto)
        {
            if (reqDto == null)
            {
                return BadRequest("Invalid data ");
            }

            var RequestToUpdate = await _reqRepo.Get(id);

            if (RequestToUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(reqDto, RequestToUpdate);

            await _reqRepo.Update(RequestToUpdate);
            _ = _mapper.Map<Request>(reqDto);

            return Ok(RequestToUpdate);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteRequestState/{id}")]
        public async Task<ActionResult<Request>> DeleteRequestState(int id)
        {

            var existingRequest = await _reqRepo.Get(id);
            if (existingRequest == null)
            {
                return NotFound("No destination found");
            }

            await _reqRepo.Delete(existingRequest);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get history of requests")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequests()
        {
            var requests = await _reqRepo.GetAll();

            // Order the requests by TravellerId
            var orderedRequests = requests.OrderBy(r => r.TravellerId);

            // Simplify the requests for response
            var simplifiedRequests = orderedRequests.Select(r => new
            {
                Id = r.Id,
                Status = r.Status,
                TravellerId = r.TravellerId,
                DestinationId = r.DestinationId,
                AppointmentId = r.AppointmentId
            }).ToList();

            return Ok(simplifiedRequests);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllTravellers")]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetAllTravellers()
        {
            
            
                // Materialize the query by calling ToListAsync() first
                var travellers = await _userRepo.GetAll();

                // Filter the travellers based on their role ID
                var travellerDtos = _mapper.Map<IEnumerable<User>, IEnumerable<UserRoleDto>>(travellers.Where(u => u.UserRoleId == 2));

                return Ok(travellerDtos);
            
           
        }
    }
    }

