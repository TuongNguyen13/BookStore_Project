using System.ComponentModel.DataAnnotations.Schema;

namespace API_BookStore.Entites
{
    [Table("Account")]
    public class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public int EmployeeID { get; set; }

    }
}
