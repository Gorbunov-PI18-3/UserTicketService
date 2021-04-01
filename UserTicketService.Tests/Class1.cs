using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTicketService.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test1()
        {
            Assert.True(100 == 100);
        }
    }


    public class Ticket
    {
        public int Id { get; }
        public string Description { get; }
        public int Price { get; }

        public Ticket(int id, string description, int price)
        {
            this.Id = id;
            this.Description = description;
            this.Price = price;
        }
    }
    public interface ITicketService
    {
        int GetTicketPrice(int ticketId);
    }
    public class TicketService : ITicketService
    {
        public int GetTicketPrice(int ticketId)
        {
            var ticket = FakeBaseData.FirstOrDefault(t => t.Id == ticketId);
            return (ticket is null) ?
              throw new TicketNotFoundException() : ticket.Id;
        }

        public Ticket GetTicket(int ticketId)
        {
            var ticket = FakeBaseData.FirstOrDefault(t => t.Id == ticketId);
            return (ticket is null) ?
              throw new TicketNotFoundException() : ticket;
        }

        public void SaveTicket(Ticket ticket)
        {
            FakeBaseData.Add(ticket);
        }

        public void DeleteTicket(Ticket ticket)
        {
            FakeBaseData.Remove(ticket);
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return FakeBaseData;
        }

        private List<Ticket> FakeBaseData = new List<Ticket>
  {
    new Ticket(1, "Москва - Санкт-Петербург", 3500),
    new Ticket(2, "Челябинск - Магадан", 3500),
    new Ticket(3, "Норильск - Уфа", 3500)
  };
    }

    public class TicketNotFoundException : Exception
    {
    }


    [TestFixture]
    public class TicketServiceIntegrationTests
    {
        [Test]
        public void SaveTicketMustAddTicketInBase()
        {
            var ticketServiceTest = new TicketService();
            var newTicket = new Ticket(300, "Test ticket", 1000);

            ticketServiceTest.SaveTicket(newTicket);

            var allTicketsAfterAddingNewTicket = ticketServiceTest.GetAllTickets();
            CollectionAssert.Contains(allTicketsAfterAddingNewTicket, newTicket);

            ticketServiceTest.DeleteTicket(newTicket);

            var allTicketsAfterDeletingNewTicket = ticketServiceTest.GetAllTickets();
            CollectionAssert.DoesNotContain(allTicketsAfterDeletingNewTicket, newTicket);
        }
    }

    public class Calculator
    {
        public int Additional(int a, int b)
        {
            return a + b;
        }

        public int Subtraction(int a, int b)
        {
            return a - b;
        }

        public int Miltiplication(int a, int b)
        {
            return a * b;
        }

        public int Division(int a, int b)
        {
            return a / b;
        }
    }
    [TestFixture]
    public class CalculatorIntegrationTests
    {
        [Test]
        public void Division_MustThrowException()
        {
            var calculator = new Calculator();
            Assert.Throws<DivideByZeroException>(() => calculator.Division(30, 0));
        }
        [Test]
        public void Subtraction_MustReturnCorrectValue()
        {
            Calculator calculator = new Calculator();
            Assert.True(calculator.Subtraction(300, 10) == 290);
        }
        [Test]
        public void Additional_MustReturnCorrectValue()
        {
            Calculator calculator = new Calculator();
            Assert.True(calculator.Additional(300, 10) == 310);
        }
        [Test]
        public void Multiplication_MustReturnCorrectValue()
        {
            Calculator calculator = new Calculator();
            Assert.True(calculator.Miltiplication(300, 10) == 3000);
        }
    }
}
