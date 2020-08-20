using System;
using System.Collections.Generic;
using System.Linq;

namespace BomTool.NetCore.Models
{
    public static class Extensions
    {
        /// <summary>
        /// Group bom data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Log"></param>
        /// <returns></returns>
        public static IEnumerable<GrouppedBomData> Group(this IEnumerable<BomData> data, Action<string> Log)
        {
            var header = data.ElementAt(0);

            var grouppedData = new List<GrouppedBomData>();
            int index = 0;

            foreach (var line in header.Lines)
            {
                grouppedData.Add(GroupData(data, index, line, Log));
                index++;
            }
            return grouppedData;
        }

        private static GrouppedBomData GroupData(IEnumerable<BomData> bomData, int index, string line, Action<string> Log)
        {
            Log($"Parsing data for {line}...");
            var result = new List<BomData>();
            var filteredData = bomData.Where(o =>
            {
                var l = o.Lines[index];
                Log($"Filtering data for line: {line}");
                return string.IsNullOrEmpty(l);
            }).GroupBy(o => o.Code);

            foreach (var group in filteredData)
            {
                var first = group.First();
                var references = string.Join(",", group.AsEnumerable().Select(o => o.Reference));
                var data = new BomData
                {
                    Reference = references,
                    Code = first.Code,
                    Type = first.Type,
                    Description = first.Description,
                    Value = first.Value,
                    Count = group.Count()

                };
                Log($"Groupped Data: {data}");
                result.Add(data);
            }
            return new GrouppedBomData(line, index, result);
        }
    }
}
