using Ardalis.SmartEnum;

namespace PharmacyApp.Domain.CartManagement.Enum;
    public sealed class CartStateEnum : SmartEnum<CartStateEnum, int>
    {
        public static readonly CartStateEnum Active = new(nameof(Active), 1);
        public static readonly CartStateEnum Inactive = new(nameof(Inactive), 2);

        private CartStateEnum(string name, int value) : base(name, value)
        {
        }

        public bool CanTransitionTo(CartStateEnum nextState)
        {
            return this switch
            {
                var s when s == Active => nextState == Inactive,
                var s when s == Inactive => false,
                _ => false
            };
        }
    }

