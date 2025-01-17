﻿using System;
using System.Collections.Generic;

namespace ConsoleApplication5
{
    public class Market 
    {
        public delegate void ObserverDelegate(object sender);

        private ObserverDelegate _observerHandler;
        public ObserverDelegate ObserverHandler
        {
            get { return _observerHandler; }
            set
            {
                _observerHandler = value;
                NotifyObserver();
            }
        }
    
 
        public List<Bid> BidList
        {
            get
            {
                return _bidList;
            }
        }

        public List<Ask> AskList
        {
            get
            {
                return _askList;
            }
        }

        private List<Bid> _bidList = new List<Bid>();
        private List<Ask> _askList = new List<Ask>();


        private void AcquireTrades()
        {
            foreach (Bid bid in _bidList)
            {
                foreach (Ask ask in _askList)
                {
                    if (bid.ackquireTranId == Guid.Empty)
                        if (bid.SecCode == ask.SecCode && ask.Amount == bid.Amount && bid.Price <= ask.Price &&
                            ask.ackquireTranId == Guid.Empty)
                        {
                            var tranId = Guid.NewGuid();

                            ask.ackquireTranId = tranId;
                            bid.ackquireTranId = tranId;
                        }
                }
            }
        }

        public void RegisterSell(string secCode, decimal price, int amount, Broker broker)
        {
            _bidList.Add(new Bid(secCode, price, amount, broker));
            AcquireTrades();
            NotifyObserver();

        }
        public void RegisterBuy(string secCode, decimal price, int amount, Broker broker)
        {
            _askList.Add(new Ask(secCode, price, amount, broker));
            AcquireTrades();
            NotifyObserver();

        }

        private void NotifyObserver()
        {
            if (ObserverHandler!=null)
            ObserverHandler(this);
        }
    }
}
