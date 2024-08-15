namespace Entities.Dto
{
    public class BaseUser
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public int UserType { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}