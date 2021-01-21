using LaundryIroningContract.Business;
using LaundryIroningContract.Repository;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningHelper.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningBusiness.Entity
{
    public class UserBusiness: IUserBusiness
    {
        #region Private Variable

        private readonly IUserRepository _userRepository;

        #endregion

        #region Constructor

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IUnitOfWork Uow
        {
            get
            {
                return _userRepository.Uow;
            }
            set
            {
                _userRepository.Uow = value;
            }
        }

        #endregion


        #region Get Method
        public async Task<List<Users>> GetUsersAsync()
        {
            var unitsList = await _userRepository.SelectAsync();
            return unitsList.ToList();
        }
        #endregion

        #region Add Method

        public async Task<int> AddUserAsync(Users user)
        {
            if (string.IsNullOrEmpty(user.UserName) || user.Password == null || user.Name == null || user.MobileNo == null)
                return (int)StatusCode.ExpectationFailed;

            var existingTemplate = await _userRepository.SelectAsync(u => u.UserName == user.UserName || u.MobileNo == user.MobileNo);

            if (existingTemplate.Any())
                return (int)StatusCode.ConflictStatusCode;

            user.CreatedAt = DateTimeOffset.UtcNow;
              

            await _userRepository.AddAsync(user);
            await _userRepository.Uow.SaveChangesAsync();

            return (int)StatusCode.SuccessfulStatusCode;
        }
        #endregion
    }
}
