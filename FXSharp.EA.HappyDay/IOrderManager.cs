namespace FXSharp.EA.HappyDay
{
    public interface IOrderManager
    {
        //void OnNewBar();

        void OnTick();

        void OnLondonOpen();

        void OnNewYorkClose();
    }
}