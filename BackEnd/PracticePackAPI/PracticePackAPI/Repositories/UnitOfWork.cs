using PracticePackAPI.Context;
using PracticePackAPI.Models;

namespace PracticePackAPI.Repositories
{
    public interface iUnitOfWork
    {
        Task CommitChangesAsync();

        GenericRepository<Address> AddressRepo { get; }
        GenericRepository<Branch> BranchRepo { get; }
        GenericRepository<Card> CardRepo { get; }
        GenericRepository<Package> PackageRepo { get; }
        GenericRepository<User> UserRepo { get; }

    }

    public class UnitOfWork : iUnitOfWork
    {
        public ApiContext _context;
        private GenericRepository<Address> _addressRepo;
        private GenericRepository<Branch> _branchRepo;
        private GenericRepository<Card> _cardRepo;
        private GenericRepository<Package> _packageRepo;
        private GenericRepository<User> _userRepo;

        public UnitOfWork(ApiContext context)
        {
            _context =  context;
        }

        public async Task CommitChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public GenericRepository<Address> AddressRepo
        {
            get
            {
                if(_addressRepo == null)
                    _addressRepo = new GenericRepository<Address>(_context);
                return _addressRepo;
            }
        }

        public GenericRepository<Branch> BranchRepo
        {
            get
            {
                if(_branchRepo == null)
                    _branchRepo = new GenericRepository<Branch>(_context);
                return _branchRepo;
            }
        }

        public GenericRepository<Card> CardRepo
        {
            get
            {
                if(_cardRepo == null)
                    _cardRepo = new GenericRepository<Card>(_context);
                return _cardRepo;
            }
        }

        public GenericRepository<Package> PackageRepo
        {
            get
            {
                if(_packageRepo == null)
                    _packageRepo = new GenericRepository<Package>(_context);
                return _packageRepo;
            }
        }

        public GenericRepository<User> UserRepo
        {
            get
            {
                if(_userRepo == null)
                    _userRepo = new GenericRepository<User>(_context);
                return _userRepo;
            }
        }

    }
}