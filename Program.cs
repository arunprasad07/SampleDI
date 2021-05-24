using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// File is NOT using Depency Injection.
/// </summary>

namespace SampleDI
{
    interface ILog
    {
        void write(string message);
    }
    class ConsoleLog : ILog
    {
        public void write(string message)
        {
            Console.WriteLine(message);
        }
    }
    class Car
    {
        private Engine engine;
        private ILog log;

        public Car(Engine engine, ILog log)
        {
            this.log = log;
            this.engine = engine;
        }

        public void GoForward()
        {
            engine.GoAhead(100);
            log.write("Car going ahead...");
        }
    }
    class Engine
    {
        private ILog log;
        private int id;
             
        public Engine(ILog log)
        {
            this.log = log;
            this.id = new Random().Next();
        }

        public void GoAhead(int power)
        {
            log.write($"Engine [{id}] ahead {power}");
        }
    }
   
    class Program
    {
        static void Main(string[] args)
        {
            var log = new ConsoleLog();
            var engine = new Engine(log);
            var car = new Car(engine, log);
            car.GoForward();

            Console.ReadKey();
        }
    }
}
