namespace Event.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        
        public int CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? DeleteBy { get; set; }

        public DateTime? DeleteDate { get; set; }

        public int State { get; set; }
    }
}
