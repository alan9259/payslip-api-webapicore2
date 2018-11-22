using Entity;
using Moq;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class PaySlipServiceTest
    {
        readonly IPaySlipService _paySlipService;
        readonly Mock<IPaySlipRepository> _stubPaySlipRepository;
        readonly Mock<ITaxFactory> _stubTaxFactory;
        public PaySlipServiceTest()
        {
            _stubPaySlipRepository = new Mock<IPaySlipRepository>();
            _stubTaxFactory = new Mock<ITaxFactory>();

            _stubTaxFactory
                .Setup(factory => factory.GetTaxable())
                .Returns(SetupTaxable());

            _stubPaySlipRepository
                .Setup(repo => repo.CreatePaySlipAsync(It.IsAny<Entity.PaySlip>()))
                .ReturnsAsync(1);

            _paySlipService = new PaySlipService(_stubTaxFactory.Object, _stubPaySlipRepository.Object);
        }

        [Fact]
        public async Task ShouldSuccess_CreatePaySlipAsync()
        {
            var employee = CreateEmployee("Alan", "Wang", 60050, (decimal)0.09, new DateTime(2018, 10, 30));

            var result = await _paySlipService.CreatePaySlipAsync(employee);

            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldFullNameReturned_CreatePaySlipAsync()
        {
            var employee = CreateEmployee("Alan", "Wang", 60050, (decimal)0.09, new DateTime(2018, 10, 30));

            var result = await _paySlipService.CreatePaySlipAsync(employee);

            Assert.True(result.FullName == "Alan Wang");
        }


        [Fact]
        public async Task ShouldGrossIncomeReturned_CreatePaySlipAsync()
        {
            var employee = CreateEmployee("Alan", "Wang", 60050, (decimal)0.09, new DateTime(2018, 10, 30));

            var result = await _paySlipService.CreatePaySlipAsync(employee);

            Assert.True(result.GrossIncome == 5004);
        }

        [Fact]
        public async Task ShouldIncomeTaxReturned_CreatePaySlipAsync()
        {
            var employee = CreateEmployee("Alan", "Wang", 60050, (decimal)0.09, new DateTime(2018, 10, 30));

            var result = await _paySlipService.CreatePaySlipAsync(employee);

            Assert.True(result.IncomeTax == 922);
        }

        [Fact]
        public async Task ShouldNetIncomeReturned_CreatePaySlipAsync()
        {
            var employee = CreateEmployee("Alan", "Wang", 60050, (decimal)0.09, new DateTime(2018, 10, 30));

            var result = await _paySlipService.CreatePaySlipAsync(employee);

            Assert.True(result.NetIncome == 4082);
        }

        [Fact]
        public async Task ShouldSuperReturned_CreatePaySlipAsync()
        {
            var employee = CreateEmployee("Alan", "Wang", 60050, (decimal)0.09, new DateTime(2018, 10, 30));

            var result = await _paySlipService.CreatePaySlipAsync(employee);

            Assert.True(result.Super == 450);
        }

        [Fact]
        public async Task ShouldPayPeriodReturned_CreatePaySlipAsync()
        {
            var employee = CreateEmployee("Alan", "Wang", 60050, (decimal)0.09, new DateTime(2018, 10, 30));

            var result = await _paySlipService.CreatePaySlipAsync(employee);

            Assert.True(result.PayPeriod == "01 October - 31 October");
        }


        private Employee CreateEmployee(
            string firstName, 
            string lastName, 
            decimal annualSalary, 
            decimal superRate, 
            DateTime paymentStartDate)
        {
            var employee = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                AnnualSalary = annualSalary,
                SuperRate = superRate,
                PaymentStartDate = paymentStartDate
            };

            return employee;
        }

        private List<Tax> SetupTaxable()
        {
            var taxable = new List<Tax>();

            //the tax paramters should come out of a database, however for the sake of simplicity, I used hard coded value.

            var tax1 = new Tax(0, 18200, 0, 0); //nill
            var tax2 = new Tax(18201, 37000, (decimal)0.19, 0); //19c for each $1 over $18,200
            var tax3 = new Tax(37001, 87000, (decimal)0.325, 3572); //$3,572 plus 32.5c for each $1 over $37,000
            var tax4 = new Tax(87001, 180000, (decimal)0.37, 19822); //$19,822 plus 37c for each $1 over $87, 000
            var tax5 = new Tax(180001, decimal.MaxValue, (decimal)0.45, 54232); //$54,232 plus 45c for each $1 over $180, 000

            taxable.AddRange(new[] { tax1, tax2, tax3, tax4, tax5 });

            return taxable;
        }
    }
}
