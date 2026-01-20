public class User

{

    public required string Username { get; set; }

    public required string AccountNumber { get; set; }

    public required string Pin { get; set; }

    public required string PhoneNumber { get; set; }

    public string BirthPlace { get; set; }

    public DateTime BirthDate { get; set; }

    public int Balance { get; set; } = 100000;

    public List<string> Transactions { get; set; } = new List<string>();

}
