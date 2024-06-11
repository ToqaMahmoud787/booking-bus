using Bus.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus.Infrastructure.Data.Configurations
{
	public class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
	{
		public void Configure(EntityTypeBuilder<Appointment> builder)
		{
			builder.Property(A => A.Price).HasColumnType("decimal(12,2)");
			builder.HasOne(A => A.Destination).WithMany(D => D.Appointments).HasForeignKey(A => A.DestinationId);
		}
	}
}
