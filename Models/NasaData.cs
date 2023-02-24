using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class NasaData
    {
        public string? name { get; set; }
        public int  id { get; set; }

        //[DataType(DataType.Date)]
        public string? nametype { get; set; }
        public string? recclass { get; set; }
        public string? mass { get; set; }
        public string? fall { get; set; }
        public string? year { get; set; }
        public Decimal reclat { get; set; }
        public Decimal reclong { get; set; }
        public string? GeoLocation { get; set; }


    }
}
