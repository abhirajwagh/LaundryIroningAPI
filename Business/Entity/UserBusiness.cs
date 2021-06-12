using LaundryIroningCommon;
using LaundryIroningContract.Business;
using LaundryIroningContract.Repository;
using LaundryIroningEntity.Contract;
using LaundryIroningEntity.Entity;
using LaundryIroningEntity.ViewModels;
using LaundryIroningEntity.ViewModels.StoredProcedureModels;
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

        public async Task<List<AdminAgentUserViewModel>> GetAdminAgentOperatorUsersAsync(List<string> userType)
        {
            var userList = await _userRepository.GetAdminAgentOperatorUsersAsync(userType);
            if (userList.Count() > 0)
                for (int i = 0; i < userList.Count(); i++)
                {
                    userList[i].Password = Convert.ToString(EncryptionandDecryption.Decrypt(userList[i].Password));
                }
            return userList.ToList();
        }

        public async Task<List<AgentViewModel>> GetAgentUsersAsync(List<string> userType)
        {
            var agentList = new List<AgentViewModel>();
            var userList = await _userRepository.GetAdminAgentOperatorUsersAsync(userType);
            for (int i = 0; i < userList.Count(); i++)
            {
                var agentModel = new AgentViewModel();
                agentModel.UserId = userList[i].UserId;
                agentModel.UserName = userList[i].UserName;
                agentModel.Name = userList[i].Name;
                agentModel.MobileNo = userList[i].MobileNo;
                agentList.Add(agentModel);
            }
            return agentList;
        }

        public async Task<List<GetAllOrdersForCustomer>> GetAllOrdersForCustomerAsync(Guid customerId, int noOfDays)
        {
            return await _userRepository.GetAllOrdersForCustomerAsync(customerId,noOfDays);
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
            catch (Exception)
            { 
                return new Users();
            }
           
            
        }

        /// <summary>
        /// Check the enter security answer is valid or not 
        /// </summary>
        /// <param name="answerModel"></param>
        /// <returns>boolean value</returns>
        public async Task<bool> CheckSecurityAnswersAsync(SecurityAnswerModelViewModel answerModel)
        {
            var users = await _userRepository.SelectAsync(u =>  u.UserName == answerModel.UserName && 
                                                                u.MobileNo == answerModel.MobileNo && 
                                                                u.SecurityAnswerOne == answerModel.SecurityAnswerOne && 
                                                                u.SecurityAnswerTwo == answerModel.SecurityAnswerTwo);
            if (users.Any())
            {
                return true;
            }
            else
            {
                return false;
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

        /// <summary>
        /// add the admin or agent user 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> AddAdminAgentUsersAsync(AdminAgentUserViewModel user)
        {
            if (string.IsNullOrEmpty(user.UserName) || user.Password == null || user.Name == null || user.MobileNo == null)
                return (int)StatusCode.ExpectationFailed;

            var existingTemplate = await _userRepository.SelectAsync(u => u.UserName == user.UserName || u.MobileNo == user.MobileNo);
            if (existingTemplate.Any())
                return (int)StatusCode.ConflictStatusCode;

            var userTypes = new List<UserTypes>();
            if (user.IsAdmin == false && user.IsAgent == true && user.IsOperator == false)
            {
                userTypes = (await _userTypesRepository.SelectAsync(u => u.UserType == UserTypesConstants.Agent)).ToList();
            }
            else if (user.IsAdmin == true && user.IsAgent == false && user.IsOperator == false)
            {
                userTypes = (await _userTypesRepository.SelectAsync(u => u.UserType == UserTypesConstants.Admin)).ToList();
            }
            else if (user.IsAdmin == false && user.IsAgent == false && user.IsOperator == true)
            {
                userTypes = (await _userTypesRepository.SelectAsync(u => u.UserType == UserTypesConstants.Operator)).ToList();
            }

            var newuser = new Users();
            newuser.UserId = Guid.NewGuid();
            newuser.UserName = user.UserName;
            newuser.Password = Convert.ToString(EncryptionandDecryption.Encrypt(user.Password));
            newuser.Name = user.Name;
            newuser.MobileNo = user.MobileNo;
            newuser.Address = user.Address;
            newuser.Email = user.Email;
            newuser.CreatedAt = DateTime.UtcNow;
            newuser.UserTypeId = userTypes[0].UserTypeId;
            newuser.DateOfBirth = user.DateOfBirth;
            newuser.AadharNo = user.AadharNo;
            newuser.DrivingLicenceNo = user.DrivingLicenceNo;
            

            await _userRepository.AddAsync(newuser);
            await _userRepository.Uow.SaveChangesAsync();

            return (int)StatusCode.SuccessfulStatusCode;
        }

        #endregion

        #region Update Methods
        /// <summary>
        /// update admin and agent users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> UpdateAdminAgentUsersAsync(AdminAgentUserViewModel user)
        {
            if (string.IsNullOrEmpty(user.UserName) || user.Password == null || user.Name == null || user.MobileNo == null)
                return (int)StatusCode.ExpectationFailed;

            var existingTemplate = await _userRepository.SelectAsync(u => u.UserId == user.UserId);
            if (existingTemplate.Any())
            {
                var existingMobile = await _userRepository.SelectAsync(u => u.MobileNo == user.MobileNo && u.UserId != user.UserId);
                if (existingMobile.Any())
                    return (int)StatusCode.ConflictStatusCode;

                existingTemplate[0].Password = Convert.ToString(EncryptionandDecryption.Encrypt(user.Password));
                existingTemplate[0].Name = user.Name;
                existingTemplate[0].MobileNo = user.MobileNo;
                existingTemplate[0].Address = user.Address;
                existingTemplate[0].Email = user.Email;
                existingTemplate[0].CreatedAt = DateTime.UtcNow;

                await _userRepository.UpdateAsync(existingTemplate[0]);
                await _userRepository.Uow.SaveChangesAsync();

                return (int)StatusCode.SuccessfulStatusCode;

            } else
            {
                return (int)StatusCode.NoContent;
            }
           
        }
        #endregion

        #region Delete Methods

        /// <summary>
        /// delete admin agent users
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleteAdminAgentUsers(Guid userId)
        {
            var existingTemplate = await _userRepository.SelectAsync(u => u.UserId == userId);
            if (existingTemplate.Any())
            {
                await _userRepository.DeleteAsync(existingTemplate[0]);
                await _userRepository.Uow.SaveChangesAsync();
                return (int)StatusCode.SuccessfulStatusCode;
            } else
            {
                return (int)StatusCode.NoContent;
            }
        }
        #endregion
    }
}
