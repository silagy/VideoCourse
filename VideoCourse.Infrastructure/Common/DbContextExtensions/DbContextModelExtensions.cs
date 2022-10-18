using Microsoft.EntityFrameworkCore;
using VideoCourse.Domain.Primitives;

namespace VideoCourse.Infrastructure.Common.DbContextExtensions;

public static class DbContextModelExtensions
{
 public static void LowercaseTablesAndProperties(this ModelBuilder modelBuilder)
 {
  foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(e => e.ClrType.BaseType != typeof(ValueObject)))
  {
   entityType.SetTableName(entityType.GetTableName()!.ToSnakeCase());

   foreach (var property in entityType.GetProperties())
   {
    property.SetColumnName(property.GetColumnName().ToSnakeCase());
   }
  }
 }
}