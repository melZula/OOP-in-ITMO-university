using System;

namespace lab7
{
    class Client
    {
        public string name;
        public string surname;
        public string address;
        public string passportNo;
        public bool good;
        public Client(string n, string sn, string a, string pass)
        {
            name = n; surname = sn;
            address = a; passportNo = pass;
            if (String.IsNullOrEmpty(address) || String.IsNullOrEmpty(pass)){
                good = false;
            } else { good = true; }
        }
    }
}