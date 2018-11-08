# payslip-api-webapicore2
a restful api built with c# web api core 2.1

To set up the project:

* Visual Studio 2017 is preferred.
* WebApi core 2.1 SDK is required.
* Open in VS.
* Restore nuget packages and rebuild.
* Run the project.

There are two endpoints available:

1. `GET /payslips/healthCheck` - health check for the api availibity.
2. `POST /payslips` - Create a payslip for a employee.

Models:

**Employee:**
```
{
	"firstName": "Alan",
	"lastName": "wang",
	"annualSalary": "115000",
	"superRate": "0.05",
	"paymentStartDate": "2018-10-11"
}
```

**PaySlip:**
```
{
    "id": 54,
    "fullName": "Alan wang",
    "payPeriod": "01 October - 31 October",
    "grossIncome": 9583,
    "incomeTax": 2515,
    "netIncome": 7068,
    "super": 479
}
```
