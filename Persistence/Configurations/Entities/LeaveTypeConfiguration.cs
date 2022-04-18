using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain;

namespace Persistence.Configurations.Entities;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasData(
            new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Vacation",
                CreatedBy = "Sourav",
                LastModifiedBy = "Sourav"
            },
            new LeaveType
            {
                Id = 2,
                DefaultDays = 12,
                Name = "Sick",
                CreatedBy = "Sourav",
                LastModifiedBy = "Sourav"
            }
        );
    }
}