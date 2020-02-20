using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

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
        public IEnumerable<dynamic> GetUsers(string filter, out int totalCount, string sort = "", int skip = 0, int take = 0, string includeProperties = "", string fields = "")
        {
            IEnumerable<User> user = _unitOfWork.Users
                .Find(null, out totalCount, filter, includeProperties, sort, skip, take);
            var mapped = _mapper.Map<IEnumerable<UserDTO>>(user);
            return mapped.DynamicSelect(fields).ToDynamicList();
        }
        public bool IsUserExist(string filter = "")
        {
            return _unitOfWork.Users.FirstOrDefault(null, filter) != null;
        }
        public UserDTO GetUser(long id, string filter = "", string includeProperties = "")
        {
            var user = _unitOfWork.Users.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            return _mapper.Map<UserDTO>(user);
        }

        public void PostUser(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _unitOfWork.Users.Create(user);
            _unitOfWork.SaveChanges();
            userDto.id = user.Id;
        }
        public void PutUser(UserDTO userDto)
        {            
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
