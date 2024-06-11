using Bus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus.Core.Repositories.Contract
{
	public interface IDataRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll();

		Task<T> Get(int id);

		Task Add(T entity);

		Task Update(T entity);

		Task Delete(T entity);
		Task<IEnumerable<Appointment>> FilterByPriceAndStartPoint(decimal minPrice, decimal maxPrice, string startPoint);
		Task<IEnumerable<TravellerHistorySearch>> GetSearchHistoryByTravelerId(int travellerId);
		Task<Destination> GetByLocationAsync(string endPoint);
        Task<int> GetRequestCountForAppointment(int appointmentId);
    }
}
