using System.Linq.Expressions;
using PracticePackAPI.Models;
using PracticePackAPI.Repositories;

namespace PracticePackAPI.Services
{
    public class PackageService : iService<Package>
    {
        private readonly iUnitOfWork _unitOfWork;

        public PackageService(iUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public Task<ICollection<Package>> GetAll()
        {
            return _unitOfWork.PackageRepo.GetAll();
        }

        public ICollection<Package> GetAllWithData(
            Expression<Func<Package, bool>> filter = null,
            Func<IQueryable<Package>, IOrderedQueryable<Package>> orderBy = null)
        {
            return _unitOfWork.PackageRepo.GetAllWithData(filter, orderBy).ToList();
        }

        public Task<Package> GetById(Guid Id)
        {
            return _unitOfWork.PackageRepo.GetById(Id);
        }

        public async Task<Package> Create(Package entity)
        {
            await _unitOfWork.PackageRepo.Create(entity);
            await _unitOfWork.CommitChangesAsync();
            return entity;
        }

        public async Task<Package> Update(Package entity)
        {
            if(await _unitOfWork.PackageRepo.Exist(entity.Id))
            {
                _unitOfWork.PackageRepo.Update(entity);
                await _unitOfWork.CommitChangesAsync();
            }
            else
                return null;                
            return await _unitOfWork.PackageRepo.GetById(entity.Id);
        }

        public async Task Delete(Guid Id)
        {
            await _unitOfWork.PackageRepo.Delete(Id);
            await _unitOfWork.CommitChangesAsync();
        }

        public async Task<bool> Exist(Guid id)
        {
            return await _unitOfWork.PackageRepo.Exist(id);
        }
    }
}