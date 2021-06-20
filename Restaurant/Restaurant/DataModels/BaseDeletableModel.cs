namespace Restaurant.DataModels
{
    using System;

    public class BaseDeletableModel<T>
    {
        public T Id { get; set; }

        public bool IsDelete { get; set; }

        public DateTime DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
