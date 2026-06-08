namespace PAA_Modul2.Models
{
    public class Student
    {
        public int id_student { get; set; }
        public string nama { get; set; } = string.Empty;
        public string kelas { get; set; } = string.Empty;
        public string nim { get; set; } = string.Empty;
        public string? alamat { get; set; }
        public decimal? nilai { get; set; }
    }
}