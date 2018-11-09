using Entity;
using Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public interface IPaySlipService
    {
        Task<PaySlip> CreatePaySlipAsync(Employee employee);
    }

    public class PaySlipService : IPaySlipService
    {
        readonly ITaxFactory _taxFactory;
        readonly IPaySlipRepository _paySlipRepository;

        public PaySlipService(
            ITaxFactory taxFactory,
            IPaySlipRepository paySlipRepository)
        {
            _taxFactory = taxFactory;
            _paySlipRepository = paySlipRepository;
        }

        /// <summary>
        /// Create a Pay Slip for a Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns><see cref="{PaySlip}"/></returns>
        public async Task<PaySlip> CreatePaySlipAsync(Employee employee)
        {
            var grossIncome = CalculateGrossIncome(employee.AnnualSalary);
            var incomeTax = CalculateIncomeTax(employee.AnnualSalary);
            var netIncome = CalculateNetIncome(grossIncome, incomeTax);
            var super = CalculateSuper(grossIncome, employee.SuperRate);

            var payPeriod = GetPayPeriod(employee.PaymentStartDate);

            var paySlip = new PaySlip
            {
                FullName = $"{employee.FirstName} {employee.LastName}",
                PayPeriod = payPeriod,
                GrossIncome = grossIncome,
                IncomeTax = incomeTax,
                NetIncome = netIncome,
                Super = super
            };

            //persist payslips
            paySlip.Id = await _paySlipRepository.CreatePaySlipAsync(paySlip);

            return paySlip;
        }

        /// <summary>
        /// Calculate Gross Income
        /// </summary>
        /// <param name="annualSalary"></param>
        /// <returns>115000</returns>
        private decimal CalculateGrossIncome(decimal annualSalary)
        {
            return Math.Round(annualSalary / 12);
        }

        /// <summary>
        /// Calculate Income Tax
        /// </summary>
        /// <param name="annualSalary"></param>
        /// <returns>1</returns>
        private decimal CalculateIncomeTax(decimal annualSalary)
        {
            decimal incomeTax = 0;

            //get taxable structs
            var taxable = _taxFactory.GetTaxable();

            //var tax = taxable.ToArray().SingleOrDefault(t => t.Low <= annualSalary && t.High >= annualSalary);

            //incomeTax = Math.Round((tax.CarryOver + tax.TaxPerDollar * (annualSalary - tax.Low)) / 12);

            for (int i=0; i<taxable.Count; i++)
            {
                //find the tax band
                if (taxable[i].Low <= annualSalary && taxable[i].High >= annualSalary)
                {
                    //calculate tax based on tax band information.
                    incomeTax = Math.Round((taxable[i].CarryOver + taxable[i].TaxPerDollar * (annualSalary - taxable[i].Low)) / 12);
                    break;
                }
            }

            return incomeTax;
        }

        /// <summary>
        /// Calculate Net Income
        /// </summary>
        /// <param name="grossIncome"></param>
        /// <param name="incomeTax"></param>
        /// <returns>9583</returns>
        private decimal CalculateNetIncome(decimal grossIncome, decimal incomeTax)
        {
            return grossIncome - incomeTax;
        }

        /// <summary>
        /// Calculate Super
        /// </summary>
        /// <param name="grossIncome"></param>
        /// <param name="superRate"></param>
        /// <returns>287</returns>
        private decimal CalculateSuper(decimal grossIncome, decimal superRate)
        {
            return Math.Round(grossIncome * superRate);
        }

        //Can be extracted to a helper class later if there is a need.
        /// <summary>
        /// Get the pay period based on input date
        /// </summary>
        /// <param name="payPeriod"></param>
        /// <returns>01 October - 31 October</returns>
        private string GetPayPeriod(DateTime payPeriod)
        {
            var lastDayOfMonth = new DateTime(payPeriod.Year, payPeriod.Month, DateTime.DaysInMonth(payPeriod.Year, payPeriod.Month));
            var firstDayOfMonth = new DateTime(payPeriod.Year, payPeriod.Month, 1);

            return $"{firstDayOfMonth.ToString("dd MMMM")} - {lastDayOfMonth.ToString("dd MMMM")}";
        }

    }
}
