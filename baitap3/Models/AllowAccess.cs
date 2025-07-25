// Models/AllowAccess.cs
namespace BaiOnline3.Models
{
    public class AllowAccess
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string TableName { get; set; }
        public string AccessProperties { get; set; }
    }
}