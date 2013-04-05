using System;
using TradePlatform.MT4.SDK.API;
using TradePlatform.MT4.SDK.Library.Handlers;

namespace TradePlatform.MT4.SDK.Library.Experts
{
    public class MACrossExpert : ExtendedExpertAdvisor
    {
        private bool fastUnderSlow;

        protected override int Init()
        {
            return 1;
        }

        protected override int Start()
        {
            double fastMA = this.iMA(this.Symbol(), TIME_FRAME.PERIOD_H1, 3, 0, MA_METHOD.MODE_EMA,
                                     APPLY_PRICE.PRICE_CLOSE, 0);
            double slowMA = this.iMA(this.Symbol(), TIME_FRAME.PERIOD_H1, 5, 0, MA_METHOD.MODE_EMA,
                                     APPLY_PRICE.PRICE_CLOSE, 0);

            fastMA = Math.Round(fastMA, this.Digits());
            slowMA = Math.Round(slowMA, this.Digits());

            if (fastMA < slowMA)
            {
                fastUnderSlow = true;
            }
            else
            {
                fastUnderSlow = false;
            }

            if (fastMA == slowMA)
            {
                if (fastUnderSlow)
                {
                    this.OrderSend(this.Symbol(), ORDER_TYPE.OP_BUY, 0.1, this.Ask(), 3, this.Bid() - 100*this.Point(),
                                   this.Bid() + 100*this.Point());
                }
                else
                {
                    this.OrderSend(this.Symbol(), ORDER_TYPE.OP_SELL, 0.1, this.Bid(), 3, this.Ask() + 100*this.Point(),
                                   this.Ask() - 100*this.Point());
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