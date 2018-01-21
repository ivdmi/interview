using System;

namespace PatternSingleton
{
    public class SimplySinleton
    {
        private static SimplySinleton instance;

        public string Id { get; set; }

        private SimplySinleton()
        {
            Id = Guid.NewGuid().ToString();
        }

        public static SimplySinleton GetInstance()
        {
            if (instance == null)
                instance = new SimplySinleton();
            return instance;
        }
    }

    public class LockSinleton
    {
        private static LockSinleton instance;

        private static object _lockObject = new object();

        public string Id { get; set; }

        private LockSinleton()
        {
            Id = Guid.NewGuid().ToString();
        }

        public static LockSinleton GetInstance()
        {
            if (instance == null)
                lock (_lockObject)
                {
                    if (instance == null)
                        instance = new LockSinleton();
                }
            return instance;
        }
    }

}
