using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using UserManagement.Const;

namespace UserManagement.Utilities
{
    public static class FileHelper
    {
        public static IEnumerable<IEnumerable<T>> BatchReadCsv<T>(IFormFile file, int batchSize = ServiceConstants.BulkOperationsBatchSize)
        {

            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<UserMap>();
            while (csv.Read())
            {
                var batch = new List<T>();
                do
                {
                    var record = csv.GetRecord<T>();
                    if (record != null)
                    {
                        batch.Add(record);
                    }
                }
                while (batch.Count < batchSize && csv.Read());

                yield return batch;
            }
        }

        public class UserMap : ClassMap<DTO.CreateUserRequest>
        {
            public UserMap()
            {
                Map(m => m.Id).Index(0).Name("id");
                Map(m => m.FirstName).Index(1).Name("firstName");
                Map(m => m.LastName).Index(1).Name("lastName");
                Map(m => m.Email).Index(1).Name("email");
                Map(m => m.Password).Index(1).Name("password");
            }
        }
    }
}
