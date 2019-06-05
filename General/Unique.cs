using System;

namespace General
{
    public interface IUnique
    {
        int Next();
    };
    
    public class Unique : IUnique
    {
        private readonly object semaphone = new object();
        private int identity = 0;

        public int Next()
        {
            lock (semaphone)
            {
                identity++;
                return identity;
            }
        }
    };
};
