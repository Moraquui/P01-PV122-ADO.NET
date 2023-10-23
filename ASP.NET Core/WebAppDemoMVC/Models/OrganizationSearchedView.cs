using DemoClients;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace WebAppDemoMVC.Models
{
    public class OrganizationSearchedView<T> : IEnumerable<T> where T : class
    {
        public OrganizationSearchedView() {

            foreach (var field in typeof(T).GetProperties())
            {
                if (field.PropertyType == typeof(string))
                    AvailableFields.Add(field.Name);
            }
        }

        public SearchType SearchType { get; set; }
        private string fieldName {  get; set; } = string.Empty;
        public string FieldName { get
            {
                return fieldName;
            }
            set {
                fieldName = AvailableFields.Contains(value) ? value : string.Empty;
            } }
        public string Filter {  get; set; } = string.Empty;
        public List<string> AvailableFields { get; set; } = new List<string>();
        public IEnumerable<T>? Values { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            if (Values == null)
                throw new InvalidOperationException("Values is null");

            return Values.GetEnumerator();
        }
        public IQueryable<T> GetSortedQueue(IQueryable<T> query)
        {
            if (!string.IsNullOrEmpty(Filter))
            {
                switch (SearchType)
                {
                    case SearchType.StartsWith:
                        query = query.Where(obj => EF.Property<string>(obj, FieldName).StartsWith(Filter));
                        break;
                    case SearchType.EndWith:
                        query = query.Where(obj => EF.Property<string>(obj, FieldName).EndsWith(Filter));
                        break;
                    case SearchType.Contains:
                        query = query.Where(obj => EF.Property<string>(obj, FieldName).Contains(Filter));
                        break;
                    default:
                        query = query.Where(obj => EF.Property<string>(obj, FieldName).StartsWith(Filter));
                        break;
                }
            }

            return query;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
