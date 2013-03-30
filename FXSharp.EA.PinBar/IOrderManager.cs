namespace FXSharp.EA.PinBar
{
    internal interface IOrderManager
    {
        void OnNewBullishPinBar();

        void OnNewBearishPinBar();

        void OnTick();
    }
}