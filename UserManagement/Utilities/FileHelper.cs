using CsvHelper;
using System.Globalization;
using UserManagement.Const;

namespace UserManagement.Utilities
{
    public static class FileHelper
    {
        public static IEnumerable<IEnumerable<T>> BatchReadCsv<T>(string path, int batchSize = ServiceConstants.BulkOperationsBatchSize)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                throw new ArgumentException($"File {path} does not exist");
            }

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
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
    }
}
