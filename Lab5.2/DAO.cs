using System;
using System.IO;

namespace Lab5._2
{
        public class DAO: IDisposable
    {
        private string mode;
        private bool disposed;
        public DAO()
        {
            using (StreamReader file = new StreamReader(@"config.txt"))
            {
                string line = file.ReadLine();
                if (line == "database")
                {
                    mode = "db";
                }
                else { mode = "csv"; }
            }
        }
        public IModel data()
        {
            if (mode == "db") { return new ShoppingDB(); }
            else { return new ShoppingCSV(); }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                // Free any other managed objects here.
                //
            }
            // Free any unmanaged objects here.
            //
            disposed = true;
        }
        ~DAO()
        {
            Dispose(false);
        }
    }

}