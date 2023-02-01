using System.ComponentModel.DataAnnotations;
using WebAPI_one.Validation;

namespace WebAPI_one.ViewModel
{
    public class Car
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [StringLength(15)]
        public string Name { get; set; } = "";
        [ProductionDateValidation]
        public DateTime ProductionDate { get; set; }

        public string Type { get; set; } = "Gas";
    }
}
