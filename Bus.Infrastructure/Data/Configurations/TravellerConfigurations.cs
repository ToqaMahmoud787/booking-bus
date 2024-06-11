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
	public class TravellerConfigurations : IEntityTypeConfiguration<Traveller>
	{
		public void Configure(EntityTypeBuilder<Traveller> builder)
		{
		  builder.HasMany(T=>T.Requests).WithOne(R=>R.Traveller).HasForeignKey(R=>R.TravellerId);	
			builder.HasOne(T => T.TravellerHistorySearchs)
        .WithOne(sh => sh.Traveller)
        .HasForeignKey<TravellerHistorySearch>(sh => sh.TravellerId);

        }
	}
}
