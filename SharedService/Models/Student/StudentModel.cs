using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharedService.Models.Student
{
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนชื่อนักเรียน")]
        [DisplayName("ชื่อนักเรียน")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("คะแนนสอบ")]
        [Range(0, 100, ErrorMessage = "กรุณาป้อนคะแนนสอบในช่วง 0-100")]
        public int Score { get; set; }
    }
}
