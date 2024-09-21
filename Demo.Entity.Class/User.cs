namespace Dmo.Entity.Class
{

    public class User
    {

        //private string? uname;
        private int account;
        private string pwd;

        public int Account { get => account; set => account = value; }
        public string Pwd { get => pwd; set => pwd = value; }
        //public string Uname { get => uname; set => uname = value; }
    }
}
