using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Samples.ViewModels.Configuration;

public class CarMakeViewModelConfiguration : IEntityTypeConfiguration<CarMakeViewModel>
{
    public void Configure(EntityTypeBuilder<CarMakeViewModel> builder)
    {
        // consider making this a view and using .ToView

        // if you just want to run the query and have no table or migration made, use .ToTable((x) => x.ExcludeFromMigrations());
        builder.HasNoKey().ToSqlQuery(@"SELECT m.Id MakeId, m.Name Make, i.Id CarId, i.IsDrivable, i.DisplayName, i.DateBuilt, i.Color, i.PetName FOM dbo.MAKES m INNER JOIN dbo.Inventory i ON i.MakeId = m.Id");
    }
}
