using Domain.Account.Agreggates;
using Bogus;
using Bogus.Extensions.Brazil;

namespace __mock__
{
    public static class MockCustomer
    {
        public static Customer GetFaker()
        {
            var fakeCustomer = new Faker<Customer>()
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.Name, f => f.Name.FirstName())
                .RuleFor(c => c.CPF, f => f.Person.Cpf())
                .RuleFor(c => c.Birth, f => f.Person.DateOfBirth)
                .Generate();

            return fakeCustomer;
        }
    }
}