using VideoGameStore.Domain.Enums;
namespace VideoGameStore.Domain.ValueObjects
{
    public record Membership(MembershipType Type)
    {
        public int RentLimit => Type switch
        {
            MembershipType.Free => 1,
            MembershipType.Silver => 3,
            MembershipType.Gold => 5,
            _ => 1
        };

        public bool CanRent(int activeRentals) => activeRentals < RentLimit;
    }

}
