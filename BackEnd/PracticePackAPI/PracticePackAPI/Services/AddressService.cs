using System.Linq.Expressions;
using PracticePackAPI.Models;
using PracticePackAPI.Repositories;

namespace PracticePackAPI.Services
{
    public class AddressService : iService<Address>
    {
        private readonly iUnitOfWork _unitOfWork;

        public AddressService(iUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public Task<ICollection<Address>> GetAll()
        {
            return _unitOfWork.AddressRepo.GetAll();
        }

        public ICollection<Address> GetAllWithData(
            Expression<Func<Address, bool>> filter = null,
            Func<IQueryable<Address>, IOrderedQueryable<Address>> orderBy = null)
        {
            return _unitOfWork.AddressRepo.GetAllWithData(filter, orderBy).ToList();
        }

        public Task<Address> GetById(Guid Id)
        {
            return _unitOfWork.AddressRepo.GetById(Id);
        }

        public async Task<Address> Create(Address entity)
        {
            await _unitOfWork.AddressRepo.Create(entity);
            await _unitOfWork.CommitChangesAsync();
            return entity;
        }

        public async Task<Address> Update(Address entity)
        {
            if(await _unitOfWork.AddressRepo.Exist(entity.Id))
            {
                _unitOfWork.AddressRepo.Update(entity);
                await _unitOfWork.CommitChangesAsync();
            }
            else
                return null;                
            return await _unitOfWork.AddressRepo.GetById(entity.Id);
        }

        public async Task Delete(Guid Id)
        {
            await _unitOfWork.AddressRepo.Delete(Id);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task<bool> Exist(Guid id)
        {
            return await _unitOfWork.AddressRepo.Exist(id);
        }
    }
}