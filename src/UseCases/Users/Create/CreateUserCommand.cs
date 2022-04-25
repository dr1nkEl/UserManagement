using MediatR;
using UseCases.Common.User;

namespace UseCases.Users;

public record CreateUserCommand(UserRegisterDto RegistrationModel) : IRequest;
