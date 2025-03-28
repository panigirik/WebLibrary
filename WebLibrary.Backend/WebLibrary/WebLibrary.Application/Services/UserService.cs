using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto?>(user);
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        return _mapper.Map<UserDto?>(user);
    }

    public async Task AddUserAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userRepository.AddAsync(user);
    }

    public async Task UpdateUserAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
    }
}