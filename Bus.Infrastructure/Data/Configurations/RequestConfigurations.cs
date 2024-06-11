using Azure.Core;
using Bus.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Request = Bus.Core.Models.Request;

namespace Bus.Infrastructure.Data.Configurations
{
	public class RequestConfigurations : IEntityTypeConfiguration<Core.Models.Request>
	{
		public void Configure(EntityTypeBuilder<Request> builder)
		{
			builder.HasOne(R=>R.Appointment).WithMany(R=>R.Requests).HasForeignKey(R=>R.AppointmentId);
			builder.HasOne(R=>R.Destination).WithMany(R=>R.Requests).HasForeignKey(R=>R.DestinationId);

		}
	}
}
