using Autofac;
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
    interface IConsole
    {

    }
    class ConsoleLog : ILog
    {
        public void write(string message)
        {
            Console.WriteLine(message);
        }
    }
    class EmailLog : ILog, IConsole //(B) Mutliple interfaces
    {
        private string email = "admin@foo.com";
        public void write(string message)
        {
            Console.WriteLine($"Email sent to {email}: {message}");
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
            var builder = new ContainerBuilder();
            //builder.RegisterType<ConsoleLog>().As<ILog>();
            //builder.RegisterType<ConsoleLog>().As<ILog>().AsSelf();// (A)

            // (B) In order register for multiple interface chain AS()
            builder.RegisterType<EmailLog>()
                .As<IConsole>()
                .As<ILog>();
            builder.RegisterType<ConsoleLog>().As<ILog>().PreserveExistingDefaults();


            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            IContainer container = builder.Build();

            var car = container.Resolve<Car>();

            // (A) If we need to resolve or get the object for console log as below, we need to call AsSelf() while register type
            //var logObj = container.Resolve<ConsoleLog>();

            //Below is for without DI.
            //var log = new ConsoleLog();
            //var engine = new Engine(log);
            //var car = new Car(engine, log);
            car.GoForward();

            Console.ReadKey();
        }
    }
}
