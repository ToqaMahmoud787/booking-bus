using Bus.Core.Models;
using Bus.Core.Repositories.Contract;
using Bus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus.Infrastructure
{
	public class DataRepository<T> : IDataRepository<T> where T : class
	{
				
	private readonly ApplicationDbContext _dbContext;
		public DataRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Add(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			_dbContext.SaveChanges();
		}

		public async Task Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await _dbContext.Set<T>().ToListAsync();

		}

		public async Task<T> Get(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);

		}


		public async Task Update(T entity)
		{
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}


        public async Task<IEnumerable<Appointment>> FilterByPriceAndStartPoint(decimal minPrice, decimal maxPrice, string startPoint)
        {
            // Filter appointments by price range and start point
            return await _dbContext.Appointments
                .Where(a => a.Price >= minPrice && a.Price <= maxPrice && a.StartPoint == startPoint)
                .ToListAsync();
        }
      
        public async Task<IEnumerable<TravellerHistorySearch>> GetSearchHistoryByTravelerId(int travellerId)
        {
            return await _dbContext.TravellerHistorySearchs
                                  .Where(th => th.TravellerId == travellerId)
                                  .ToListAsync();
        }


        public async Task<Destination> GetByLocationAsync(string endPoint)
        {
            return await _dbContext.Destinations.FirstOrDefaultAsync(d => d.Location == endPoint);
        }
        public async Task<int> GetRequestCountForAppointment(int appointmentId)
        {
            return await _dbContext.Requests
                .CountAsync(r => r.AppointmentId == appointmentId);
        }
    }
}
