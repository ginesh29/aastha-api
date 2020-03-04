using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AASTHA2.Models;
using AutoMapper;
using System.Collections.Generic;

namespace AASTHA2.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public PaginationModel GetUsers(FilterModel filterModel)
        {
            IEnumerable<User> user = _unitOfWork.Users
                .Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort);
            return _mapper.Map<IEnumerable<UserDTO>>(user).ToPageList(filterModel.skip, filterModel.take);
        }
        public bool IsUserExist(string filter = "")
        {
            return _unitOfWork.Users.FirstOrDefault(null, filter) != null;
        }
        public UserDTO VerifyUser(LoginModel loginModel)
        {
            var user = _unitOfWork.Users.FirstOrDefault(m => m.Username == loginModel.Username && m.Password == loginModel.Password);
            return _mapper.Map<UserDTO>(user);
        }
        public UserDTO GetUser(long id, string filter = "", string includeProperties = "")
        {
            var user = _unitOfWork.Users.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            user.Password = "*****";
            return _mapper.Map<UserDTO>(user);
        }

        public void PostUser(UserDTO userDto)
        {
            userDto.password = PasswordHash.GenerateHash(userDto.password);
            var user = _mapper.Map<User>(userDto);
            _unitOfWork.Users.Create(user);
            _unitOfWork.SaveChanges();
            userDto.id = user.Id;
        }
        public void PutUser(UserDTO userDto)
        {
            userDto.password = PasswordHash.GenerateHash(userDto.password);
            var user = _mapper.Map<UserDTO, User>(userDto);
            _unitOfWork.Users.Update(user);
            _unitOfWork.SaveChanges();
        }
        public void RemoveUser(UserDTO userDto, string filter = "", bool removePhysical = false)
        {
            var user = _mapper.Map<User>(userDto);
            _unitOfWork.Users.Delete(user, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
