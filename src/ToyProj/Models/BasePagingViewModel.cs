using System.ComponentModel.DataAnnotations;

namespace ToyProj.Models
{
    public abstract class BasePagingViewModel
    {
        public const int CountDefault = 50;
        public int Skip { get; set; } = 0;
        public int Count { get; set; } = CountDefault;
        public int? Total { get; set; }
        public string SearchTerm { get; set; }
    }
}
