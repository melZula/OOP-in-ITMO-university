using System;

namespace lab7
{
    class Account
    {
        public int id;
        protected double _money;
        virtual public double money {get; set;}
        protected Client owner; 
        protected Bank bank;
        protected double procent = 0;
        protected DateTime created = Time.Get();
        protected DateTime lastUpdate = Time.Get();
        virtual public void update() {}
        protected double commission;
        public Account(Client cl, Bank bk)
        {
            id = Banking.uniq;
            _money = 0;
            owner = cl;
            bank = bk;
        }
        public void show()
        {
            System.Console.WriteLine("{0} || {1} || {2} ", id, owner.name, Math.Round(_money, 2));
        }   
    }
    class DebetAccount: Account
    {
        public override double money
        {
            get { return _money; }
            set
            {
                if (value >= 0) { this._money = value; }
            }
        }
        public override void update()
        {
            if (Time.Get() > lastUpdate.AddDays(1))
            {
                procent += _money * bank.procent / 365;
                lastUpdate = Time.Get();
            }
            if (Time.Get().Day == created.Day)
            {
                _money += procent;
            }
        }
        public DebetAccount(Client cl, Bank bk) : base(cl, bk) {}
    }
    class DepositAccount: Account
    {
        double depositProcent = 0;
        DateTime expDt;
        public override double money
        {
            get { return _money; }
            set
            {
                if (value >= 0) { _money = value; }
                else if (Time.Get() > expDt && (value > 0)) { this._money = value; }
            } 
        }
        public override void update()
        {
            if (Time.Get() > lastUpdate.AddDays(1))
            {
                procent += _money * depositProcent;
                lastUpdate = Time.Get();
            }
            if (Time.Get().Day == created.Day)
            {
                _money += procent;
            }
        }
        public DepositAccount(Client cl, Bank bk, double money, DateTime _exp): base(cl, bk)
        {
            expDt = _exp;
            _money = money;
            if(_money < 50000) {depositProcent = 0.05; }
            else if (_money < 100000) {depositProcent = 0.055; }
            else {depositProcent = 0.06; }
        }
    }
    class CreditAccount: Account
    {
        private double negLimit;
        public override double money
        {
            get { return _money; }
            set
            {
                if (value > negLimit) { this._money = value; }
            }
        }
        public override void update()
        {
            if (Time.Get() > lastUpdate.AddDays(1))
            {
                if (_money < 0) {commission += bank.commission * money; }
                lastUpdate = Time.Get();
            }
            if (Time.Get().Day == created.Day)
            {
                _money -= commission;
            }
        }
        public CreditAccount(Client cl, Bank bk, double _limit): base (cl, bk)
        {
            negLimit = _limit;
        }
    }
}