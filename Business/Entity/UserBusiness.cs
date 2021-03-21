using LaundryIroningCommon;
using LaundryIroningContract.Business;
using LaundryIroningContract.Repository;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
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
        private readonly IUserTypesRepository _userTypesRepository;
        #endregion

        #region Constructor

        public UserBusiness(IUserRepository userRepository, IUserTypesRepository userTypesRepository)
        {
            _userRepository = userRepository;
            _userTypesRepository = userTypesRepository;
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
                _userTypesRepository.Uow = value;
            }
        }

        #endregion


        /// <summary>
        /// Get All User
        /// </summary>
        /// <returns></returns>
        #region Get Method
        public async Task<List<Users>> GetUsersAsync()
        {
            var unitsList = await _userRepository.SelectAsync();
            return unitsList.ToList();
        }

        /// <summary>
        /// get 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> GetUserNameAsync(string username)
        {
            var users = await _userRepository.SelectAsync(u => u.UserName == username);
            if (users.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check user exists
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<Users> GetUserDetailsAsync(Login login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.UserName) || login.Password == null)
                    return new Users();

                var user = await _userRepository.SelectAsync(u => u.UserName == login.UserName);
                var pasword = EncryptionandDecryption.Decrypt(user[0].Password);
                if (user.Count() > 0)
                {
                    if (pasword == login.Password)
                    {
                        return user[0];
                    } else
                    {
                        return new Users();
                    }
                }
                else
                {
                    return new Users();
                }
            }
            catch (Exception ex)
            { 
                return new Users();
            }
           
            
        }
        #endregion

        #region Add Method

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> AddUserAsync(Users user)
        {
            if (string.IsNullOrEmpty(user.UserName) || user.Password == null || user.Name == null || user.MobileNo == null)
                return (int)StatusCode.ExpectationFailed;

            var existingTemplate = await _userRepository.SelectAsync(u => u.UserName == user.UserName || u.MobileNo == user.MobileNo);
            var userTypes = await _userTypesRepository.SelectAsync(u => u.UserType == UserTypesConstants.Customer);

            if (existingTemplate.Any())
                return (int)StatusCode.ConflictStatusCode;
            user.Password = Convert.ToString(EncryptionandDecryption.Encrypt(user.Password));
            user.CreatedAt = DateTime.UtcNow;
            user.UserId = Guid.NewGuid();
            user.UserTypeId = userTypes[0].UserTypeId;

            await _userRepository.AddAsync(user);
            await _userRepository.Uow.SaveChangesAsync();

            return (int)StatusCode.SuccessfulStatusCode;
        }
        
        #endregion
    }
}
