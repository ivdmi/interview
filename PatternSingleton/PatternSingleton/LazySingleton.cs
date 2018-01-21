using System;

namespace PatternSingleton
{
    public class LazySingleton
    {
        public string Id { get; set; }

        private LazySingleton()
        {
            Id = Guid.NewGuid().ToString();
        }

        public static LazySingleton GetInstance()
        {
            return Nested.instance;
        }

        private class Nested
        {
            internal static LazySingleton instance = new LazySingleton();
        }
    }

    public class LazyTSingleton
    {

        /* 
         Для .NET Framework 4 (или выше), можно использовать System.Lazy<T>, 
         чтобы реализовать ленивую загрузку. 
         Нужно передать делегат конструктору, который вызывает конструктор Singleton, которому передается лямбда-выражение
        */

        public string Id { get; set; }

        private LazyTSingleton()
        {
            Id = Guid.NewGuid().ToString();
        }

        // В связи с тем, что конструктор private а не public, он должен быть передан делегату для инициализации
        private static Lazy<LazyTSingleton> lazyInstance = new Lazy<LazyTSingleton>( ()=> new LazyTSingleton());
        
        public static LazyTSingleton GetInstance()
        {
            return lazyInstance.Value;
        }
    }
}
