namespace TradePlatform.MT4.SDK.Library.Experts
{
    using System;
    using TradePlatform.MT4.SDK.API;
    using TradePlatform.MT4.SDK.Library.Handlers;

    public class MACrossExpert : ExtendedExpertAdvisor
    {
        bool fastUnderSlow = false;

        protected override int Init()
        {
            return 1;
        }

        protected override int Start()
        {
            double fastMA = this.iMA(this.Symbol(), TIME_FRAME.PERIOD_H1, 3, 0, MA_METHOD.MODE_EMA, APPLY_PRICE.PRICE_CLOSE, 0);
            double slowMA = this.iMA(this.Symbol(), TIME_FRAME.PERIOD_H1, 5, 0, MA_METHOD.MODE_EMA, APPLY_PRICE.PRICE_CLOSE, 0);

            fastMA = Math.Round(fastMA, this.Digits());
            slowMA = Math.Round(slowMA, this.Digits());

            if (fastMA < slowMA)
            {
                this.fastUnderSlow = true;
            }
            else
            {
                this.fastUnderSlow = false;
            }

            if (fastMA == slowMA)
            {
                if (this.fastUnderSlow)
                {
                    this.OrderSend(this.Symbol(), ORDER_TYPE.OP_BUY, 0.1, this.Ask(), 3, this.Bid() - 100 * this.Point(), this.Bid() + 100 * this.Point());
                }
                else
                {
                    this.OrderSend(this.Symbol(), ORDER_TYPE.OP_SELL, 0.1, this.Bid(), 3, this.Ask() + 100 * this.Point(), this.Ask() - 100 * this.Point());
                }
            }

            return 1;
        }

        protected override int DeInit()
        {
            return 1;
        }
    }
}

