using ClassLibrary;
using FluentAssertions;
using NSubstitute.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class FluentAssertionsTests
    {
        [Fact]
        public void AssertingStringsTest() 
        {
            var result = "Some String";
            
            result.Should().NotBeNullOrEmpty();
            result.Should().Be("Some String");
            result.Should().StartWith("Some");
            result.Should().EndWith("String");
            result.Should().HaveLength(11);
            result.Should().NotBe("String Some");
            result.ToLower().Should().NotBeLowerCased();
            result.ToUpper().Should().NotBeUpperCased();
        }

        [Fact]
        public void AssertingNummberTest() 
        {
            int result = 17;

            result.Should().Be(17);
            result.Should().BePositive();
            result.Should().BeGreaterThan(16);
            result.Should().BeLessThanOrEqualTo(17);   
            result.Should().BeInRange(16,18);   
        }

        [Fact]
        public void AssertingDatesTest() 
        {
            DateTime result = new(2023,10,11);

            result.Should().Be(new(2023, 10, 11));
        }

        public class TestCustomer 
        {
            public int Number { get; set; }
            public string Name { get; set; } = "";
            public DateTime? Date{ get; set; }
        };
        [Fact]
        public void AssertingObjects()
        {
            var expected = new TestCustomer() 
            {
                Number = 10, 
                Name = "This is the Name", 
                Date =new DateTime(2024, 1, 1)
            };            
            
            var result = new TestCustomer() 
            {
                Number = 10, 
                Name = "This is the Name", 
                Date =new DateTime(2024, 1, 1)
            };

            //You can check all the values of an object.
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void AssertingEnumerablesWithValues()
        {
            IEnumerable<int> array = new[] { 1, 2, 3, 4, 5 };

            array.Should().Contain(4);
        }

        public IEnumerable<TestCustomer> customers = new[]
        {
            new TestCustomer
            {
                Number = 10,
                Name = "This is the Name",
                Date =new DateTime(2024, 1, 1)
            },
            new TestCustomer
            {
                Number = 11,
                Name = "This is 11th Name",
                Date =new DateTime(2024, 1, 11)
            },    
            new TestCustomer
            {
                Number = 12,
                Name = "This is 12th Name",
                Date =new DateTime(2024, 1, 12)
            }
        };   
        [Fact]
        public void AssertingEnumerablesWithObjects() 
        {
            var expected = new TestCustomer
            {
                Number = 10,
                Name = "This is the Name",
                Date = new DateTime(2024, 1, 1)
            };

            //this is only to cast to an array. Same as ToArray();
            var customersArray = customers.As<TestCustomer[]>();

            customersArray.Should().ContainEquivalentOf(expected);
            customersArray.Should().HaveCount(3);
            customersArray.Should().Contain(x => x.Name == "This is the Name" && x.Number == 10);
        }

        [Fact]
        public void AssertThrowException()
        {
            //Arrange
            var _sut = new Calculator();
            //Act
            Action result = () => _sut.Divide(1, 0);
            //Test
            result.Should().Throw<Exception>();
            result.Should().Throw<DivideByZeroException>();
            result.Should().Throw<DivideByZeroException>().WithMessage("Attempted to divide by zero.");
        }

        public class ObjectWithEvent 
        {
            public event EventHandler? ExampleEvent;
            public virtual void RaiseExampleEvent() 
            {
                if(ExampleEvent is not null)
                    ExampleEvent(this, EventArgs.Empty);
            }
        }
        [Fact]
        public void AssertEventRaised() 
        {
            var _sut = new ObjectWithEvent();
            var monitor = _sut.Monitor();
            _sut.RaiseExampleEvent();

            monitor.Should().Raise("ExampleEvent");
        }
    }

}
