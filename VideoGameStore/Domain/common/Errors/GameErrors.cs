namespace VideoGameStore.Domain.common.Errors
{
    public static class GameErrors
    {
        public static readonly Error InValidPrice = new("Game.Price.Invalid", "Price is invalid");
        public static readonly Error GameUnavailable = new("Game.IsAvailable.False", "Game is not available");
        public static readonly Error GameNotFetched = new("Game.Get.Fail", "Game couldn't be fetched");
    }
}
