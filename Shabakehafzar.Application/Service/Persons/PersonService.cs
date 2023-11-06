using Shabakehafzar.Application.DTOs.RequestModels.Person;
using Shabakehafzar.Application.DTOs.ResponceModels.Person;
using Shabakehafzar.Application.Helpers;
using Shabakehafzar.Data.DTOs.Person;
using Shabakehafzar.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shabakehafzar.Application.Service.Persons
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetAllPersonResponce>> GetAllWithLinq()
        {
            var persons = await _unitOfWork.PersonRepository.GetAllWithLinq();

            var result = persons.Select(p => new GetAllPersonResponce
            {
                Id = p.Id,
                FullName = p.FullName,
                Addresses = p.Addresses.Select(a => new PersonAddressResponce
                {
                    City = a.City,
                    Street = a.Street,
                    Id = a.Id

                })
            });

            return result;
        }

        public async Task<IEnumerable<GetAllPersonResponce>> GetAllWithLambda()
        {
            var persons = await _unitOfWork.PersonRepository.GetAllWithLambda();

            var result = persons.Select(p => new GetAllPersonResponce
            {
                Id = p.Id,
                FullName = p.FullName,
                Addresses = p.Addresses.Select(a => new PersonAddressResponce
                {
                    City = a.City,
                    Street = a.Street,
                    Id = a.Id

                })
            });

            return result;
        }

        public async Task<IEnumerable<GetAllPersonResponce>> GetAllWithTSQL()
        {
            var persons = await _unitOfWork.PersonRepository.GetAllWithTSQL();

            var result = persons.Select(p => new GetAllPersonResponce
            {
                Id = p.Id,
                FullName = p.FullName,
                Addresses = p.Addresses.Select(a => new PersonAddressResponce
                {
                    City = a.City,
                    Street = a.Street,
                    Id = a.Id

                })
            });

            return result;
        }

        public async Task<Guid> Create(CreatePersonRequest command)
        {
            var checkFullNameIsDuplicated = await _unitOfWork.PersonRepository.AnyAsync(p=>p.FullName == command.FullName);

            if (checkFullNameIsDuplicated)
                ExceptionResponceHandler.ThrowManagedException("Full Name is Duplicate");

            var personAddressesDto = command.Addresses.Select(a => new CreatePersonAddressDto
            {
                City = a.City,
                Street = a.Street
            });
            var personId = _unitOfWork.PersonRepository.Create(command.FullName, personAddressesDto);
            await _unitOfWork.CommitAsync();

            return personId;
        }


    }
}
