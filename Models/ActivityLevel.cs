using System.ComponentModel.DataAnnotations;

namespace serverSideCapstone.Models
{
    public class ActivityLevel
    {
        [Key]
        public int ActivityLevelId {get; set;}
        public string Name {get; set;}
    }
}