using Npgsql;
using PAA_Modul2.Helpers;
using PAA_Modul2.Models;

namespace PAA_Modul2.Context
{
    public class StudentContext
    {
        private string __constr;
        private string __ErrorMsg = string.Empty;

        public StudentContext(string pConstr)
        {
            __constr = pConstr;
        }

        public List<Student> ListStudent()
        {
            List<Student> list = new List<Student>();
            string query = "SELECT id_student, nama, kelas, nim, alamat, nilai FROM school.student ORDER BY id_student";
            SqlDBHelper db = new SqlDBHelper(__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Student()
                    {
                        id_student = int.Parse(reader["id_student"].ToString()!),
                        nama = reader["nama"].ToString()!,
                        kelas = reader["kelas"].ToString()!,
                        nim = reader["nim"].ToString()!,
                        alamat = reader["alamat"].ToString(),
                        nilai = reader["nilai"] == DBNull.Value ? null
                                     : decimal.Parse(reader["nilai"].ToString()!)
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; }
            return list;
        }

        public Student? GetStudentById(int id)
        {
            Student? student = null;
            string query = $"SELECT id_student, nama, kelas, nim, alamat, nilai FROM school.student WHERE id_student = {id}";
            SqlDBHelper db = new SqlDBHelper(__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    student = new Student()
                    {
                        id_student = int.Parse(reader["id_student"].ToString()!),
                        nama = reader["nama"].ToString()!,
                        kelas = reader["kelas"].ToString()!,
                        nim = reader["nim"].ToString()!,
                        alamat = reader["alamat"].ToString(),
                        nilai = reader["nilai"] == DBNull.Value ? null
                                     : decimal.Parse(reader["nilai"].ToString()!)
                    };
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; }
            return student;
        }

        public bool CreateStudent(Student s)
        {
            string nilai = s.nilai.HasValue ? s.nilai.ToString()! : "NULL";
            string query = $"INSERT INTO school.student (nama, kelas, nim, alamat, nilai) VALUES ('{s.nama}', '{s.kelas}', '{s.nim}', '{s.alamat}', {nilai})";
            SqlDBHelper db = new SqlDBHelper(__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return true;
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; return false; }
        }

        public bool UpdateStudent(int id, Student s)
        {
            string nilai = s.nilai.HasValue ? s.nilai.ToString()! : "NULL";
            string query = $"UPDATE school.student SET nama='{s.nama}', kelas='{s.kelas}', nim='{s.nim}', alamat='{s.alamat}', nilai={nilai} WHERE id_student={id}";
            SqlDBHelper db = new SqlDBHelper(__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                int rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return rows > 0;
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; return false; }
        }

        public bool DeleteStudent(int id)
        {
            string query = $"DELETE FROM school.student WHERE id_student={id}";
            SqlDBHelper db = new SqlDBHelper(__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                int rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return rows > 0;
            }
            catch (Exception ex) { __ErrorMsg = ex.Message; return false; }
        }
    }
}