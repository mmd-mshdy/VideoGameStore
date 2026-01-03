using VideoGameStore.Domain.Abstractions;
using VideoGameStore.Domain.common;
namespace VideoGameStore.Domain.Entities
{

    public class Inventory : BaseEntity
    {
        public int TotalStock { get; private set; }
        public int AvailableStock { get; private set; }
        public bool IsDigital { get; private set; }

        protected Inventory() { }

        public Inventory(int totalStock, bool isDigital)
        {
            IsDigital = isDigital;
            TotalStock = isDigital ? int.MaxValue : totalStock;
            AvailableStock = isDigital ? int.MaxValue : totalStock;
        }

        public Result Reserve(int quantity)
        {
            if (!IsDigital && AvailableStock < quantity)
                return Result.Failure(new ("Stock.Unavailable","Stock is not available"));

            if (!IsDigital)
            {
                AvailableStock -= quantity;
                return Result.Success();
            }
            return Result.Success();


        }

        public Result Release(int quantity)
        {
            if (!IsDigital)
            {
                AvailableStock += quantity;
                return Result.Success();
            }
            return Result.Failure(new("Inventory.Release.Failed", "can not release this game"));

        }
    }
}
