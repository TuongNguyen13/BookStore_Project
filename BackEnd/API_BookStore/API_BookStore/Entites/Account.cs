using System.ComponentModel.DataAnnotations.Schema;

namespace API_BookStore.Entites
{
    [Table("Account")]
    public class Account
    {
        private int ID { get; set; }
        private string Username { get; set; }
        private string Pass { get; set; }
        private int EmployeeID { get; set; }

    }
}
