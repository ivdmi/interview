using System;
using System.Linq;

namespace TestConsoleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            TestDelegate testDelegate = new TestDelegate();
            testDelegate.MyDelegate += TestMethod2;
            testDelegate.MyDelegate += TestMethod;
            testDelegate.MyDelegate += delegate(int x, int y) { Console.WriteLine("x*x + y*y = " + (x * x + y * y)); return (x*x + y*y); };
            testDelegate.MyDelegate += (x, y) =>
            {
                Console.WriteLine("То же с лямбда: x*x + y*y = " + (x*x + y*y));
                return (x*x + y*y);
            };


            testDelegate.MyEvent += TestMethodString;
            testDelegate.MyEvent += TestMethodStringReverse;

            testDelegate.MyEventHandler += TestMethodEvent;

            testDelegate.MyInterfaceEvent += TestDelegate_MyInterfaceEvent;

            testDelegate.MyActionEvent += delegate(EventArgs eventArgs) { Console.WriteLine("Произошло событие MyActionEvent");};

            testDelegate.LaunchDelegate(5, 7);

            // в отличии от event с полем-делегатом можно делать извне что угодно: переназначить, вызвать, ... - что нарушает инкапсуляцию
            testDelegate.MyDelegate = TestMethod2;
            testDelegate.MyDelegate(10, 10);

            var byRef = new ByRef() {Value = 0};
            var byVal = new ByVal() { Value = 0 };

            Console.WriteLine("Finish");

            Console.ReadLine();
        }

        class ByRef
        {
            public byte Value { get; set; }
        }

        struct ByVal
        {
            public byte Value { get; set; }
        }

        // Интерфейс - демонстрация : Событие может быть членом интерфейса
        interface IEvent
        {
            event EventHandler<MyEventArgs> MyInterfaceEvent;
        }

        public class MyEventArgs : EventArgs
        {
            public string msg;
        }

        // объявить тип делегата - как класс
        public delegate int MyDelegateType(int x, int y);
        public delegate void MyDelegateString(string s);

        public class TestDelegate: IEvent
        {
            public MyDelegateType MyDelegate;

            public event MyDelegateString MyEvent;
            public event EventHandler MyEventHandler;
            public event EventHandler<MyEventArgs> MyInterfaceEvent;
            public event Action<EventArgs> MyActionEvent;
            public void LaunchDelegate(int x, int y)
            {
                int z = MyDelegate(x, y);
                Console.WriteLine("Z = " + z);

                MyEvent("Произошло простое событие!");
                MyEventHandler(this, new EventArgs());

                try
                {
                    MyInterfaceEvent(this, new MyEventArgs() {msg = "Возврат в вызываемый метод - МОЕ СООБЩЕНИЕ"});
                }
                catch (NullReferenceException e1)
                {
                    Console.WriteLine("NullReferenceException ошибка " + e1.Message);
                }
                catch (Exception e2)
                {
                    Console.WriteLine("Общая ошибка " + e2.Message);
                }
                finally
                {
                    Console.WriteLine("Этот блок будет вызыван всегда");
                }

                if (MyActionEvent != null) MyActionEvent(new EventArgs());
            }

        }

        public static int TestMethod(int x, int y)
        {
            int z = x + y;
            return z;
        }

        public static int TestMethod2(int x, int y)
        {
            int z = x*y;
            Console.WriteLine("Произведение: " + z);
            return z;
        }

        public static void TestMethodString(string s)
        {
            Console.WriteLine("Строка1: " + s);
        }
        public static void TestMethodStringReverse(string s)
        {
            var revStr = new string(s.Reverse().ToArray());
            Console.WriteLine("Реверсированная строка: " + revStr);
        }

        public static void TestMethodEvent(object sender, EventArgs e)
        {
            Console.WriteLine("Событие!!! " + sender);
        }

        public static void TestDelegate_MyInterfaceEvent(object sender, MyEventArgs e)
        {
            Console.WriteLine(sender + " вернул: " + e.msg);
        }
    }
}

/*
Произведение: 35
x*x + y*y = 74
То же с лямбда: x*x + y*y = 74
Z = 74
Строка: Произошло простое событие!
Событие!!! TestConsoleApplication.Program+TestDelegate
TestConsoleApplication.Program+TestDelegate вернул: Возврат в вызываемый метод -
 МОЕ СООБЩЕНИЕ
Этот блок будет вызыван всегда
Произошло событие MyActionEvent
Произведение: 100
Finish

*/
