using System;
using System.Threading;
using System.Threading.Tasks;

namespace PatternSingleton
{
    class Program
    {
        private static void Main(string[] args)
        {
            // простейший SingletonSimple - норм. работает в 1 потоке

            // проблемы многопоточности: при инициализации синглтона, когда оба потока одновременно выполняют код
            // if (instance == null) instance = ... 


            // раздельная реализация - повтор усвоенного
            
            new Task(() =>
            {
                var stone1 = SimplySinleton.GetInstance();
                Console.WriteLine("stone1: " + stone1.Id);
            }
            ).Start();

            new Task(() =>
            {
                var stone2 = SimplySinleton.GetInstance();
                Console.WriteLine("stone2: " + stone2.Id);
            }
            ).Start();


            new Task(() =>
            {
                var lockStone1 = LockSinleton.GetInstance();
                Console.WriteLine("LockStone1: " + lockStone1.Id);
            }).Start();

            new Task(() =>
            {
                var lockStone2 = LockSinleton.GetInstance();
                Console.WriteLine("LockStone2: " + lockStone2.Id);
            }).Start();

            new Task(() =>
            {
                var lazyStone1 = LazySingleton.GetInstance();
                Console.WriteLine("LazyStone1: " + lazyStone1.Id);
            }
            ).Start();

            new Task(() =>
            {
                var lazyStone2 = LazySingleton.GetInstance();
                Console.WriteLine("LazyStone2: " + lazyStone2.Id);
            }
            ).Start();

            new Task(() =>
            {
                string lazyTSingleton1 = LazyTSingleton.GetInstance().Id;
                Console.WriteLine("lazyTSingleton1: " + lazyTSingleton1);
            }).Start();

            new Task(() =>
            {
                string lazyTSingleton2 = LazyTSingleton.GetInstance().Id;
                Console.WriteLine("lazyTSingleton2: " + lazyTSingleton2);
            }).Start();

            // конец раздельной реализации


            new Task(() =>
            {
                SingletonSimple guid1 = SingletonSimple.GetInstance();
                Console.WriteLine("{0} - {1}", "guid-1", guid1.Name);
            }
            ).Start();


            SingletonSimple guid2 = SingletonSimple.GetInstance();
            Console.WriteLine("{0} - {1}", "guid-2", guid2.Name);

            Thread.Sleep(100);

            // lock

            new Task(() =>
            {
                GuidSingletonLock guidLock1 = GuidSingletonLock.GetInstance();
                Console.WriteLine("{0} - {1}", "guidLock1", guidLock1.Name);
            }
            ).Start();

            // можем получить: 
            // guid - 1 - 85686239 - 905b - 494f - be74 - f00249cdee9a
            // guid - 2 - 1f1e9f14 - 6501 - 437f - 9366 - f2dee9704723

            GuidSingletonLock guidLock2 = GuidSingletonLock.GetInstance();
            Console.WriteLine("{0} - {1}", "guidLock2", guidLock2.Name);

            // без lock

            new Task(() =>
            {
                GuidSingletonNoneLock guidNoneLock1 = GuidSingletonNoneLock.GetInstance();
                Console.WriteLine("{0} - {1}", "guidNoneLock1", guidNoneLock1.Name);
            }
            ).Start();

            GuidSingletonNoneLock guidNoneLock2 = GuidSingletonNoneLock.GetInstance();
            Console.WriteLine("{0} - {1}", "guidNoneLock2", guidNoneLock2.Name);

            // Lazy

            new Task(() =>
            {
                GuidSingletonLazy guidSingletonLazy1 = GuidSingletonLazy.GetInstance();
                Console.WriteLine("{0} - {1}", "GuidSingletonLazy1", guidSingletonLazy1.Name);
            }
            ).Start();

            GuidSingletonLazy guidSingletonLazy2 = GuidSingletonLazy.GetInstance();
            Console.WriteLine("{0} - {1}", "GuidSingletonLazy2", guidSingletonLazy2.Name);


            // Lazy<T>

            new Task(() =>
            {
                GuidSingletonLazyT GuidSingletonLazyT1 = GuidSingletonLazyT.GetInstance();
                Console.WriteLine("{0} - {1}", "GuidSingletonLazyT1", GuidSingletonLazyT1.Name);
            }
            ).Start();

            GuidSingletonLazyT GuidSingletonLazyT2 = GuidSingletonLazyT.GetInstance();
            Console.WriteLine("{0} - {1}", "GuidSingletonLazyT2", GuidSingletonLazyT2.Name);

            Console.ReadKey();
        }
    }


    // простейший Singleton
    class SingletonSimple
    {
        private static SingletonSimple instance;

        public string Name { get; private set; }

        private SingletonSimple(string name)
        {
            Name = name;
        }

        public static SingletonSimple GetInstance()
        {
            if (instance == null)
                instance = new SingletonSimple(Guid.NewGuid().ToString());
            return instance;
        }
    }

    // потокобезопасный Singleton  - lock
    class GuidSingletonLock
    {
        private static GuidSingletonLock instance;

        public string Name { get; private set; }

        private static object lockObject = new object();

        private GuidSingletonLock(string name)
        {
            Name = name;
        }

        public static GuidSingletonLock GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)                                       // блокируем до завершения операции
                {
                    if (instance == null)
                        instance = new GuidSingletonLock(Guid.NewGuid().ToString());
                }
            }

            return instance;
        }
    }

    // потокобезопасный Singleton  - без lock
    class GuidSingletonNoneLock
    {
        private static readonly GuidSingletonNoneLock instance = new GuidSingletonNoneLock();

        public string Name { get; private set; }

        private GuidSingletonNoneLock()
        {
            Name = Guid.NewGuid().ToString();
        }

        public static GuidSingletonNoneLock GetInstance()
        {
            return instance;
        }
    }


    // потокобезопасный Singleton  - Lazy - инициализация
    class GuidSingletonLazy
    {
        public string Name { get; private set; }

        private GuidSingletonLazy()
        {
            Name = Guid.NewGuid().ToString();
        }

        public static GuidSingletonLazy GetInstance()
        {
            return Nested.instance;
        }

        private class Nested
        {
            internal static readonly GuidSingletonLazy instance = new GuidSingletonLazy();
        }
    }


    // потокобезопасный Singleton  - инициализация через Lazy<T> 
    class GuidSingletonLazyT
    {
        private static readonly Lazy<GuidSingletonLazyT> lazyInstance =
            new Lazy<GuidSingletonLazyT>(() => new GuidSingletonLazyT());

        public string Name { get; private set; }

        private GuidSingletonLazyT()
        {
            Name = Guid.NewGuid().ToString();
        }

        public static GuidSingletonLazyT GetInstance()
        {
            return lazyInstance.Value;
        }
    }



}
