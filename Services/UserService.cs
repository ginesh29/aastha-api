using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.Entities.Models;
using AASTHA2.Repositories.Interfaces;
using AASTHA2.Services.DTO;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

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
            var users = _unitOfWork.Users.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort);
            var totalCount = users.Count();
            var paged = users.ToPageList(filterModel.skip, filterModel.take);
            var mapped = _mapper.Map<List<UserDTO>>(paged).AsQueryable();
            return new PaginationModel
            {
                Data = mapped,
                StartPage = totalCount > 0 ? filterModel.skip + 1 : 0,
                EndPage = totalCount > filterModel.take ? filterModel.skip + filterModel.take : totalCount,
                TotalCount = users.Count()
            };
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
            userDto.Password = PasswordHash.GenerateHash(userDto.Password);
            var user = _mapper.Map<User>(userDto);
            _unitOfWork.Users.Create(user);
            _unitOfWork.SaveChanges();
            userDto.Id = user.Id;
        }
        public void PutUser(UserDTO userDto)
        {
            userDto.Password = PasswordHash.GenerateHash(userDto.Password);
            var user = _mapper.Map<UserDTO, User>(userDto);
            _unitOfWork.Users.Update(user);
            _unitOfWork.SaveChanges();
        }
        public void RemoveUser(UserDTO userDto, bool removePhysical = false)
        {
            var user = _mapper.Map<User>(userDto);
            _unitOfWork.Users.Delete(user, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
